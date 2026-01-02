#include <iostream>
#include <string>
#include <vector>
#include <fstream>
#include <algorithm>
#include "../httpClient/httpClient.hpp"
#include "../cJSON/cJSON.h"

const std::string ACC_FILE = "acc.txt";
const std::string TARGET_URL = "https://fireflies.chiculture.org.hk/api/core/auth";

std::string trim(const std::string& str) {
    size_t first = str.find_first_not_of(" \t\n\r");
    if (std::string::npos == first) {
        return str;
    }
    size_t last = str.find_last_not_of(" \t\n\r");
    return str.substr(first, (last - first + 1));
}

int main() {
    std::ifstream file(ACC_FILE);
    std::string user_email;
    std::string user_passwd;

    if (file.is_open()) {
        if (std::getline(file, user_email)) {
            user_email = trim(user_email);
        }
        if (std::getline(file, user_passwd)) {
            user_passwd = trim(user_passwd);
        }
        file.close();
    }
    else {
        // [Error] Unable to open acc.txt, please confirm file exists.
        std::cout << "[Error] Unable to open " << ACC_FILE << ". Please check if the file exists." << std::endl;
        return 1;
    }

    if (user_email.empty() || user_passwd.empty()) {
        // [Error] Account or password is empty.
        std::cout << "[Error] Email or password is empty. Please check content in " << ACC_FILE << "." << std::endl;
        return 1;
    }

    // [getAccessID] Reading account...
    std::cout << "[getAccessID] Reading account: " << user_email << std::endl;

    HttpClient client;

    client.setCookieFile("cookies_student.txt");

    // [getAccessID] Logging in...
    std::cout << "[getAccessID] Logging in..." << std::endl;

    client.addHeader("Accept-Encoding: identity");
    client.addHeader("accept: application/json, text/plain, */*");
    client.addHeader("content-type: application/json;charset=UTF-8");
    client.addHeader("origin: https://fireflies.chiculture.org.hk");
    client.addHeader("referer: https://fireflies.chiculture.org.hk/");
    client.addHeader("sec-ch-ua: \"Google Chrome\";v=\"143\", \"Chromium\";v=\"143\", \"Not A(Brand\";v=\"24\"");
    client.addHeader("sec-ch-ua-mobile: ?0");
    client.addHeader("sec-ch-ua-platform: \"Windows\"");
    client.addHeader("priority: u=1, i");

    cJSON* root = cJSON_CreateObject();
    cJSON_AddStringToObject(root, "email", user_email.c_str());
    cJSON_AddStringToObject(root, "password", user_passwd.c_str());
    cJSON_AddNumberToObject(root, "web", 1);
    cJSON_AddBoolToObject(root, "persist", cJSON_True);

    char* jsonPayload = cJSON_PrintUnformatted(root);

    std::string response = client.post(TARGET_URL, std::string(jsonPayload));

    cJSON_Delete(root);
    free(jsonPayload);

    std::string accessId = client.getCookie("access.id");

    if (!accessId.empty()) {
        std::cout << "\n========================================" << std::endl;
        std::cout << "[Success] Login successful!" << std::endl;
        std::cout << "[Access ID] " << accessId.substr(0, 20) << "..." << " ()" << std::endl;
        std::cout << "[Cookie] Automatically saved to cookies_student.txt." << std::endl;
        std::cout << "========================================" << std::endl;
    }
    else {
        std::cout << "[Failed] Request completed, but access.id Cookie not found." << std::endl;
        std::cout << "Response length: " << response.length() << std::endl;
        //std::cout << "Server response: " << response << std::endl;
    }

    return 0;
}