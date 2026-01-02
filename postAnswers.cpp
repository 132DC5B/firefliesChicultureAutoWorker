#include <iostream>
#include <string>
#include <vector>
#include <fstream> 
#include <algorithm> // for trim
#include <random>
#include <windows.h> 
#include "httpClient.hpp" 
#include "cJSON.h"

// ==========================================
// 設定
// ==========================================
const std::string DATE_FILE = "date.txt";
const std::string BASE_API = "https://fireflies.chiculture.org.hk/api/quiz";

// 檔案名稱
const std::string COOKIE_STUDENT = "cookies_student.txt";
const std::string COOKIE_ADMIN = "cookies_admin.txt";

struct QuestionAnswer {
    std::string qId;
    int correctAns;
};

struct QuizInfo {
    std::string assignmentId;
    std::string level;
    std::vector<QuestionAnswer> questions;
    bool valid = false;
};

// 【修改】讀取多行日期
std::vector<std::string> loadDatesFromFile(const std::string& filename) {
    std::vector<std::string> dates;
    std::ifstream file(filename);
    std::string line;

    if (file.is_open()) {
        while (std::getline(file, line)) {
            // 去除尾部空白與換行
            size_t end = line.find_last_not_of(" \t\n\r");
            if (end != std::string::npos) {
                line = line.substr(0, end + 1);
            }
            else {
                continue; // 空行跳過
            }

            // 去除首部空白
            size_t start = line.find_first_not_of(" \t\n\r");
            if (start != std::string::npos) {
                line = line.substr(start);
            }

            if (!line.empty()) {
                dates.push_back(line);
            }
        }
        file.close();
    }
    return dates;
}

