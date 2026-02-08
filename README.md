# FirefliesChicultureAutoWorker

本專案是一個針對「篇篇流螢」平台 (fireflies.chiculture.org.hk) 的自動化解決方案。專案採用 **分離式架構** 設計，由 VB.NET 構建的圖形介面 (GUI) 作為前端，調用底層高效能的 C++ 命令列工具執行實際的 HTTP 請求與邏輯處理。

## 技術堆疊

### 前端
- **語言**: Visual Basic .NET (VB.NET)
- **框架**: .NET Framework 4.8 (Windows Forms)
- **功能**:
  - 使用者憑證管理
  - 任務排程與進程調用
  - 檔案監控 (`FileSystemWatcher` 監控 `date.txt`)

### 核心邏輯
- **語言**: C++ (C++14 Standard)
- **依賴庫**:
  - **libcurl**: 用於處理所有 HTTP/HTTPS 請求 (且需支援 SSL/TLS)。
  - **cJSON**: 用於解析與生成 JSON 數據。
  - **Windows API**: 用於控制台輸出編碼 (`SetConsoleOutputCP`) 與檔案操作。
  - **Standard Library**: 使用 `<future>` 與 `<thread>` 實現高並發處理。

---

## 系統架構

本系統採用 **IPC (行程間通訊)** 模式，透過**檔案**與**命令列參數**進行互動。

### 1. 通訊協議
*   **輸入**: GUI 透過命令列參數啟動 C++ 程式。
*   **狀態共享**: 透過工作目錄下的文字檔共享數據。
    *   `cookies_student.txt`: 存放學生 Session Cookie。
    *   `cookies_admin.txt`: 存放管理員 Session Cookie (用於獲取正確答案)。
    *   `date.txt`: 存放待處理的日期列表。
*   **輸出**: C++ 程式將執行結果輸出至 StdOut，GUI 可選擇性截取或僅顯示狀態。

### 2. 模組職責詳解

#### A. `getAccessID.exe` (身份驗證模組)
負責處理登入並獲取 Cookie。

| 函數/方法 | 參數 | 描述 |
| :--- | :--- | :--- |
| **`main`** | `argc`, `argv` | 程式入口。解析參數 `-em` (Email), `-pw` (密碼), `-a`/`-s` (角色)。初始化 `HttpClient` 並執行登入請求。 |
| **`print_usage`** | `progName` | 當參數錯誤時，印出正確的使用方法說明。 |
| **`getExeDir`** | 無 | 透過 Windows API `GetModuleFileNameA` 獲取當前執行檔所在的目錄路徑，確保 Cookie 檔案寫入正確位置。 |

#### B. `firefliesChicultureAutoWorker.exe` (核心任務模組)
負責實際的答題與閱讀任務執行。

##### 1. 主流程控制 (`src/main/postAnswers.cpp`)

| 函數/方法 | 參數 | 描述 |
| :--- | :--- | :--- |
| **`main`** | `argc`, `argv` | 1. 檢查 `cookies_student.txt` 是否存在且有效。<br>2. 解析參數 `-f` (讀檔), `-nf` (讀參數), `-a` (隨機), `-e` (停用額外閱讀)。<br>3. 建立 `std::vector<std::future<void>>` 執行緒池，並行啟動多個 `processTask`。 |
| **`processTask`** | `index`, `total` (進度顯示)<br>`TARGET_DATE` (目標日期)<br>`randomMode` (隨機)<br>`extraRead` (閱讀) | **核心工作單元**。負責處理**單一日期**的所有流程：<br>1. 呼叫 `fetchAssignmentId`。<br>2. 根據模式呼叫 `fetchAnswers` 或 `fetchQuestions`。<br>3. 呼叫 `submitAnswers`。<br>4. 若成功且啟用，呼叫 `submitExtraRead`。<br>此函數內使用 `std::mutex` 鎖保護 Log 輸出。 |
| **`split_dates`** | `s` (逗號分隔字串) | 將命令列傳入的日期字串 (e.g., "2023-01-01,2023-01-02") 分割為 `vector<string>`。 |

