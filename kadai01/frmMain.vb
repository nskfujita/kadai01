Imports System
Imports System.IO
Imports System.Text

Public Class frmMain
    ''' <summary>
    ''' 列挙型 CSVデータ
    ''' </summary>
    Private Enum enmCsvData
        YMD = 0
        Rain
        RainNoInfo
        HighTemp
        LowTemp
    End Enum

    ''' <summary>
    ''' 構造体 CSVデータ
    ''' </summary>
    Private Structure STC_CSV_DATA
        Public datYMD As Date               '日付
        Public decRain As Decimal           '降水量
        Public decRainNoInfo As Decimal     '降水量（未使用情報）
        Public decHighTemp As Decimal       '最高気温
        Public decLowTemp As Decimal        '最低気温

        ''' <summary>
        ''' 構造体設定
        ''' </summary>
        ''' <param name="strArgs"></param>
        Public Sub SetInfo(ByVal strArgs As String())
            datYMD = CType(strArgs(enmCsvData.YMD), Date)
            decRain = CType(strArgs(enmCsvData.Rain), Decimal)
            decRainNoInfo = CType(strArgs(enmCsvData.RainNoInfo), Decimal)
            decHighTemp = CType(strArgs(enmCsvData.HighTemp), Decimal)
            decLowTemp = CType(strArgs(enmCsvData.LowTemp), Decimal)
        End Sub
    End Structure

    ''' <summary>
    ''' クラス定数
    ''' </summary>
    Private Const CON_DELEMITER As String = ","     'CSV区切り文字

    ''' <summary>
    ''' フォームロード
    ''' </summary>
    ''' <param name="sender">オブジェクト</param>
    ''' <param name="e">イベント</param>
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim stcCsvData1 As STC_CSV_DATA() = Nothing
        Dim stcCsvData2 As STC_CSV_DATA() = Nothing
        Dim strEffectDate1 As String() = Nothing
        Dim strEffectDate2 As String() = Nothing
        Dim strResult As String() = Nothing

        Try
            '引数設定判定
            If CheckCommandLine() = False Then
                FormClose()
                Exit Sub
            End If

            'CSV1データ読み込み
            If ReadData(Environment.GetCommandLineArgs(1), stcCsvData1) = False Then
                FormClose()
                Exit Sub
            End If
            If ReadData(Environment.GetCommandLineArgs(2), stcCsvData2) = False Then
                FormClose()
                Exit Sub
            End If

            '有効日付取得
            GetEffectDate(stcCsvData1, strEffectDate1)
            GetEffectDate(stcCsvData2, strEffectDate2)

            '日付比較
            DateCompare(strEffectDate1, strEffectDate2, strResult)

            '画面表示
            SetDateData(strResult)

        Catch exErr As Exception
            'エラー処理
            Message.ShowErrMessage(exErr)
            FormClose()

        End Try

    End Sub

    ''' <summary>
    ''' 引数設定判定
    ''' </summary>
    ''' <returns>True - OK, False - NG</returns>
    Private Function CheckCommandLine() As Boolean
        Try
            If Environment.GetCommandLineArgs.Length = 1 Then
                Message.ShowMessage("1001")
                Return False
            End If

            If Environment.GetCommandLineArgs.Length <> 3 Then
                Message.ShowMessage("1002")
                Return False
            End If

            Return True

        Catch exErr As Exception
            'エラー処理
            Message.ShowErrMessage(exErr)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' CSVデータ読み込み
    ''' </summary>
    ''' <param name="strFileName">CSVファイル名</param>
    ''' <param name="stcCsvData">読み込みデータ</param>
    ''' <returns>True - 正常終了, False - 異常終了</returns>
    Private Function ReadData(ByVal strFileName As String,
                              ByRef stcCsvData As STC_CSV_DATA()) As String

        Dim strFilePath As String = String.Empty
        Dim strDir As String = String.Empty
        Dim strReadLine As String = String.Empty
        Dim intIdx As Integer = 0
        Dim objTS As StreamReader = Nothing
        Dim blnRet As Boolean = False

        Try
            'CSVファイルパス取得
            strDir = Directory.GetCurrentDirectory
            strFilePath = Path.Combine(strDir.ToString, strFileName.ToString)

            'ファイル存在判定
            If File.Exists(strFilePath) = False Then
                Message.ShowMessage("2001")
                Return blnRet
            End If

            'ファイルオープン
            objTS = New StreamReader(strFilePath, Encoding.Default)

            'データ読み込み
            Do Until objTS.EndOfStream = True
                strReadLine = objTS.ReadLine

                '読み込みデータ整合性判定
                If CheckCsvData(strReadLine) = True Then
                    ReDim Preserve stcCsvData(intIdx)
                    stcCsvData(intIdx).SetInfo(strReadLine.Split(CON_DELEMITER))
                    intIdx += 1
                End If
            Loop

            blnRet = True

        Catch expErr As Exception
            Message.ShowErrMessage(expErr)

        Finally
            If Not objTS Is Nothing Then
                objTS.Close()
            End If

        End Try

        Return blnRet

    End Function

    ''' <summary>
    ''' データ整合性判定
    ''' </summary>
    ''' <param name="strLine">読み込み行</param>
    ''' <returns>True - 判定OK, False - 判定NG</returns>
    Private Function CheckCsvData(ByVal strLine As String) As Boolean
        Dim strValue As String()

        '空白判定
        If strLine.Equals(String.Empty) = True Then
            Return False
        End If

        '区切り文字で分割
        strValue = strLine.Split(CON_DELEMITER)

        '配列数判定
        If strValue.Length <> 5 Then
            Return False
        End If

        'データ判定
        '日付
        If CheckDatatime(strValue(enmCsvData.YMD)) = False Then
            Return False
        End If
        '降水量
        If CheckDecimal(strValue(enmCsvData.Rain)) = False Then
            Return False
        End If
        '降水量（未使用データ）
        If CheckDecimal(strValue(enmCsvData.RainNoInfo)) = False Then
            Return False
        End If
        '最高気温
        If CheckDecimal(strValue(enmCsvData.HighTemp)) = False Then
            Return False
        End If
        '最低気温
        If CheckDecimal(strValue(enmCsvData.LowTemp)) = False Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' 日付形式判定
    ''' </summary>
    ''' <param name="strChkValue">チェック文字列</param>
    ''' <returns>True - 日付型, False - 日付型以外</returns>
    Private Function CheckDatatime(ByVal strChkValue As String) As Boolean
        Dim datWork As DateTime
        If DateTime.TryParse(strChkValue, datWork) = False Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' 少数数値形式判定
    ''' </summary>
    ''' <param name="strChkValue">チェック文字列</param>
    ''' <returns>True - 少数数値型, False - 少数数値型以外</returns>
    Private Function CheckDecimal(ByVal strChkValue As String) As Boolean
        Dim decWork As Decimal
        If Decimal.TryParse(strChkValue, decWork) = False Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' 有効日付取得
    ''' </summary>
    ''' <param name="stcCsvData">読み込みデータ</param>
    ''' <param name="strEffectDate">有効データ</param>
    Private Sub GetEffectDate(ByRef stcCsvData As STC_CSV_DATA(), ByRef strEffectDate As String())
        Dim intIdx As Integer = 0
        Dim intAddIdx As Integer = 0

        '読み込みデータ分、繰り返し
        For intIdx = 0 To stcCsvData.Length - 1
            With stcCsvData(intIdx)
                '降水量が1以上、気温差が1度以上
                If .decRain >= 1 And (.decHighTemp - .decLowTemp) >= 1 Then
                    ReDim Preserve strEffectDate(intAddIdx)
                    strEffectDate(intAddIdx) = CType(.datYMD, String)
                    intAddIdx += 1
                End If
            End With
        Next
    End Sub

    ''' <summary>
    ''' 日付比較
    ''' </summary>
    ''' <param name="strCompare1">比較元データ</param>
    ''' <param name="strCompare2">比較先データ</param>
    ''' <param name="strResult">比較結果</param>
    Private Sub DateCompare(ByRef strCompare1 As String(), ByRef strCompare2 As String(), ByRef strResult As String())
        Dim intComp1Idx As Integer = 0
        Dim intComp2Idx As Integer = 0
        Dim intResult As Integer = 0
        Dim intRet As Integer = 0

        '比較元データ数分、繰り返し
        For intComp1Idx = 0 To strCompare1.Length
            '比較先データ数分、繰り返し
            Do Until intComp2Idx = strCompare2.Length - 1
                '日付比較
                intRet = Date.Compare(strCompare1(intComp1Idx), strCompare2(intComp2Idx))
                If intRet = 0 Then
                    '一致する場合
                    ReDim Preserve strResult(intResult)
                    strResult(intResult) = strCompare1(intComp1Idx)
                    intResult += 1
                    intComp2Idx += 1
                    Exit Do
                ElseIf intRet < 0 Then
                    '比較先が大きい場合
                    Exit Do
                ElseIf intRet > 0 Then
                    '比較元が大きい場合
                    intComp2Idx += 1
                End If
            Loop
        Next
    End Sub

    ''' <summary>
    ''' 画面表示
    ''' </summary>
    ''' <param name="strDate">日付</param>
    Private Sub SetDateData(ByRef strDate As String())
        Dim intIdx As Integer = 0

        'データグリッドクリア
        Me.dgvDate.Rows.Clear()

        '日付設定
        For intIdx = 0 To strDate.Length - 1
            Me.dgvDate.Rows.Add()
            Me.dgvDate.Rows(intIdx).Cells(0).Value = strDate(intIdx)
        Next
    End Sub

    ''' <summary>
    ''' 画面終了
    ''' </summary>
    Private Sub FormClose() Handles btnEnd.Click
        Me.Close()
    End Sub
End Class