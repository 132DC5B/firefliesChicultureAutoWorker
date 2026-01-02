#ifndef CONFIG_H
#define CONFIG_H

#include <iostream>

// ============================================================================
//									功能開關設定
// ============================================================================

// 1. 主程式 (postAnswers.cpp) 輸出開關
	#define ENABLE_APP_LOG

// 2. 網路模組 (httpClient.cpp) 輸出開關
//	#define ENABLE_CURL_LOG

// 3. 是否執行額外閱讀 (Extra Read) 提交 (+10分每文章)
	#define ENABLE_EXTRA_READ


// ============================================================================
//									宏定義邏輯
// ============================================================================

#ifdef ENABLE_APP_LOG
	#define LOG std::cout
#else
	#define LOG if(0) std::cout
#endif

#endif // CONFIG_H