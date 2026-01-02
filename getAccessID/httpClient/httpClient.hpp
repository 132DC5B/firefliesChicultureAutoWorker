#ifndef HTTP_CLIENT_HPP
#define HTTP_CLIENT_HPP

#include <string>
#include <vector>
#include <curl/curl.h>

struct MemoryStruct {
    char* memory;
    size_t size;
};

class HttpClient {
public:
    HttpClient();
    ~HttpClient();

    void addHeader(const std::string& header);

    std::string getCookie(const std::string& name);
    void setCookieFile(const std::string& filename);

    std::string get(const std::string& url);

    std::string post(const std::string& url, const std::string& jsonData);

private:
    CURL* curl;
    struct curl_slist* headers = NULL;

    std::string cookieFile;

    static size_t WriteMemoryCallback(void* contents, size_t size, size_t nmemb, void* userp);

    void setupCommonOptions();
};

#endif