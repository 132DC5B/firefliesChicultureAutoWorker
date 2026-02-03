#pragma once
#define GETACCESSID_EXPORTS
#ifdef GETACCESSID_EXPORTS
#define GETACCESSID_API __declspec(dllexport)
#else
#define GETACCESSID_API __declspec(dllimport)
#endif

extern "C" {
 // 回傳 true 表示成功，false 表示失敗
 GETACCESSID_API bool LoginAndSaveCookie(const char* email, const char* password, const char* cookieFile);
}
