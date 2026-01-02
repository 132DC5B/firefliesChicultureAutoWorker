// ==========================================
// 輸出開關設定
// ==========================================
// 若欲關閉輸出，請註釋下行（即加 // 於前）：
#define ENABLE_OUTPUT 

#ifdef ENABLE_OUTPUT
    // 開啟時，LOG 即為 std::cout
#define LOG std::cout
#else
    // 關閉時，LOG 視為假判斷，不執行後續輸出
#define LOG if(0) std::cout
#endif