##### 2. 任務邏輯 (`src/main/assignment.cpp`, `answers.cpp`)

| 函數/方法 | 參數 | 描述 |
| :--- | :--- | :--- |
| **`fetchAssignmentId`** | `date`<br>`&assignmentId` (out)<br>`&level` (out) | **GET** `/api/quiz/assignments/{date}`<br>查詢該日期對應的作業 ID 與等級 (Level)。若找不到則回傳 `false`。 |
| **`fetchAnswers`** | `assignmentId`<br>`level`<br>`&finalInfo` (out) | **GET** (Admin) `/api/quiz/assignments/{id}`<br>**需要 Admin Cookie**。直接從後台獲取正確答案 (`correctAnswer`) 並填入 `finalInfo` 結構。 |
| **`fetchQuestions`** | `assignmentId`<br>`level`<br>`&finalInfo` (out) | **GET** (Admin) `/api/quiz/assignments/{id}`<br>**隨機模式**。同樣查詢題目結構，但不讀取正確答案，而是使用 `std::random_device` 隨機生成 0-3 的選項。 |
| **`submitAnswers`** | `finalInfo` (含題目與答案)<br>`date` | **POST** `/api/quiz/answers`<br>構造提交用的 JSON Payload。包含 `assignmentId`, `lv`, 以及 `answers` 陣列。檢查回傳是否包含 `score` 或 `correct` 判定成功。 |
| **`submitExtraRead`** | `finalInfo` | **POST** `/api/quiz/answers/extra-read`<br>構造額外閱讀任務的 Payload。 |

#### C. `FirefliesGUI` (VB.NET 前端)

| 函數/方法 (.vb) | 描述 |
| :--- | :--- |
| **`CallGetAccessID`** | 封裝 `Process.Start` 調用 C++ `getAccessID.exe`。傳入帳號密碼，並同步等待 (WaitForExit)，回傳 Exit Code (0 為成功)。 |
| **`UpdateUIState`** | 檢查本地 `cookies_student.txt` 與 `cookies_admin.txt`。根據 Cookie 的存在與否，啟用/停用「Start Task」按鈕與「Random Answers」勾選框 (若無 Admin Cookie 則強制勾選隨機)。 |
| **`InitDateFileWatcher`** | 初始化 `FileSystemWatcher`，監控 `date.txt` 的變更。若外部編輯器修改了檔案，GUI 會自動重載內容。 |
| **`rtbDateList_TextChanged`** | 當使用者在 GUI 文字框打字時，自動將內容回寫至 `date.txt`，實現雙向綁定。 |

---



## 開發與建置

### 1. 環境需求
*   **IDE**: Visual Studio 2022
*   **Toolsets**:
    *   Desktop development with C++
    *   .NET desktop development
*   **Runtime**: Windows 10/11 (因使用了 WinAPI)

>[!WARNING]
> 需確保 `libcurl.dll` (及 SSL 相關 dll 如 `libssl-1_1-x64.dll`, `libcrypto-1_1-x64.dll`) 存在於編譯輸出的執行檔目錄中。

### 2. 編譯步驟
1.  開啟 `firefliesChicultureAutoWorker.sln`。
2.  **設定組態**: 選擇 `Release` / `x64` (為了效能與相容性)。
3.  **建置順序**:
    *   先建置 `getAccessID`。
    *   再建置 `firefliesChicultureAutoWorker`。
    *   最後建置 `firefliesGUI`。
4.  **部署**: 將所有生成的 `.exe` 與依賴的 `.dll` 檔案集中於同一目錄。

---

## 除錯

*   **Log 開關**: 在 `src/config.h` 中定義了 `ENABLE_APP_LOG` 與 `ENABLE_CURL_LOG`。
    *   開啟 `ENABLE_CURL_LOG` 可在控制台看到詳細的 HTTP Header 與 Payload 傳輸過程 (libcurl verbose mode)。


# 授權

MIT License (詳見 `LICENSE.txt`)