int main() {
    SetConsoleOutputCP(65001);

    // 1. 讀取所有日期
    std::vector<std::string> dateList = loadDatesFromFile(DATE_FILE);

    if (dateList.empty()) {
        std::cout << "[Error] No valid dates found in date.txt!" << std::endl;
        return 1;
    }

    std::cout << "[System] Loaded " << dateList.size() << " tasks. Starting batch execution...\n" << std::endl;

    // 初始化隨機數 (用於選項順序)
    std::random_device rd;
    std::mt19937 gen(rd());

    // ==========================================
    // 批量迴圈開始
    // ==========================================
    for (size_t i = 0; i < dateList.size(); ++i) {
        std::string TARGET_DATE = dateList[i];

        std::cout << "==================================================" << std::endl;
        std::cout << "Task [" << (i + 1) << "/" << dateList.size() << "] Target Date: " << TARGET_DATE << std::endl;
        std::cout << "==================================================" << std::endl;

        // 【關鍵】HttpClient 宣告於迴圈內
        // 確保每一輪的 Header 都是新的，不會殘留上一輪的 Referer
        HttpClient client;

        // ------------------------------------------------------
        // 第一階段：學生身分 - 獲取 Assignment ID
        // ------------------------------------------------------
        std::cout << "[1/3] (Student) Fetching Assignment ID..." << std::endl;

        client.setCookieFile(COOKIE_STUDENT);
        client.addHeader("Accept-Encoding: identity");
        client.addHeader("referer: https://fireflies.chiculture.org.hk/app/assignments/" + TARGET_DATE);

        std::string studentUrl = BASE_API + "/assignments/" + TARGET_DATE;
        std::string studentResp = client.get(studentUrl);

        std::string assignmentId = "";
        std::string level = "";

        cJSON* sJson = cJSON_Parse(studentResp.c_str());
        if (sJson) {
            cJSON* root = cJSON_GetObjectItem(sJson, "data") ? cJSON_GetObjectItem(sJson, "data") : sJson;
            cJSON* idItem = cJSON_GetObjectItem(root, "id");
            if (!idItem) idItem = cJSON_GetObjectItem(root, "_id");
            cJSON* lvItem = cJSON_GetObjectItem(root, "lv");

            if (idItem) {
                assignmentId = idItem->valuestring;
                if (lvItem) level = lvItem->valuestring;
            }
            cJSON_Delete(sJson);
        }

        if (assignmentId.empty()) {
            std::cout << "[Skip] Cannot fetch Assignment ID for this date (Assignment may not exist).\n" << std::endl;
            continue; // 跳至下一個日期
        }
        std::cout << "[Success] Assignment ID: " << assignmentId << std::endl;


        // ------------------------------------------------------
        // 第二階段：管理員身分 - 獲取標準答案
        // ------------------------------------------------------
        std::cout << "[2/3] (Admin) Fetching answers via Admin..." << std::endl;

        client.setCookieFile(COOKIE_ADMIN);
        client.addHeader("referer: https://fireflies.chiculture.org.hk/admin/assignments/" + assignmentId);

        std::string adminUrl = BASE_API + "/assignments/" + assignmentId;
        std::string adminResp = client.get(adminUrl);

        QuizInfo finalInfo;
        finalInfo.assignmentId = assignmentId;
        finalInfo.level = level;

        cJSON* aJson = cJSON_Parse(adminResp.c_str());
        if (aJson) {
            cJSON* root = cJSON_GetObjectItem(aJson, "data") ? cJSON_GetObjectItem(aJson, "data") : aJson;
            cJSON* questions = cJSON_GetObjectItem(root, "questions");
            if (!questions) {
                cJSON* article = cJSON_GetObjectItem(root, "article");
                if (article) questions = cJSON_GetObjectItem(article, "questions");
            }

            if (questions) {
                int qCount = cJSON_GetArraySize(questions);
                for (int j = 0; j < qCount; j++) {
                    cJSON* q = cJSON_GetArrayItem(questions, j);
                    cJSON* qid = cJSON_GetObjectItem(q, "_id");
                    cJSON* ansItem = cJSON_GetObjectItem(q, "answer");
                    if (!ansItem) ansItem = cJSON_GetObjectItem(q, "correctAnswer");

                    if (qid && ansItem) {
                        QuestionAnswer qa;
                        qa.qId = qid->valuestring;
                        qa.correctAns = ansItem->valueint;
                        finalInfo.questions.push_back(qa);
                    }
                }
                if (!finalInfo.questions.empty()) finalInfo.valid = true;
            }
            cJSON_Delete(aJson);
        }

        if (!finalInfo.valid) {
            std::cout << "[Error] Failed to extract answers via Admin. Skipping.\n" << std::endl;
            continue;
        }


        // ------------------------------------------------------
        // 第三階段：學生身分 - 提交滿分試卷
        // ------------------------------------------------------
        std::cout << "[3/3] (Student) Submitting correct answers..." << std::endl;

        client.setCookieFile(COOKIE_STUDENT);
        client.addHeader("Content-Type: application/json;charset=UTF-8");
        client.addHeader("origin: https://fireflies.chiculture.org.hk");
        client.addHeader("referer: https://fireflies.chiculture.org.hk/app/assignments/" + TARGET_DATE);

        cJSON* submitRoot = cJSON_CreateObject();
        cJSON_AddStringToObject(submitRoot, "assignment", finalInfo.assignmentId.c_str());
        cJSON_AddStringToObject(submitRoot, "assignmentId", finalInfo.assignmentId.c_str()); // 有些接口可能需要這個
        cJSON_AddStringToObject(submitRoot, "lv", finalInfo.level.c_str());

        cJSON* answersArray = cJSON_CreateArray();

        for (const auto& qa : finalInfo.questions) {
            cJSON* ansObj = cJSON_CreateObject();
            cJSON_AddStringToObject(ansObj, "question", qa.qId.c_str());
            cJSON_AddStringToObject(ansObj, "format", "single-select");

            cJSON* rands = cJSON_CreateStringArray(
                (const char* const*)std::vector<const char*>{"0", "1", "2", "3"}.data(), 4);
            cJSON_AddItemToObject(ansObj, "randoms", rands);

            cJSON_AddStringToObject(ansObj, "answered", std::to_string(qa.correctAns).c_str());
            cJSON_AddItemToObject(ansObj, "answeredSeq", cJSON_CreateArray());

            cJSON_AddItemToArray(answersArray, ansObj);
        }
        cJSON_AddItemToObject(submitRoot, "answers", answersArray);

        char* submitPayload = cJSON_PrintUnformatted(submitRoot);
        std::string submitUrl = BASE_API + "/answers";
        std::string submitResp = client.post(submitUrl, std::string(submitPayload));

        if (submitResp.find("score") != std::string::npos || submitResp.find("correct") != std::string::npos) {
            std::cout << "[Result] Submission Successful!" << std::endl;

            // Extra Read
            std::cout << "[Extra] Submitting Extra Read..." << std::endl;
            cJSON* readJson = cJSON_CreateObject();
            cJSON_AddStringToObject(readJson, "assignment", finalInfo.assignmentId.c_str());
            cJSON_AddStringToObject(readJson, "lv", finalInfo.level.c_str());
            char* readP = cJSON_PrintUnformatted(readJson);
            client.post(BASE_API + "/answers/extra-read", std::string(readP));
            cJSON_Delete(readJson);
            free(readP);
        }
        else {
            // 這裡移除了原始 JSON 輸出，改為簡單提示
            std::cout << "[Warning] Submission response abnormal." << std::endl;
        }

        cJSON_Delete(submitRoot);
        free(submitPayload);

        std::cout << ">>> Date " << TARGET_DATE << " Completed.\n" << std::endl;

        // 稍微暫停 1 秒，避免過於頻繁請求被封鎖 (可選)
        Sleep(1000);
    }

    std::cout << "All tasks completed." << std::endl;
    return 0;
}