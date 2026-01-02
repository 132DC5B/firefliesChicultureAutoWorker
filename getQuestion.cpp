// 
// 
// 
// 
// 
// 
// 
// 
// 
// 
// 
// 
// 
//  這是一個開發時測試的工具，你不必使用!!
// 
// 
// 
// 
// 
// 
// 
// 
// 
// 
// 
// 
#include <iostream>
#include <string>
#include <vector>
#include <windows.h>
#include "httpClient.hpp"
#include "cJSON.h"

// ==========================================
// 目標設定
// ==========================================
const std::string TARGET_DATE = "2025-12-29"; // 君可改為動態日期
const std::string TARGET_URL = "https://fireflies.chiculture.org.hk/api/quiz/assignments/" + TARGET_DATE;
const std::string REFERER_URL = "https://fireflies.chiculture.org.hk/app/assignments/" + TARGET_DATE;

int main_() {
    // 1. 設定 Console 為 UTF-8，以免中文亂碼
    SetConsoleOutputCP(65001);

    HttpClient client;

    std::cout << "[getQuestion] 正在讀取 Cookie 並獲取題目..." << std::endl;
    std::cout << "[目標] " << TARGET_URL << std::endl;

    // 2. Header 設定 (復刻 Curl)
    // -----------------------------------------------------------
    // 基礎設定
    client.addHeader("Accept-Encoding: identity"); // 強制明文，避開亂碼
    client.addHeader("accept: application/json, text/plain, */*");
    client.addHeader("accept-language: en-GB,en-US;q=0.9,en;q=0.8,zh-TW;q=0.7,zh;q=0.6");

    // 來源偽裝
    client.addHeader("referer: " + REFERER_URL);

    // 瀏覽器指紋 (Chrome 143)
    client.addHeader("sec-ch-ua: \"Google Chrome\";v=\"143\", \"Chromium\";v=\"143\", \"Not A(Brand\";v=\"24\"");
    client.addHeader("sec-ch-ua-mobile: ?0");
    client.addHeader("sec-ch-ua-platform: \"Windows\"");
    client.addHeader("sec-fetch-dest: empty");
    client.addHeader("sec-fetch-mode: cors");
    client.addHeader("sec-fetch-site: same-origin");
    client.addHeader("priority: u=1, i");

    // 【重要】 If-None-Match (Etag)
    // 此行會導致伺服器回傳 304 空內容 (若題目未變)。
    // 為了確保抓到 JSON，吾將其隱去。若君欲測試緩存，請取消註解。
    // client.addHeader("if-none-match: W/\"5010-63pPMUVlX2Dbshk/K28Y0A6k95U\"");

    // 註：Cookie 由 http_client 自動讀取 cookies.txt，無需手動 addHeader

    // 3. 發送 GET 請求
    std::string response = client.get(TARGET_URL);

    // 4. 結果處理
    if (response.empty()) {
        std::cout << "[警示] 回傳內容為空！" << std::endl;
        std::cout << "可能原因：1. Cookie 過期（請重跑 getAccessID） 2. 觸發了 If-None-Match (304)" << std::endl;
        return 1;
    }

    std::cout << "[getQuestion] 收到回覆 (" << response.length() << " bytes)" << std::endl;

    // 5. 解析 JSON 並顯示
    cJSON* resJson = cJSON_Parse(response.c_str());
    if (resJson) {
        // 為了讓君看清結構，先將其格式化印出
        char* prettyJson = cJSON_Print(resJson);
        std::cout << "\n>>> 題目數據 (JSON) <<<\n" << prettyJson << "\n" << std::endl;

        //// 嘗試提取題目 ID 或標題 (需視實際 JSON 結構調整)
        //// 假設結構為 { "data": { "title": "...", "questions": [...] } }
        //cJSON* data = cJSON_GetObjectItem(resJson, "data");
        //if (data) {
        //    cJSON* title = cJSON_GetObjectItem(data, "title");
        //    if (title && title->valuestring) {
        //        std::cout << "--------------------------------" << std::endl;
        //        std::cout << "試卷標題: " << title->valuestring << std::endl;
        //        std::cout << "--------------------------------" << std::endl;
        //    }
        //}

        free(prettyJson);
        cJSON_Delete(resJson);
    }
    else {
        std::cout << "[錯誤] 無法解析 JSON，原始內容如下：" << std::endl;
        std::cout << response << std::endl;
    }

    return 0;
}