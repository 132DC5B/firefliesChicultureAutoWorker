#include <iostream>
#include <string>
#include <vector>
#include <windows.h>
#include "../../lib/httpClient/httpClient.hpp"
#include "../../lib/cJSON/cJSON.h"

// ==========================================
// 目標設定
// ==========================================
const std::string TARGET_DATE = "2026-01-01";
const std::string TARGET_URL = "https://fireflies.chiculture.org.hk/api/quiz/assignments/" + TARGET_DATE;
const std::string REFERER_URL = "https://fireflies.chiculture.org.hk/app/assignments/" + TARGET_DATE;

int main_() {
    SetConsoleOutputCP(65001);

    HttpClient client;

    std::cout << "[getQuestion] 正在讀取 Cookie 並獲取題目..." << std::endl;
    std::cout << "[目標] " << TARGET_URL << std::endl;

    client.addHeader("Accept-Encoding: identity");
    client.addHeader("accept: application/json, text/plain, */*");
    client.addHeader("accept-language: en-GB,en-US;q=0.9,en;q=0.8,zh-TW;q=0.7,zh;q=0.6");

    client.addHeader("referer: " + REFERER_URL);

    client.addHeader("sec-ch-ua: \"Google Chrome\";v=\"143\", \"Chromium\";v=\"143\", \"Not A(Brand\";v=\"24\"");
    client.addHeader("sec-ch-ua-mobile: ?0");
    client.addHeader("sec-ch-ua-platform: \"Windows\"");
    client.addHeader("sec-fetch-dest: empty");
    client.addHeader("sec-fetch-mode: cors");
    client.addHeader("sec-fetch-site: same-origin");
    client.addHeader("priority: u=1, i");

    std::string response = client.get(TARGET_URL);

    if (response.empty()) {
        std::cout << "[警示] 回傳內容為空！" << std::endl;
        std::cout << "可能原因：1. Cookie 過期（請重跑 getAccessID） 2. 觸發了 If-None-Match (304)" << std::endl;
        return 1;
    }

    std::cout << "[getQuestion] 收到回覆 (" << response.length() << " bytes)" << std::endl;

    cJSON* resJson = cJSON_Parse(response.c_str());
    if (resJson) {
        char* prettyJson = cJSON_Print(resJson);
        std::cout << "\n>>> 題目數據 (JSON) <<<\n" << prettyJson << "\n" << std::endl;

        free(prettyJson);
        cJSON_Delete(resJson);
    }
    else {
        std::cout << "[錯誤] 無法解析 JSON，原始內容如下：" << std::endl;
        std::cout << response << std::endl;
    }

    return 0;
}