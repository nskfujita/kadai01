<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.dgvDate = New System.Windows.Forms.DataGridView()
        Me.btnEnd = New System.Windows.Forms.Button()
        Me.日付 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvDate
        '
        Me.dgvDate.AllowUserToAddRows = False
        Me.dgvDate.AllowUserToDeleteRows = False
        Me.dgvDate.AllowUserToResizeColumns = False
        Me.dgvDate.AllowUserToResizeRows = False
        Me.dgvDate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDate.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.日付})
        Me.dgvDate.Location = New System.Drawing.Point(12, 12)
        Me.dgvDate.Name = "dgvDate"
        Me.dgvDate.RowTemplate.Height = 21
        Me.dgvDate.Size = New System.Drawing.Size(175, 238)
        Me.dgvDate.TabIndex = 0
        '
        'btnEnd
        '
        Me.btnEnd.Location = New System.Drawing.Point(41, 256)
        Me.btnEnd.Name = "btnEnd"
        Me.btnEnd.Size = New System.Drawing.Size(123, 44)
        Me.btnEnd.TabIndex = 1
        Me.btnEnd.Text = "終了"
        Me.btnEnd.UseVisualStyleBackColor = True
        '
        '日付
        '
        Me.日付.HeaderText = "日付"
        Me.日付.Name = "日付"
        Me.日付.ReadOnly = True
        Me.日付.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(199, 312)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnEnd)
        Me.Controls.Add(Me.dgvDate)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(215, 350)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(215, 350)
        Me.Name = "frmMain"
        Me.Text = "課題01"
        CType(Me.dgvDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dgvDate As DataGridView
    Friend WithEvents btnEnd As Button
    Friend WithEvents 日付 As DataGridViewTextBoxColumn
End Class
