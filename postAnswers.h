// ==========================================
// 輸出開關設定
// 若欲關閉輸出，請註釋下行（即加 // 於前）：
//
	#define ENABLE_OUTPUT 
//
// ==========================================



#ifdef ENABLE_OUTPUT
	#define LOG std::cout
#else
	#define LOG if(0) std::cout
#endif