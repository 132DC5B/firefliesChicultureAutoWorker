#include <iostream>
#include <string>
#include <vector>
#include <fstream> 
#include <algorithm>
#include <random>
#include <windows.h> 
#include "CUI/CUI.h"
#include "httpClient/httpClient.hpp" 
#include "cJSON/cJSON.h"
#include "config.h"

// ==========================================
// Basic Settings
// ==========================================
const std::string DATE_FILE = "date.txt";
const std::string BASE_API = "https://fireflies.chiculture.org.hk/api/quiz";

// File names
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

// Function to load dates
std::vector<std::string> loadDatesFromFile(const std::string& filename) {
    std::vector<std::string> dates;
    std::ifstream file(filename);
    std::string line;

    if (file.is_open()) {
        while (std::getline(file, line)) {
            // Trim trailing
            size_t end = line.find_last_not_of(" \t\n\r");
            if (end != std::string::npos) {
                line = line.substr(0, end + 1);
            }
            else {
                continue;
            }

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
    // Enable ANSI colors for Windows Console
    SetConsoleOutputCP(65001);
    HANDLE hOut = GetStdHandle(STD_OUTPUT_HANDLE);
    DWORD dwMode = 0;
    GetConsoleMode(hOut, &dwMode);
    dwMode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING;
    SetConsoleMode(hOut, dwMode);

    // Load dates
    std::vector<std::string> dateList = loadDatesFromFile(DATE_FILE);

    if (dateList.empty()) {
        LOG << "[Error] No valid dates found in date.txt!" << std::endl;
        return 1;
    }

    LOG << "[System] Loaded " << dateList.size() << " tasks. Starting batch execution..." << std::endl;
    // Hide cursor to prevent flickering
    std::cout << "\033[?25l";

    std::random_device rd;
    std::mt19937 gen(rd());

    // ==========================================
    // Batch Loop Start
    // ==========================================
    for (size_t i = 0; i < dateList.size(); ++i) {
        std::string TARGET_DATE = dateList[i];

        // Update Progress: Initial
        drawProgressBar(i + 1, dateList.size(), TARGET_DATE, "Initializing");

        HttpClient client;

        // ======================================================
        // Step 1: Student - Fetch Assignment ID
        // ======================================================
        drawProgressBar(i + 1, dateList.size(), TARGET_DATE, "Fetching ID");

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
            // Newline before error logging to preserve progress bar layout
            std::cout << "\n";
            LOG << "[Skip] Assignment not found for date: " << TARGET_DATE << std::endl;
            continue;
        }

        // ======================================================
        // Step 2: Admin - Fetch Answers
        // ======================================================
        drawProgressBar(i + 1, dateList.size(), TARGET_DATE, "Fetching Ans");

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
            std::cout << "\n";
            LOG << "[Error] Failed to parse answers for date: " << TARGET_DATE << std::endl;
            continue;
        }

        // ======================================================
        // Step 3: Student - Submit Answers
        // ======================================================
        drawProgressBar(i + 1, dateList.size(), TARGET_DATE, "Submitting");

        client.setCookieFile(COOKIE_STUDENT);
        client.addHeader("Content-Type: application/json;charset=UTF-8");
        client.addHeader("origin: https://fireflies.chiculture.org.hk");
        client.addHeader("referer: https://fireflies.chiculture.org.hk/app/assignments/" + TARGET_DATE);

        cJSON* submitRoot = cJSON_CreateObject();
        cJSON_AddStringToObject(submitRoot, "assignment", finalInfo.assignmentId.c_str());
        cJSON_AddStringToObject(submitRoot, "assignmentId", finalInfo.assignmentId.c_str());
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
            // Success - No log needed, progress bar handles it

            // ==========================================
            //    Extra Read
            // ==========================================
#ifdef ENABLE_EXTRA_READ
            drawProgressBar(i + 1, dateList.size(), TARGET_DATE, "Extra Read");
            cJSON* readJson = cJSON_CreateObject();
            cJSON_AddStringToObject(readJson, "assignment", finalInfo.assignmentId.c_str());
            cJSON_AddStringToObject(readJson, "lv", finalInfo.level.c_str());

            char* readP = cJSON_PrintUnformatted(readJson);

            client.post(BASE_API + "/answers/extra-read", std::string(readP));

            cJSON_Delete(readJson);
            free(readP);
#endif
            // ==========================================
        }
        else {
            std::cout << "\n";
            LOG << "[Warning] Submission response abnormal: " << TARGET_DATE << std::endl;
        }

        cJSON_Delete(submitRoot);
        free(submitPayload);
    }

    // Show cursor again
    std::cout << "\033[?25h";

    // Final newline to clear the last progress bar line
    std::cout << "\n\nAll tasks completed. Press Enter to exit." << std::endl;
    std::cin.get();
    return 0;
}