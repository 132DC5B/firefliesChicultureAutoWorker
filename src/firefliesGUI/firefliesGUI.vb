Imports System.IO

Public Class firefliesGUI
    Private watcher As FileSystemWatcher
    Private isInternalUpdate As Boolean = False

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitDateFileWatcher()
        LoadDateFileToRichTextBox()
        UpdateUIState()
    End Sub

    Private Sub InitDateFileWatcher()
        Dim path As String = "date.txt"
        If Not IO.File.Exists(path) Then
            IO.File.WriteAllText(path, "")
        End If
        watcher = New FileSystemWatcher()
        watcher.Path = IO.Path.GetDirectoryName(IO.Path.GetFullPath(path))
        watcher.Filter = IO.Path.GetFileName(path)
        watcher.NotifyFilter = NotifyFilters.LastWrite Or NotifyFilters.FileName
        AddHandler watcher.Changed, AddressOf OnDateFileChanged
        AddHandler watcher.Created, AddressOf OnDateFileChanged
        AddHandler watcher.Renamed, AddressOf OnDateFileChanged
        watcher.EnableRaisingEvents = True
    End Sub

    Private Sub UpdateUIState()
        ' Check Student Cookie
        Dim studentCookieFile As String = "cookies_student.txt"
        Dim hasStudentCookie As Boolean = False
        If File.Exists(studentCookieFile) AndAlso File.ReadAllText(studentCookieFile).Contains("access.id") Then
            hasStudentCookie = True
        End If

        ' Check Admin Cookie
        Dim adminCookieFile As String = "cookies_admin.txt"
        Dim hasAdminCookie As Boolean = False
        If File.Exists(adminCookieFile) AndAlso File.ReadAllText(adminCookieFile).Contains("access.id") Then
            hasAdminCookie = True
        End If

        ' Update Start Task Button behavior
        ' Must have Student cookie to submit answers.
        If hasStudentCookie Then
            btnStartTask.Enabled = True
        Else
            btnStartTask.Enabled = False
        End If

        ' Update Random Answers Checkbox behavior
        ' If Admin cookie exists, we can fetch answers (Random optional).
        ' If Admin cookie missing, we MUST use Random (Random forced).
        If hasAdminCookie Then
            chkRandomAnswers.Enabled = True
        Else
            chkRandomAnswers.Checked = True
            chkRandomAnswers.Enabled = False
        End If
    End Sub

    Private Sub OnDateFileChanged(sender As Object, e As FileSystemEventArgs)
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(AddressOf LoadDateFileToRichTextBox))
        Else
            LoadDateFileToRichTextBox()
        End If
    End Sub

    Private Sub rtbDateList_TextChanged(sender As Object, e As EventArgs) Handles rtbDateList.TextChanged
        If isInternalUpdate Then Return
        Try
            watcher.EnableRaisingEvents = False
            IO.File.WriteAllText("date.txt", rtbDateList.Text)
        Catch ex As Exception
            '
        Finally
            watcher.EnableRaisingEvents = True
        End Try
    End Sub

    Private Sub LoadDateFileToRichTextBox()
        Dim path As String = "date.txt"
        If Not IO.File.Exists(path) Then
            IO.File.WriteAllText(path, "")
        End If
        Dim success As Boolean = False
        For i As Integer = 1 To 5
            Try
                isInternalUpdate = True
                Using fs As New FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                    Using sr As New StreamReader(fs)
                        rtbDateList.Text = sr.ReadToEnd()
                    End Using
                End Using
                success = True
                Exit For
            Catch ex As IOException
                Threading.Thread.Sleep(50)
            Finally
                isInternalUpdate = False
            End Try
        Next
        If Not success Then
            rtbDateList.Text = "Error loading date.txt: The file is locked by another process."
        End If
    End Sub

    Private Function CallGetAccessID(email As String, password As String, isAdmin As Boolean) As Integer
        Try
            Dim p As New Process()
            p.StartInfo.FileName = "getAccessID.exe"
            Dim roleArg As String = If(isAdmin, "-a", "-s")
            p.StartInfo.Arguments = $"-em ""{email}"" -pw ""{password}"" {roleArg}"
            p.StartInfo.UseShellExecute = False
            p.StartInfo.CreateNoWindow = True
            p.Start()
            p.WaitForExit()
            Return p.ExitCode
        Catch ex As Exception
            MessageBox.Show("Failed to call API: " & ex.Message)
            Return -1
        End Try
    End Function

    Private Sub btnStudentLogin_Click(sender As Object, e As EventArgs) Handles btnStudentLogin.Click
        Dim email As String = txtStudentEmail.Text
        Dim password As String = txtStudentPassword.Text
        If String.IsNullOrWhiteSpace(email) OrElse String.IsNullOrWhiteSpace(password) Then
            MessageBox.Show("Please enter email and password.")
            Return
        End If

        tsStatusLabel.Text = "Logging in..."
        tsProgressBar.Value = 30
        Application.DoEvents()

        Dim exitCode As Integer = CallGetAccessID(email, password, False)

        If exitCode = 0 Then
            tsStatusLabel.Text = "Login successful! (Exit Code: " & exitCode & ")"
            tsProgressBar.Value = 100
        Else
            tsStatusLabel.Text = "Login failed (Exit Code: " & exitCode & ")"
            tsProgressBar.Value = 0
        End If

        UpdateUIState()
    End Sub

    Private Sub btnAdminLogin_Click(sender As Object, e As EventArgs) Handles btnAdminLogin.Click
        Dim email As String = txtAdminEmail.Text
        Dim password As String = txtAdminPassword.Text
        If String.IsNullOrWhiteSpace(email) OrElse String.IsNullOrWhiteSpace(password) Then
            MessageBox.Show("Please enter email and password.")
            Return
        End If

        tsStatusLabel.Text = "Logging in as Admin..."
        tsProgressBar.Value = 30
        Application.DoEvents()

        Dim exitCode As Integer = CallGetAccessID(email, password, True)

        If exitCode = 0 Then
            tsStatusLabel.Text = "Admin Login successful! (Exit Code: " & exitCode & ")"
            tsProgressBar.Value = 100
        Else
            tsStatusLabel.Text = "Admin Login failed (Exit Code: " & exitCode & ")"
            tsProgressBar.Value = 0
        End If

        UpdateUIState()
    End Sub

    Private Sub btnStartTask_Click(sender As Object, e As EventArgs) Handles btnStartTask.Click
        Dim args As String = "-f"
        If chkRandomAnswers.Checked Then
            args &= " -a"
        End If
        If chkDisableExtraRead.Checked Then
            args &= " -e"
        End If

        Try
            Dim p As New Process()
            p.StartInfo.FileName = "firefliesChicultureAutoWorker.exe"
            p.StartInfo.Arguments = args
            p.StartInfo.UseShellExecute = True
            p.Start()
            tsStatusLabel.Text = "Task started..."
        Catch ex As Exception
            MessageBox.Show("Failed to start task: " & ex.Message)
        End Try
    End Sub
End Class
