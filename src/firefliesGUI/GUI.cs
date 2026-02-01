using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace firefliesGUI
{
    public partial class GUI : Form
    {
        // ==========================================
        // File Name Settings
        // ==========================================
        const string LOGIN_STUDENT_EXE = "getAccessID.exe";       // Student login tool
        const string LOGIN_ADMIN_EXE = "getAccessID_admin.exe";   // Admin login tool
        const string WORKER_EXE = "firefliesChicultureAutoWorker.exe";

        const string ACC_FILE = "acc.txt";
        const string DATE_FILE = "date.txt";

        // Files to check for success (Optional)
        const string COOKIE_STUDENT = "cookies_student.txt";
        const string COOKIE_ADMIN = "cookies_admin.txt";

        // Variable to track the currently running external process
        private Process _runningProcess = null;

        public GUI()
        {
            InitializeComponent();

            // Bind button events
            this.getStudentsAccessID.Click += new EventHandler(this.btnGetStudent_Click);
            this.getAdminsAccessID.Click += new EventHandler(this.btnGetAdmin_Click);
            this.startTask.Click += new EventHandler(this.btnStartTask_Click);

            // Bind FormClosing event to kill background processes when GUI closes
            this.FormClosing += new FormClosingEventHandler(this.MainForm_FormClosing);

            Log("[System] GUI initialized. Waiting for operation...");
        }

        // ==========================================
        // Event: Form Closing
        // Logic: Force kill any running external process (like the worker exe)
        // ==========================================
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_runningProcess != null && !_runningProcess.HasExited)
            {
                try
                {
                    _runningProcess.Kill(); // Force kill
                }
                catch (Exception)
                {
                    // Ignore errors if process is already dead
                }
            }
        }

        // ==========================================
        // Button 1: Get Student Access ID
        // Logic: Write to acc.txt -> Call getAccessID.exe
        // ==========================================
        private async void btnGetStudent_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            // 1. Write input to acc.txt
            SaveCredentials();

            ToggleUI(false);
            Log($"[Student Login] Writing credentials and calling {LOGIN_STUDENT_EXE}...");

            // 2. Execute student login tool
            await RunProcessAsync(LOGIN_STUDENT_EXE);

            // 3. Simple success check
            if (File.Exists(COOKIE_STUDENT))
                Log("[Success] Student Cookie updated.");
            else
                Log("[Tip] Execution finished (Cookie file not detected).");

            ToggleUI(true);
        }

        // ==========================================
        // Button 2: Get Admin Access ID
        // Logic: Write to acc.txt -> Call getAccessID_admin.exe
        // ==========================================
        private async void btnGetAdmin_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            // 1. Write input to acc.txt
            SaveCredentials();

            ToggleUI(false);
            Log($"[Admin Login] Writing credentials and calling {LOGIN_ADMIN_EXE}...");

            // 2. Execute admin login tool
            await RunProcessAsync(LOGIN_ADMIN_EXE);

            // 3. Simple success check
            if (File.Exists(COOKIE_ADMIN))
                Log("[Success] Admin Cookie updated.");
            else
                Log("[Tip] Execution finished (Cookie file not detected).");

            ToggleUI(true);
        }

        // ==========================================
        // Button 3: Start Task
        // ==========================================
        private async void btnStartTask_Click(object sender, EventArgs e)
        {
            string dateContent = txtDateRange.Text;

            if (string.IsNullOrWhiteSpace(dateContent))
            {
                MessageBox.Show("Please enter Date Range.", "Missing Field");
                return;
            }

            ToggleUI(false);
            Log("=== Task Started ===");

            // 1. Write date settings
            try
            {
                File.WriteAllText(DATE_FILE, dateContent, Encoding.UTF8);
                Log($"Date settings updated ({txtDateRange.Lines.Length} lines).");
            }
            catch (Exception ex)
            {
                Log($"[Error] Failed to write date.txt: {ex.Message}");
                ToggleUI(true);
                return;
            }

            // 2. Execute worker process
            await RunProcessAsync(WORKER_EXE);

            Log("=== Execution Finished ===");
            ToggleUI(true);
        }

        // ==========================================
        // Core Functions & Helpers
        // ==========================================

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter Email and Password.", "Missing Fields");
                return false;
            }
            return true;
        }

        private void SaveCredentials()
        {
            try
            {
                // Format: Account on line 1, Password on line 2
                string content = $"{txtEmail.Text.Trim()}\r\n{txtPassword.Text.Trim()}";
                File.WriteAllText(ACC_FILE, content, Encoding.UTF8);
                Log("[System] Credentials saved to acc.txt.");
            }
            catch (Exception ex)
            {
                Log($"[Error] Failed to write acc.txt: {ex.Message}");
            }
        }

        // Helper: Run external process asynchronously
        private async Task RunProcessAsync(string exeName)
        {
            if (!File.Exists(exeName))
            {
                Log($"[Critical Error] File not found: {exeName}");
                return;
            }

            await Task.Run(() =>
            {
                try
                {
                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = exeName,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true,
                        StandardOutputEncoding = Encoding.UTF8
                    };

                    using (Process proc = new Process())
                    {
                        proc.StartInfo = psi;

                        // Capture output
                        proc.OutputDataReceived += (s, ev) => { if (ev.Data != null) Log(ev.Data); };
                        proc.ErrorDataReceived += (s, ev) => { if (ev.Data != null) Log("[Error] " + ev.Data); };

                        // Track process so we can kill it if GUI closes
                        _runningProcess = proc;

                        proc.Start();
                        proc.BeginOutputReadLine();
                        proc.BeginErrorReadLine();
                        proc.WaitForExit();
                    }
                }
                catch (Exception ex)
                {
                    Log($"[Execution Exception] {ex.Message}");
                }
                finally
                {
                    // Clear the tracker when process ends
                    _runningProcess = null;
                }
            });
        }

        // Helper: Log message to RichTextBox (Thread-Safe)
        private void Log(string msg)
        {
            if (richTextBox1.InvokeRequired)
            {
                try
                {
                    richTextBox1.Invoke(new Action<string>(Log), msg);
                }
                catch
                {
                    // Prevent crash if window is closing
                }
            }
            else
            {
                if (richTextBox1.IsDisposed) return;

                // Color coding for better visibility
                if (msg.Contains("[Error]") || msg.Contains("Failed") || msg.Contains("Exception"))
                    richTextBox1.SelectionColor = System.Drawing.Color.Red;
                else if (msg.Contains("[Success]") || msg.Contains("[System]") || msg.Contains("Success"))
                    richTextBox1.SelectionColor = System.Drawing.Color.LightGreen;
                else if (msg.Contains("[Warning]") || msg.Contains("Warning"))
                    richTextBox1.SelectionColor = System.Drawing.Color.Yellow;
                else
                    richTextBox1.SelectionColor = System.Drawing.Color.White;

                richTextBox1.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}\r\n");
                richTextBox1.ScrollToCaret();
            }
        }

        // Helper: Lock or Unlock UI elements
        private void ToggleUI(bool enable)
        {
            if (InvokeRequired)
            {
                try { Invoke(new Action<bool>(ToggleUI), enable); } catch { }
            }
            else
            {
                getStudentsAccessID.Enabled = enable;
                getAdminsAccessID.Enabled = enable;
                startTask.Enabled = enable;
                txtEmail.Enabled = enable;
                txtPassword.Enabled = enable;
                txtDateRange.Enabled = enable;
            }
        }

        // Empty event handlers (Keep these to avoid Designer errors)
        private void mainTable2_Paint(object sender, PaintEventArgs e) { }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) { }
        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e) { }
    }
}