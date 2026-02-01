#include <iostream>
#include <string>
#include <vector>
#include <windows.h> 
#include "httpClient/httpClient.hpp" 
#include "cJSON/cJSON.h"
#include "config.h" 
#include "date/date.h"

// ==========================================
// Basic Settings
// ==========================================
const std::string DATE_FILE = "date.txt";
const std::string BASE_API = "https://fireflies.chiculture.org.hk/api/quiz";
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

int main() {
    // Set Console to UTF-8
    SetConsoleOutputCP(65001);

    // Load dates (Auto-expands ranges and skips weekends)
    std::vector<std::string> dateList = loadDatesFromFile(DATE_FILE);

    if (dateList.empty()) {
        LOG << "[Error] No valid dates found (Check date.txt or if dates are weekends)!" << std::endl;
        return 1;
    }

    LOG << "[System] Loaded " << dateList.size() << " tasks (Weekends skipped). Starting..." << std::endl;

    for (size_t i = 0; i < dateList.size(); ++i) {
        std::string TARGET_DATE = dateList[i];
        LOG << "\n[Task " << (i + 1) << "/" << dateList.size() << "] Processing Date: " << TARGET_DATE << std::endl;

        HttpClient client;

        // Step 1: Student - Fetch Assignment ID
        LOG << "[Step] Fetching ID..." << std::endl;
        client.setCookieFile(COOKIE_STUDENT);
        client.addHeader("Accept-Encoding: identity");
        client.addHeader("referer: https://fireflies.chiculture.org.hk/app/assignments/" + TARGET_DATE);

        std::string studentUrl = BASE_API + "/assignments/" + TARGET_DATE;
        std::string studentResp = client.get(studentUrl);

        std::string assignmentId = "";
        std::string level = "";

        cJSON* sJson = cJSON_Parse(studentResp.c_str());
        if (sJson) {
            cJSON* data = cJSON_GetObjectItem(sJson, "data");
            cJSON* root = data ? data : sJson;
            cJSON* idItem = cJSON_GetObjectItem(root, "id");
            if (!idItem) idItem = cJSON_GetObjectItem(root, "_id");
            cJSON* lvItem = cJSON_GetObjectItem(root, "lv");

            if (idItem && idItem->valuestring) {
                assignmentId = idItem->valuestring;
                if (lvItem && lvItem->valuestring) level = lvItem->valuestring;
            }
            cJSON_Delete(sJson);
        }

        if (assignmentId.empty()) {
            LOG << "[Skip] Assignment not found for date: " << TARGET_DATE << std::endl;
            continue;
        }

        // Step 2: Admin - Fetch Answers
        LOG << "[Step] Fetching Ans..." << std::endl;
        client.setCookieFile(COOKIE_ADMIN);
        client.addHeader("referer: https://fireflies.chiculture.org.hk/admin/assignments/" + assignmentId);

        std::string adminUrl = BASE_API + "/assignments/" + assignmentId;
        std::string adminResp = client.get(adminUrl);

        QuizInfo finalInfo;
        finalInfo.assignmentId = assignmentId;
        finalInfo.level = level;

        cJSON* aJson = cJSON_Parse(adminResp.c_str());
        if (aJson) {
            cJSON* data = cJSON_GetObjectItem(aJson, "data");
            cJSON* root = data ? data : aJson;
            cJSON* questions = cJSON_GetObjectItem(root, "questions");
            if (!questions) {
                cJSON* article = cJSON_GetObjectItem(root, "article");
                if (article) questions = cJSON_GetObjectItem(article, "questions");
            }

            if (questions && cJSON_IsArray(questions)) {
                int qCount = cJSON_GetArraySize(questions);
                for (int j = 0; j < qCount; j++) {
                    cJSON* q = cJSON_GetArrayItem(questions, j);
                    cJSON* qid = cJSON_GetObjectItem(q, "_id");
                    cJSON* ansItem = cJSON_GetObjectItem(q, "answer");
                    if (!ansItem) ansItem = cJSON_GetObjectItem(q, "correctAnswer");

                    if (qid && qid->valuestring && ansItem) {
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
            LOG << "[Error] Failed to parse answers for date: " << TARGET_DATE << std::endl;
            continue;
        }

        // Step 3: Student - Submit Answers
        LOG << "[Step] Submitting..." << std::endl;
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
            const char* rands_data[] = { "0", "1", "2", "3" };
            cJSON_AddItemToObject(ansObj, "randoms", cJSON_CreateStringArray(rands_data, 4));
            cJSON_AddStringToObject(ansObj, "answered", std::to_string(qa.correctAns).c_str());
            cJSON_AddItemToObject(ansObj, "answeredSeq", cJSON_CreateArray());
            cJSON_AddItemToArray(answersArray, ansObj);
        }
        cJSON_AddItemToObject(submitRoot, "answers", answersArray);

        char* submitPayload = cJSON_PrintUnformatted(submitRoot);
        std::string submitResp = client.post(BASE_API + "/answers", std::string(submitPayload));

        if (submitResp.find("score") != std::string::npos || submitResp.find("correct") != std::string::npos) {
            LOG << "[Success] Task completed for: " << TARGET_DATE << std::endl;

#ifdef ENABLE_EXTRA_READ
            LOG << "[Step] Extra Read..." << std::endl;
            cJSON* readJson = cJSON_CreateObject();
            cJSON_AddStringToObject(readJson, "assignment", finalInfo.assignmentId.c_str());
            cJSON_AddStringToObject(readJson, "lv", finalInfo.level.c_str());
            char* readP = cJSON_PrintUnformatted(readJson);
            client.post(BASE_API + "/answers/extra-read", std::string(readP));
            cJSON_Delete(readJson);
            free(readP);
#endif
        }
        else {
            LOG << "[Warning] Submission response abnormal: " << TARGET_DATE << std::endl;
        }

        cJSON_Delete(submitRoot);
        free(submitPayload);
    }

    LOG << "\nAll tasks completed. Press Enter to exit." << std::endl;
    std::cin.get();
    return 0;
}