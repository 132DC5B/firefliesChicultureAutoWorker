#ifndef HTTP_CLIENT_HPP
#define HTTP_CLIENT_HPP

#include <string>
#include <vector>
#include <curl/curl.h> // 需確保 Visual Studio 已配置 libcurl include 目錄

// 定義回調函數之緩衝結構
struct MemoryStruct {
    char* memory;
    size_t size;
};

class HttpClient {
public:
    HttpClient();
    ~HttpClient();

    // 增加 Header
    void addHeader(const std::string& header);

    std::string getCookie(const std::string& name);
    void setCookieFile(const std::string& filename);

    // 執行 GET 請求
    std::string get(const std::string& url);

    // 執行 POST 請求
    std::string post(const std::string& url, const std::string& jsonData);

private:
    CURL* curl;
    struct curl_slist* headers = NULL;

    std::string cookieFile;

    // 寫入回調函數
    static size_t WriteMemoryCallback(void* contents, size_t size, size_t nmemb, void* userp);

    // 初始化通用選項
    void setupCommonOptions();
};

#endif