# firefliesChicultureAutoWorker

> [!TIP]
> 此專案為一組用於 `fireflies.chiculture.org.hk` 平台的自動化工具，採用 C++（C++14）與一個 Windows Forms GUI（.NET Framework4.8）。
目標是：以可重複、可維護的方式模擬使用者行為——登入、抓取作業題目與正解並以學生身份提交答案。
此README說明專案結構、關鍵元件、建置與除錯方法...

---


# 專案概覽

- 名稱：`firefliesChicultureAutoWorker`

- 目的：自動化登入、讀取作業（assignment）、取得正確答案（以 admin cookie）並以學生身份提交答案。

- 技術：C++ (C++14)、cJSON、platform-specific Windows API、以及一個選配的 Windows Forms GUI（.NET Framework4.8）。

---

# 原始碼架構與說明

- `src/getAccessID/getAccessID.cpp`：從 `acc.txt`讀取 email/password，對 `https://fireflies.chiculture.org.hk/api/core/auth`進行登入請求，並把 cookie 寫入 `cookies_*.txt`。

-  `src/postAnswers.cpp`：主程式流程，依序：
1. 讀取 `date.txt`（由 `src/date`解析，支援範圍與跳過週末）
2. 以學生 cookie 呼叫 `GET /api/quiz/assignments/{date}`取得 `assignmentId`
3. 以管理員 cookie 呼叫 `GET /api/quiz/assignments/{assignmentId}`取得題目與正確答案
4. 以學生 cookie 呼叫 `POST /api/quiz/answers` 提交答案
5. （選配）若啟用 `ENABLE_EXTRA_READ`，會再呼叫 `POST /api/quiz/answers/extra-read`。

- `src/date`：日期解析相關程式（`date.h`/`date.cpp`），負責把 `date.txt` 中的條目展開為實際處理的日期清單，並自動排除週末。
- `src/httpClient`：專案自有的 HTTP 客戶端封裝（`httpClient.hpp` 與實作），負責 Header、Cookie 檔案、GET/POST 等方法。若改成 libcurl 或 WinHTTP，請保持相容介面或更新呼叫處。
- `src/cJSON`：內建的 JSON 處理器（`cJSON.h` / `cJSON.c`），用於解析與產生 JSON。
- `src/firefliesGUI`：Windows Forms GUI 專案，目標為 .NET Framework4.8，可視化管理 cookie 與日期，非自動化核心但可輔助操作。
- `config.h`：[config.h](#設定與常見選項configh)

---

# 開發與建置環境

- 作業系統：Windows10/11
- 編譯器：MSVC (Visual Studio2019/2022)，C++14 模式
- .NET：若要編譯 GUI，需要安裝 .NET Framework4.8 SDK


---

# 執行範例與測試資料

## 測試檔案範例：

- `acc.txt`（一個帳號/密碼檔）：
```
student@example.com
S3cretP@ss
```
- `date.txt` 支援格式：
1. 單日：
```
2026-01-05
```
2. 多行：
```
2026-01-05
2026-01-07
2026-01-10
 ```
3. 範圍：（會展開並跳過週末）
 ```
 2026-01-05/2026-01-10
```

## 執行流程示例（手動）：
1. 以學生帳號產生 `cookies_student.txt`：執行 `getAccessID`，確認 `access.id` 已寫入 cookie 檔。
2. 以管理員帳號產生 `cookies_admin.txt`：可用相同 `getAccessID` 程式（或修改參數）取得管理員 cookie。
3. 放好 `date.txt`，執行 `firefliesChicultureAutoWorker.exe`。程式會顯示每個處理日期的進度與成功/失敗訊息。

### 輸出檔案：
- `cookies_student.txt`、`cookies_admin.txt`：cookie Jar文字檔
- 日誌輸出使用 `LOG << ...`（請參考 `config.h` 中的 LOG 定義）



## 設定與常見選項（`config.h`）

### `config.h` 中包含：

- `#define LOG std::cout` : 定義日誌輸出位置
- `#define ENABLE_CURL_LOG`：啟用 libcurl 詳細日誌
- `#define ENABLE_EXTRA_READ`：啟用額外閱讀任務提交

---

# 風險、合規與安全

> [!WARNING]
> 使用本工具時，請確認擁有相關帳號與系統使用權限。未經授權的自動化可能違反服務條款或法律。

> [!CAUTION]
> 不要在公開 repo 中提交包含真實密碼的 `acc.txt` 與 cookie 檔。

> [!CAUTION]
> 本案不對任何因濫用造成的後果負責。


---

# 授權

本專案採用 MIT License，[見 `LICENSE.txt`](LICENSE.txt)。

---
