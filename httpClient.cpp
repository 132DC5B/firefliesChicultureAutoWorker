#include "httpClient.hpp"
#include <iostream>
#include <cstring>
#include <sstream>
#include <vector>

// 偽裝之 User-Agent
const char* FAKE_USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/143.0.0.0 Safari/537.36";
// 預設 Cookie 檔名
const char* DEFAULT_COOKIE_FILE = "cookies.txt";

HttpClient::HttpClient() {
    curl_global_init(CURL_GLOBAL_ALL);
    curl = curl_easy_init();

    // 【重要】初始化預設 Cookie 檔案
    this->cookieFile = DEFAULT_COOKIE_FILE;
}

HttpClient::~HttpClient() {
    if (headers) curl_slist_free_all(headers);
    if (curl) curl_easy_cleanup(curl);
    curl_global_cleanup();
}

// 【新增】動態設定 Cookie 檔案路徑 (用於切換 學生/管理員)
void HttpClient::setCookieFile(const std::string& filename) {
    this->cookieFile = filename;
}

size_t HttpClient::WriteMemoryCallback(void* contents, size_t size, size_t nmemb, void* userp) {
    size_t realsize = size * nmemb;
    struct MemoryStruct* mem = (struct MemoryStruct*)userp;

    char* ptr = (char*)realloc(mem->memory, mem->size + realsize + 1);
    if (!ptr) return 0; // Out of memory

    mem->memory = ptr;
    memcpy(&(mem->memory[mem->size]), contents, realsize);
    mem->size += realsize;
    mem->memory[mem->size] = 0; // 確保結尾有 Null Terminator

    return realsize;
}

void HttpClient::setupCommonOptions() {
    if (!curl) return;
    curl_easy_setopt(curl, CURLOPT_USERAGENT, FAKE_USER_AGENT);

    // 【修正】使用類別成員變數 cookieFile，而非全域常數
    // 此舉允許程式在運行時切換不同的 Cookie 檔
    curl_easy_setopt(curl, CURLOPT_COOKIEFILE, this->cookieFile.c_str());
    curl_easy_setopt(curl, CURLOPT_COOKIEJAR, this->cookieFile.c_str());

    // 忽略 SSL 證書驗證
    curl_easy_setopt(curl, CURLOPT_SSL_VERIFYPEER, 0L);
    curl_easy_setopt(curl, CURLOPT_SSL_VERIFYHOST, 0L);

    // 自動跟隨 301/302 跳轉
    curl_easy_setopt(curl, CURLOPT_FOLLOWLOCATION, 1L);

    // 開啟詳細除錯模式 (若不需要看詳細握手過程，可設為 0L)
    curl_easy_setopt(curl, CURLOPT_VERBOSE, 1L);

    // 回調函數
    curl_easy_setopt(curl, CURLOPT_WRITEFUNCTION, WriteMemoryCallback);
}

void HttpClient::addHeader(const std::string& header) {
    headers = curl_slist_append(headers, header.c_str());
}

std::string HttpClient::get(const std::string& url) {
    setupCommonOptions();

    struct MemoryStruct chunk;
    chunk.memory = (char*)malloc(1);
    chunk.size = 0;

    // 初始化防止垃圾記憶體
    if (chunk.memory) chunk.memory[0] = '\0';

    curl_easy_setopt(curl, CURLOPT_URL, url.c_str());
    curl_easy_setopt(curl, CURLOPT_HTTPHEADER, headers);
    curl_easy_setopt(curl, CURLOPT_WRITEDATA, (void*)&chunk);

    CURLcode res = curl_easy_perform(curl);

    std::string response = "";
    if (res == CURLE_OK) {
        if (chunk.memory) {
            response = std::string(chunk.memory);
        }
    }
    else {
        std::cerr << "[CURL Error] " << curl_easy_strerror(res) << std::endl;
    }

    free(chunk.memory);
    return response;
}

std::string HttpClient::post(const std::string& url, const std::string& jsonData) {
    setupCommonOptions();

    struct MemoryStruct chunk;
    chunk.memory = (char*)malloc(1);
    chunk.size = 0;

    // 初始化防止垃圾記憶體
    if (chunk.memory) chunk.memory[0] = '\0';

    curl_easy_setopt(curl, CURLOPT_URL, url.c_str());
    curl_easy_setopt(curl, CURLOPT_POST, 1L);
    curl_easy_setopt(curl, CURLOPT_POSTFIELDS, jsonData.c_str());
    curl_easy_setopt(curl, CURLOPT_HTTPHEADER, headers);
    curl_easy_setopt(curl, CURLOPT_WRITEDATA, (void*)&chunk);

    CURLcode res = curl_easy_perform(curl);

    std::string response = "";
    if (res == CURLE_OK) {
        if (chunk.memory) {
            response = std::string(chunk.memory);
        }
    }
    else {
        std::cerr << "[CURL Error] " << curl_easy_strerror(res) << std::endl;
    }

    free(chunk.memory);
    return response;
}

std::string HttpClient::getCookie(const std::string& targetName) {
    struct curl_slist* cookies = NULL;
    struct curl_slist* nc = NULL;
    std::string result = "";

    if (curl) {
        curl_easy_getinfo(curl, CURLINFO_COOKIELIST, &cookies);
        nc = cookies;
        while (nc) {
            std::string line = nc->data;
            std::stringstream ss(line);
            std::string segment;
            std::vector<std::string> parts;

            while (std::getline(ss, segment, '\t')) {
                parts.push_back(segment);
            }

            // 確保欄位足夠 (通常第 6 欄是名稱, 第 7 欄是值)
            if (parts.size() >= 7) {
                if (parts[5] == targetName) {
                    result = parts[6];
                    break;
                }
            }
            nc = nc->next;
        }
        curl_slist_free_all(cookies);
    }
    return result;
}