<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class firefliesGUI
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.tlpLoginContainer = New System.Windows.Forms.TableLayoutPanel()
        Me.tlpAdminLogin = New System.Windows.Forms.TableLayoutPanel()
        Me.lblAdminEmail = New System.Windows.Forms.Label()
        Me.lblAdminPassword = New System.Windows.Forms.Label()
        Me.txtAdminEmail = New System.Windows.Forms.TextBox()
        Me.txtAdminPassword = New System.Windows.Forms.TextBox()
        Me.btnAdminLogin = New System.Windows.Forms.Button()
        Me.tlpStudentLogin = New System.Windows.Forms.TableLayoutPanel()
        Me.lblStudentEmail = New System.Windows.Forms.Label()
        Me.lblStudentPassword = New System.Windows.Forms.Label()
        Me.txtStudentEmail = New System.Windows.Forms.TextBox()
        Me.txtStudentPassword = New System.Windows.Forms.TextBox()
        Me.btnStudentLogin = New System.Windows.Forms.Button()
        Me.lblShow = New System.Windows.Forms.Label()
        Me.tlpDateFile = New System.Windows.Forms.TableLayoutPanel()
        Me.lblDateFileHeader = New System.Windows.Forms.Label()
        Me.rtbDateList = New System.Windows.Forms.RichTextBox()
        Me.tlpActions = New System.Windows.Forms.TableLayoutPanel()
        Me.chkRandomAnswers = New System.Windows.Forms.CheckBox()
        Me.chkDisableExtraRead = New System.Windows.Forms.CheckBox()
        Me.btnStartTask = New System.Windows.Forms.Button()
        Me.ssMain = New System.Windows.Forms.StatusStrip()
        Me.tsStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsProgressBar = New System.Windows.Forms.ToolStripProgressBar()
        Me.tlpMain.SuspendLayout()
        Me.tlpLoginContainer.SuspendLayout()
        Me.tlpAdminLogin.SuspendLayout()
        Me.tlpStudentLogin.SuspendLayout()
        Me.tlpDateFile.SuspendLayout()
        Me.tlpActions.SuspendLayout()
        Me.ssMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.ColumnCount = 2
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.64484!))
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.35516!))
        Me.tlpMain.Controls.Add(Me.tlpLoginContainer, 0, 0)
        Me.tlpMain.Controls.Add(Me.tlpDateFile, 0, 1)
        Me.tlpMain.Controls.Add(Me.tlpActions, 1, 0)
        Me.tlpMain.Location = New System.Drawing.Point(0, 2)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 2
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpMain.Size = New System.Drawing.Size(794, 423)
        Me.tlpMain.TabIndex = 0
        '
        'tlpLoginContainer
        '
        Me.tlpLoginContainer.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.tlpLoginContainer.ColumnCount = 2
        Me.tlpLoginContainer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpLoginContainer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpLoginContainer.Controls.Add(Me.tlpAdminLogin, 1, 0)
        Me.tlpLoginContainer.Controls.Add(Me.tlpStudentLogin, 0, 0)
        Me.tlpLoginContainer.Controls.Add(Me.lblShow, 0, 1)
        Me.tlpLoginContainer.Location = New System.Drawing.Point(3, 3)
        Me.tlpLoginContainer.Name = "tlpLoginContainer"
        Me.tlpLoginContainer.RowCount = 2
        Me.tlpLoginContainer.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 78.77358!))
        Me.tlpLoginContainer.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.22642!))
        Me.tlpLoginContainer.Size = New System.Drawing.Size(411, 205)
        Me.tlpLoginContainer.TabIndex = 0
        '
        'tlpAdminLogin
        '
        Me.tlpAdminLogin.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.tlpAdminLogin.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.tlpAdminLogin.ColumnCount = 2
        Me.tlpAdminLogin.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.38624!))
        Me.tlpAdminLogin.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.61376!))
        Me.tlpAdminLogin.Controls.Add(Me.lblAdminEmail, 0, 0)
        Me.tlpAdminLogin.Controls.Add(Me.lblAdminPassword, 0, 1)
        Me.tlpAdminLogin.Controls.Add(Me.txtAdminEmail, 1, 0)
        Me.tlpAdminLogin.Controls.Add(Me.txtAdminPassword, 1, 1)
        Me.tlpAdminLogin.Controls.Add(Me.btnAdminLogin, 1, 2)
        Me.tlpAdminLogin.Location = New System.Drawing.Point(209, 45)
        Me.tlpAdminLogin.Name = "tlpAdminLogin"
        Me.tlpAdminLogin.RowCount = 3
        Me.tlpAdminLogin.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.06593!))
        Me.tlpAdminLogin.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.76923!))
        Me.tlpAdminLogin.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.84615!))
        Me.tlpAdminLogin.Size = New System.Drawing.Size(198, 112)
        Me.tlpAdminLogin.TabIndex = 2
        '
        'lblAdminEmail
        '
        Me.lblAdminEmail.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblAdminEmail.AutoSize = True
        Me.lblAdminEmail.Location = New System.Drawing.Point(22, 13)
        Me.lblAdminEmail.Name = "lblAdminEmail"
        Me.lblAdminEmail.Size = New System.Drawing.Size(60, 13)
        Me.lblAdminEmail.TabIndex = 0
        Me.lblAdminEmail.Text = "adminEmail"
        '
        'lblAdminPassword
        '
        Me.lblAdminPassword.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblAdminPassword.AutoSize = True
        Me.lblAdminPassword.Location = New System.Drawing.Point(10, 49)
        Me.lblAdminPassword.Name = "lblAdminPassword"
        Me.lblAdminPassword.Size = New System.Drawing.Size(72, 13)
        Me.lblAdminPassword.TabIndex = 1
        Me.lblAdminPassword.Text = "adminPasswd"
        '
        'txtAdminEmail
        '
        Me.txtAdminEmail.Location = New System.Drawing.Point(89, 4)
        Me.txtAdminEmail.Name = "txtAdminEmail"
        Me.txtAdminEmail.Size = New System.Drawing.Size(105, 20)
        Me.txtAdminEmail.TabIndex = 3
        '
        'txtAdminPassword
        '
        Me.txtAdminPassword.Location = New System.Drawing.Point(89, 42)
        Me.txtAdminPassword.Name = "txtAdminPassword"
        Me.txtAdminPassword.Size = New System.Drawing.Size(105, 20)
        Me.txtAdminPassword.TabIndex = 4
        '
        'btnAdminLogin
        '
        Me.btnAdminLogin.Location = New System.Drawing.Point(89, 76)
        Me.btnAdminLogin.Name = "btnAdminLogin"
        Me.btnAdminLogin.Size = New System.Drawing.Size(105, 23)
        Me.btnAdminLogin.TabIndex = 5
        Me.btnAdminLogin.Text = "btnAdminLogin"
        Me.btnAdminLogin.UseVisualStyleBackColor = True
        '
        'tlpStudentLogin
        '
        Me.tlpStudentLogin.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.tlpStudentLogin.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.tlpStudentLogin.ColumnCount = 2
        Me.tlpStudentLogin.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.87591!))
        Me.tlpStudentLogin.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.12409!))
        Me.tlpStudentLogin.Controls.Add(Me.lblStudentEmail, 0, 0)
        Me.tlpStudentLogin.Controls.Add(Me.lblStudentPassword, 0, 1)
        Me.tlpStudentLogin.Controls.Add(Me.txtStudentEmail, 1, 0)
        Me.tlpStudentLogin.Controls.Add(Me.txtStudentPassword, 1, 1)
        Me.tlpStudentLogin.Controls.Add(Me.btnStudentLogin, 1, 2)
        Me.tlpStudentLogin.Location = New System.Drawing.Point(4, 45)
        Me.tlpStudentLogin.Name = "tlpStudentLogin"
        Me.tlpStudentLogin.RowCount = 3
        Me.tlpStudentLogin.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.06593!))
        Me.tlpStudentLogin.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.76923!))
        Me.tlpStudentLogin.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.84615!))
        Me.tlpStudentLogin.Size = New System.Drawing.Size(198, 112)
        Me.tlpStudentLogin.TabIndex = 1
        '
        'lblStudentEmail
        '
        Me.lblStudentEmail.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblStudentEmail.AutoSize = True
        Me.lblStudentEmail.Location = New System.Drawing.Point(45, 13)
        Me.lblStudentEmail.Name = "lblStudentEmail"
        Me.lblStudentEmail.Size = New System.Drawing.Size(32, 13)
        Me.lblStudentEmail.TabIndex = 0
        Me.lblStudentEmail.Text = "Email"
        '
        'lblStudentPassword
        '
        Me.lblStudentPassword.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblStudentPassword.AutoSize = True
        Me.lblStudentPassword.Location = New System.Drawing.Point(33, 49)
        Me.lblStudentPassword.Name = "lblStudentPassword"
        Me.lblStudentPassword.Size = New System.Drawing.Size(44, 13)
        Me.lblStudentPassword.TabIndex = 1
        Me.lblStudentPassword.Text = "Passwd"
        '
        'txtStudentEmail
        '
        Me.txtStudentEmail.Location = New System.Drawing.Point(84, 4)
        Me.txtStudentEmail.Name = "txtStudentEmail"
        Me.txtStudentEmail.Size = New System.Drawing.Size(110, 20)
        Me.txtStudentEmail.TabIndex = 3
        '
        'txtStudentPassword
        '
        Me.txtStudentPassword.Location = New System.Drawing.Point(84, 42)
        Me.txtStudentPassword.Name = "txtStudentPassword"
        Me.txtStudentPassword.Size = New System.Drawing.Size(110, 20)
        Me.txtStudentPassword.TabIndex = 4
        '
        'btnStudentLogin
        '
        Me.btnStudentLogin.Location = New System.Drawing.Point(84, 76)
        Me.btnStudentLogin.Name = "btnStudentLogin"
        Me.btnStudentLogin.Size = New System.Drawing.Size(110, 23)
        Me.btnStudentLogin.TabIndex = 5
        Me.btnStudentLogin.Text = "btnStudentLogin"
        Me.btnStudentLogin.UseVisualStyleBackColor = True
        '
        'lblShow
        '
        Me.lblShow.AutoSize = True
        Me.lblShow.Location = New System.Drawing.Point(4, 161)
        Me.lblShow.Name = "lblShow"
        Me.lblShow.Size = New System.Drawing.Size(39, 13)
        Me.lblShow.TabIndex = 3
        Me.lblShow.Text = "Label1"
        '
        'tlpDateFile
        '
        Me.tlpDateFile.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.tlpDateFile.ColumnCount = 1
        Me.tlpDateFile.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpDateFile.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpDateFile.Controls.Add(Me.lblDateFileHeader, 0, 0)
        Me.tlpDateFile.Controls.Add(Me.rtbDateList, 0, 1)
        Me.tlpDateFile.Location = New System.Drawing.Point(3, 214)
        Me.tlpDateFile.Name = "tlpDateFile"
        Me.tlpDateFile.RowCount = 2
        Me.tlpDateFile.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.952606!))
        Me.tlpDateFile.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.04739!))
        Me.tlpDateFile.Size = New System.Drawing.Size(412, 206)
        Me.tlpDateFile.TabIndex = 1
        '
        'lblDateFileHeader
        '
        Me.lblDateFileHeader.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblDateFileHeader.AutoSize = True
        Me.lblDateFileHeader.Location = New System.Drawing.Point(4, 8)
        Me.lblDateFileHeader.Name = "lblDateFileHeader"
        Me.lblDateFileHeader.Size = New System.Drawing.Size(42, 13)
        Me.lblDateFileHeader.TabIndex = 0
        Me.lblDateFileHeader.Text = "date.txt"
        '
        'rtbDateList
        '
        Me.rtbDateList.Location = New System.Drawing.Point(4, 25)
        Me.rtbDateList.Name = "rtbDateList"
        Me.rtbDateList.Size = New System.Drawing.Size(404, 177)
        Me.rtbDateList.TabIndex = 1
        Me.rtbDateList.Text = ""
        '
        'tlpActions
        '
        Me.tlpActions.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.tlpActions.ColumnCount = 1
        Me.tlpActions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpActions.Controls.Add(Me.chkRandomAnswers, 0, 1)
        Me.tlpActions.Controls.Add(Me.chkDisableExtraRead, 0, 2)
        Me.tlpActions.Controls.Add(Me.btnStartTask, 0, 3)
        Me.tlpActions.Location = New System.Drawing.Point(421, 3)
        Me.tlpActions.Name = "tlpActions"
        Me.tlpActions.RowCount = 4
        Me.tlpActions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.54902!))
        Me.tlpActions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.96078!))
        Me.tlpActions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.39216!))
        Me.tlpActions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.07843!))
        Me.tlpActions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpActions.Size = New System.Drawing.Size(370, 205)
        Me.tlpActions.TabIndex = 2
        '
        'chkRandomAnswers
        '
        Me.chkRandomAnswers.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkRandomAnswers.AutoSize = True
        Me.chkRandomAnswers.Location = New System.Drawing.Point(4, 79)
        Me.chkRandomAnswers.Name = "chkRandomAnswers"
        Me.chkRandomAnswers.Size = New System.Drawing.Size(285, 17)
        Me.chkRandomAnswers.TabIndex = 0
        Me.chkRandomAnswers.Text = "Submit Random Answers (Must tick if no admin cookie)"
        Me.chkRandomAnswers.UseVisualStyleBackColor = True
        '
        'chkDisableExtraRead
        '
        Me.chkDisableExtraRead.AutoSize = True
        Me.chkDisableExtraRead.Location = New System.Drawing.Point(4, 103)
        Me.chkDisableExtraRead.Name = "chkDisableExtraRead"
        Me.chkDisableExtraRead.Size = New System.Drawing.Size(117, 17)
        Me.chkDisableExtraRead.TabIndex = 1
        Me.chkDisableExtraRead.Text = "Disable Extra Read"
        Me.chkDisableExtraRead.UseVisualStyleBackColor = True
        '
        'btnStartTask
        '
        Me.btnStartTask.Location = New System.Drawing.Point(4, 164)
        Me.btnStartTask.Name = "btnStartTask"
        Me.btnStartTask.Size = New System.Drawing.Size(117, 36)
        Me.btnStartTask.TabIndex = 2
        Me.btnStartTask.Text = "Start Task"
        Me.btnStartTask.UseVisualStyleBackColor = True
        '
        'ssMain
        '
        Me.ssMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsStatusLabel, Me.tsProgressBar})
        Me.ssMain.Location = New System.Drawing.Point(0, 428)
        Me.ssMain.Name = "ssMain"
        Me.ssMain.Size = New System.Drawing.Size(800, 22)
        Me.ssMain.TabIndex = 1
        Me.ssMain.Text = "StatusStrip1"
        '
        'tsStatusLabel
        '
        Me.tsStatusLabel.Name = "tsStatusLabel"
        Me.tsStatusLabel.Size = New System.Drawing.Size(0, 17)
        '
        'tsProgressBar
        '
        Me.tsProgressBar.Name = "tsProgressBar"
        Me.tsProgressBar.Size = New System.Drawing.Size(100, 16)
        '
        'firefliesGUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.ssMain)
        Me.Controls.Add(Me.tlpMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "firefliesGUI"
        Me.ShowIcon = False
        Me.Text = "firefliesGUI"
        Me.tlpMain.ResumeLayout(False)
        Me.tlpLoginContainer.ResumeLayout(False)
        Me.tlpLoginContainer.PerformLayout()
        Me.tlpAdminLogin.ResumeLayout(False)
        Me.tlpAdminLogin.PerformLayout()
        Me.tlpStudentLogin.ResumeLayout(False)
        Me.tlpStudentLogin.PerformLayout()
        Me.tlpDateFile.ResumeLayout(False)
        Me.tlpDateFile.PerformLayout()
        Me.tlpActions.ResumeLayout(False)
        Me.tlpActions.PerformLayout()
        Me.ssMain.ResumeLayout(False)
        Me.ssMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents ssMain As StatusStrip
    Friend WithEvents tsStatusLabel As ToolStripStatusLabel
    Friend WithEvents tsProgressBar As ToolStripProgressBar
    Friend WithEvents tlpLoginContainer As TableLayoutPanel
    Friend WithEvents tlpAdminLogin As TableLayoutPanel
    Friend WithEvents lblAdminEmail As Label
    Friend WithEvents lblAdminPassword As Label
    Friend WithEvents txtAdminEmail As TextBox
    Friend WithEvents txtAdminPassword As TextBox
    Friend WithEvents btnAdminLogin As Button
    Friend WithEvents tlpStudentLogin As TableLayoutPanel
    Friend WithEvents lblStudentEmail As Label
    Friend WithEvents lblStudentPassword As Label
    Friend WithEvents txtStudentEmail As TextBox
    Friend WithEvents txtStudentPassword As TextBox
    Friend WithEvents btnStudentLogin As Button
    Friend WithEvents tlpDateFile As TableLayoutPanel
    Friend WithEvents lblDateFileHeader As Label
    Friend WithEvents rtbDateList As RichTextBox
    Friend WithEvents tlpActions As TableLayoutPanel
    Friend WithEvents lblShow As Label
    Friend WithEvents chkRandomAnswers As CheckBox
    Friend WithEvents chkDisableExtraRead As CheckBox
    Friend WithEvents btnStartTask As Button
End Class
