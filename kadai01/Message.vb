Public Class Message

    Private Const CON_MESGID_ERROR As String = "1"
    Private Const CON_MESGID_EXCLAMATION As String = "2"
    Private Const CON_MESGID_QUESTION As String = "3"
    Private Const CON_MESGID_INFOMATION As String = "4"

    Private Const CON_MSGID As String = "MSGID"

    ''' <summary>
    ''' メッセージ表示
    ''' </summary>
    ''' <param name="strMsgID">メッセージID</param>
    ''' <param name="strAddMsg">追加メッセージ</param>
    Friend Shared Sub ShowMessage(ByVal strMsgID As String,
                                  Optional ByVal strAddMsg As String = Nothing)

        Dim objIcon As MessageBoxIcon = MessageBoxIcon.None
        Dim objButton As MessageBoxButtons = MessageBoxButtons.OK
        Dim strMsg As String = String.Empty

        'メッセージID上1桁判定
        Select Case Left(strMsgID, 1)
            'エラーの場合
            Case CON_MESGID_ERROR
                objIcon = MessageBoxIcon.Error

            '警告の場合
            Case CON_MESGID_EXCLAMATION
                objIcon = MessageBoxIcon.Exclamation

            '確認の場合
            Case CON_MESGID_QUESTION
                objIcon = MessageBoxIcon.Question
                objButton = MessageBoxButtons.OKCancel

            '情報の場合
            Case CON_MESGID_INFOMATION
                objIcon = MessageBoxIcon.Information

            Case Else
        End Select

        'リソースから、メッセージ取得
        strMsg = My.Resources.ResourceManager.GetString(CON_MSGID & strMsgID)
        '追加メッセージがある場合は追加
        If Not strAddMsg Is Nothing Then
            strMsg = strMsg & vbCrLf & strAddMsg
        End If

        'メッセージ表示
        MessageBox.Show(strMsg, My.Application.Info.AssemblyName, objButton, objIcon)

    End Sub

    ''' <summary>
    ''' エラーメッセージ表示
    ''' </summary>
    ''' <param name="objErr">エラー情報</param>
    Friend Shared Sub ShowErrMessage(ByRef objErr As Exception)
        MessageBox.Show(objErr.Message & vbCrLf & objErr.Source,
                        My.Application.Info.AssemblyName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
    End Sub

End Class
