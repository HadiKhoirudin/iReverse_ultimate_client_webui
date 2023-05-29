Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Text
Imports System.Threading
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports Microsoft.VisualBasic
Imports iReverse_Ultimate_Client.DXApplication.Mtkxml
Module MTK_OneClick
    Public Firmware As List(Of Firmware) = New List(Of Firmware)()
    Public SearchTime As String
    Public EndTime As String

    Public WorkerMethod As String = "UNIFERSAL"
    Public Sub MTKOneclick(Method As String)

        Mediatek.Mediatek_tool.FlashOption.Method = Method
        Watch = New Stopwatch()
        XtraMain.BgwFlashfirmware = New BackgroundWorker With {
            .WorkerSupportsCancellation = True
        }
        AddHandler XtraMain.BgwFlashfirmware.DoWork, AddressOf BgwFlashfirmware_DoWork
        AddHandler XtraMain.BgwFlashfirmware.RunWorkerCompleted, AddressOf BgwFlashfirmware_RunWorkerCompleted
        XtraMain.BgwFlashfirmware.RunWorkerAsync()
        XtraMain.BgwFlashfirmware.Dispose()

    End Sub
    Public Sub BgwFlashfirmware_DoWork(sender As Object, e As DoWorkEventArgs)
        If WorkerMethod = "UNIFERSAL" Then

            If Mediatek.Mediatek_tool.FlashOption.Method = "Readgpttable" Then

                ''If XtraMain.DelegateFunction.CheckboxEMI.Checked Then
                ''Mediatek.Authentication.Python.Command(String.Concat(New String() {"printgpt", " ", "--preloader=", Mediatek.Mediatek_tool.Mediatek.PreloaderEmi}),XtraMain.DelegateFunction.BgwFlashfirmware, e)
                ''Else
                Mediatek.Authentication.Python.Command("printgpt", XtraMain.DelegateFunction.BgwFlashfirmware, e)
                ''End If

                If XtraMain.DelegateFunction.BgwFlashfirmware.CancellationPending Then

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    TimeSpanElapsed.ElapsedPending(Watch)
                    e.Cancel = True
                    Return
                Else

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    Watch.[Stop]()
                    TimeSpanElapsed.ElapsedTime(Watch)
                End If
            ElseIf Mediatek.Mediatek_tool.FlashOption.Method = "Rebootstage" Then
                Mediatek.Authentication.Python.Command("reset", XtraMain.DelegateFunction.BgwFlashfirmware, e)

                If XtraMain.DelegateFunction.BgwFlashfirmware.CancellationPending Then

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    TimeSpanElapsed.ElapsedPending(Watch)
                    e.Cancel = True
                    Return
                Else

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    Watch.[Stop]()
                    TimeSpanElapsed.ElapsedTime(Watch)
                End If
            ElseIf Mediatek.Mediatek_tool.FlashOption.Method = "Erasegpttable" Then
                Dim text2 As String = ""

                For Each addres As Mediatek.Mediatek_list.Addresformat.Addres In Mediatek.Mediatek_list.Addresformat.Format
                    File.AppendAllText(Mediatek.Mediatek_tool.Unifersalformat.format, addres.Filename & ",")
                Next

                Using streamReader As StreamReader = New StreamReader(Mediatek.Mediatek_tool.Unifersalformat.format)
                    Dim text As String

                    'While text IsNot Nothing
                    'text2 = text.Remove(text.Length - 1)
                    'text = streamReader.ReadLine()
                    'End While


                    While CSharpImpl.__Assign(text, streamReader.ReadLine()) IsNot Nothing

                        text2 = text.Remove(text.Length - 1)
                    End While
                End Using

                If File.Exists(Mediatek.Mediatek_tool.Unifersalformat.format) Then
                    File.Delete(Mediatek.Mediatek_tool.Unifersalformat.format)
                End If

                ''If XtraMain.DelegateFunction.CheckboxEMI.Checked Then
                ''Mediatek.Authentication.Python.Command(String.Concat(New String() {"e", " ", text2, " ", "--preloader=", Mediatek.Mediatek_tool.Mediatek.PreloaderEmi}), XtraMain.DelegateFunction.BgwFlashfirmware, e)
                ''Else
                Mediatek.Authentication.Python.Command(String.Concat(New String() {"e", " ", text2}), XtraMain.DelegateFunction.BgwFlashfirmware, e)
                ''End If

                If XtraMain.DelegateFunction.BgwFlashfirmware.CancellationPending Then

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    TimeSpanElapsed.ElapsedPending(Watch)
                    e.Cancel = True
                    Return
                Else

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    Watch.[Stop]()
                    TimeSpanElapsed.ElapsedTime(Watch)
                End If
            ElseIf Mediatek.Mediatek_tool.FlashOption.Method = "Readpartitiontable" Then
                Dim arr As String = " " & Mediatek.Mediatek_tool.Mediatek.Savepartition & "=" & "," & vbLf
                Dim str As String = Mediatek.Mediatek_tool.FlashOption.Textpartition.Replace(",", arr)
                File.AppendAllText(Mediatek.Mediatek_tool.Unifersalformat.format, str)

                Using streamReader As StreamReader = New StreamReader(Mediatek.Mediatek_tool.Unifersalformat.format)
                    Dim text As String

                    'While text IsNot Nothing
                    'If text.Contains(text.Replace(text.Substring(text.IndexOf("""") - 1), "")) Then
                    'Dim text2 As String = text.Replace(text, Mediatek.Mediatek_tool.Mediatek.Savepartition & "\" & text.Replace(text.Substring(text.IndexOf("""") - 1), ","))
                    'File.AppendAllText(Mediatek.Mediatek_tool.Unifersalformat.formatpath, text2)
                    'End If
                    'text = streamReader.ReadLine()
                    'End While


                    While CSharpImpl.__Assign(text, streamReader.ReadLine()) IsNot Nothing

                        If text.Contains(text.Replace(text.Substring(text.IndexOf("""") - 1), "")) Then
                            Dim text2 As String = text.Replace(text, Mediatek.Mediatek_tool.Mediatek.Savepartition & "\" & text.Replace(text.Substring(text.IndexOf("""") - 1), ","))
                            File.AppendAllText(Mediatek.Mediatek_tool.Unifersalformat.formatpath, text2)
                        End If
                    End While
                End Using

                If File.Exists(Mediatek.Mediatek_tool.Unifersalformat.format) Then
                    File.Delete(Mediatek.Mediatek_tool.Unifersalformat.format)
                End If

                Dim text3 As String = ""

                Using streamReader As StreamReader = New StreamReader(Mediatek.Mediatek_tool.Unifersalformat.formatpath)
                    Dim text As String

                    'While text IsNot Nothing
                    'text3 = text
                    'text = streamReader.ReadLine()
                    'End While


                    While CSharpImpl.__Assign(text, streamReader.ReadLine()) IsNot Nothing

                        text3 = text
                    End While
                End Using

                If File.Exists(Mediatek.Mediatek_tool.Unifersalformat.formatpath) Then
                    File.Delete(Mediatek.Mediatek_tool.Unifersalformat.formatpath)
                End If

                ''If XtraMain.DelegateFunction.CheckboxEMI.Checked Then
                Mediatek.Authentication.Python.Command(String.Concat(New String() {"r", " ", Mediatek.Mediatek_tool.FlashOption.Textpartition.Remove(Mediatek.Mediatek_tool.FlashOption.Textpartition.Length - 1), " ", text3.Remove(text3.Length - 1), " ", "--preloader=", Mediatek.Mediatek_tool.Mediatek.PreloaderEmi}), XtraMain.DelegateFunction.BgwFlashfirmware, e)
                ''Else
                Mediatek.Authentication.Python.Command(String.Concat(New String() {"r", " ", Mediatek.Mediatek_tool.FlashOption.Textpartition.Remove(Mediatek.Mediatek_tool.FlashOption.Textpartition.Length - 1), " ", text3.Remove(text3.Length - 1)}), XtraMain.DelegateFunction.BgwFlashfirmware, e)
                ''End If

                If XtraMain.DelegateFunction.BgwFlashfirmware.CancellationPending Then

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    TimeSpanElapsed.ElapsedPending(Watch)
                    e.Cancel = True
                    Return
                Else

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    Watch.[Stop]()
                    TimeSpanElapsed.ElapsedTime(Watch)
                End If
            ElseIf Mediatek.Mediatek_tool.FlashOption.Method = "Writepartitiontable" Then

                For Each addres As Mediatek.Mediatek_list.Addresformat.Addres In Mediatek.Mediatek_list.Addresformat.Format
                    File.AppendAllText(Mediatek.Mediatek_tool.Unifersalformat.format, addres.Filename & ",")
                    File.AppendAllText(Mediatek.Mediatek_tool.Unifersalformat.formatpath, addres.Filename & " " & "=""" + addres.Filepath & """" & "," & vbLf)
                Next

                Using streamReader As StreamReader = New StreamReader(Mediatek.Mediatek_tool.Unifersalformat.format)
                    Dim text As String

                    'While text IsNot Nothing
                    'Mediatek.Mediatek_tool.FlashOption.Textpartition = text
                    'text = streamReader.ReadLine()
                    'End While


                    While CSharpImpl.__Assign(text, streamReader.ReadLine()) IsNot Nothing

                        Mediatek.Mediatek_tool.FlashOption.Textpartition = text
                    End While
                End Using

                If File.Exists(Mediatek.Mediatek_tool.Unifersalformat.format) Then
                    File.Delete(Mediatek.Mediatek_tool.Unifersalformat.format)
                End If

                Using streamReader As StreamReader = New StreamReader(Mediatek.Mediatek_tool.Unifersalformat.formatpath)
                    Dim text As String

                    'While text IsNot Nothing
                    'If text.Contains(text.Replace(text.Substring(text.IndexOf("=") - 1), "")) Then
                    'Dim text2 As String = text.Substring(text.IndexOf("=") + 1)
                    'File.AppendAllText(Mediatek.Mediatek_tool.Unifersalformat.format, text2)
                    'End If
                    'text = streamReader.ReadLine()
                    'End While


                    While CSharpImpl.__Assign(text, streamReader.ReadLine()) IsNot Nothing

                        If text.Contains(text.Replace(text.Substring(text.IndexOf("=") - 1), "")) Then
                            Dim text2 As String = text.Substring(text.IndexOf("=") + 1)
                            File.AppendAllText(Mediatek.Mediatek_tool.Unifersalformat.format, text2)
                        End If
                    End While
                End Using

                If File.Exists(Mediatek.Mediatek_tool.Unifersalformat.formatpath) Then
                    File.Delete(Mediatek.Mediatek_tool.Unifersalformat.formatpath)
                End If

                Dim text3 As String = ""

                Using streamReader As StreamReader = New StreamReader(Mediatek.Mediatek_tool.Unifersalformat.format)
                    Dim text As String

                    'While text IsNot Nothing
                    'text3 = text
                    'text = streamReader.ReadLine()
                    'End While


                    While CSharpImpl.__Assign(text, streamReader.ReadLine()) IsNot Nothing

                        text3 = text
                    End While
                End Using

                If File.Exists(Mediatek.Mediatek_tool.Unifersalformat.format) Then
                    File.Delete(Mediatek.Mediatek_tool.Unifersalformat.format)
                End If

                ''If XtraMain.DelegateFunction.CheckboxEMI.Checked Then
                ''Mediatek.Authentication.Python.Command(String.Concat(New String() {"w", " ", Mediatek.Mediatek_tool.FlashOption.Textpartition.Remove(Mediatek.Mediatek_tool.FlashOption.Textpartition.Length - 1), " ", text3.Remove(text3.Length - 1), " ", "--preloader=", Mediatek.Mediatek_tool.Mediatek.PreloaderEmi}), XtraMain.DelegateFunction.BgwFlashfirmware, e)
                ''Else
                Mediatek.Authentication.Python.Command(String.Concat(New String() {"w", " ", Mediatek.Mediatek_tool.FlashOption.Textpartition.Remove(Mediatek.Mediatek_tool.FlashOption.Textpartition.Length - 1), " ", text3.Remove(text3.Length - 1)}), XtraMain.DelegateFunction.BgwFlashfirmware, e)
                ''End If

                If XtraMain.DelegateFunction.BgwFlashfirmware.CancellationPending Then

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    TimeSpanElapsed.ElapsedPending(Watch)
                    e.Cancel = True
                    Return
                Else

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    Watch.[Stop]()
                    TimeSpanElapsed.ElapsedTime(Watch)
                End If
            ElseIf Mediatek.Mediatek_tool.FlashOption.Method = "Readinfogpt" Then

                If Not File.Exists(Mediatek.Mediatek_tool.Sourcefile.Andoidpath) Then
                    Directory.CreateDirectory(Path.GetDirectoryName(Mediatek.Mediatek_tool.Sourcefile.Andoidpath))
                    File.WriteAllBytes(Mediatek.Mediatek_tool.Sourcefile.Andoidpath, My.Resources.C4)
                End If

                ''If XtraMain.DelegateFunction.CheckboxEMI.Checked Then
                ''Dim text As String = """" & Mediatek.Mediatek_tool.Sourcefile.Dumped & """"
                ''Mediatek.Authentication.Python.Command(String.Concat(New String() {"r", " ", "recovery", " ", text, " ", "--preloader=", Mediatek.Mediatek_tool.Mediatek.PreloaderEmi}), XtraMain.DelegateFunction.BgwFlashfirmware, e)
                ''Else
                Dim text As String = """" & Mediatek.Mediatek_tool.Sourcefile.Dumped & """"
                Mediatek.Authentication.Python.Command(String.Concat(New String() {"r", " ", "recovery", " ", text}), XtraMain.DelegateFunction.BgwFlashfirmware, e)
                ''End If

                If Mediatek.Mediatek_tool.FlashOption.progres <> 100 AndAlso Mediatek.Mediatek_tool.FlashOption.progres <> 0 Then
                    RichLogs("failed", "red", True, True)
                    File.Delete(Mediatek.Mediatek_tool.Sourcefile.Andoidpath)
                    File.Delete(Mediatek.Mediatek_tool.Sourcefile.Dumped)
                    e.Cancel = True
                End If

                If XtraMain.DelegateFunction.BgwFlashfirmware.CancellationPending Then

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    Dim directory As DirectoryInfo = New DirectoryInfo(Path.GetDirectoryName(Mediatek.Mediatek_tool.Sourcefile.Andoidpath))

                    For Each file As FileInfo In directory.EnumerateFiles()
                        file.Delete()
                    Next

                    For Each subDirectory As DirectoryInfo In directory.EnumerateDirectories()
                        subDirectory.Delete(True)
                    Next

                    directory.Delete(True)
                    TimeSpanElapsed.ElapsedPending(Watch)
                    e.Cancel = True
                    Return
                Else

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    Dim directory As DirectoryInfo = New DirectoryInfo(Path.GetDirectoryName(Mediatek.Mediatek_tool.Sourcefile.Andoidpath))

                    For Each file As FileInfo In directory.EnumerateFiles()
                        file.Delete()
                    Next

                    For Each subDirectory As DirectoryInfo In directory.EnumerateDirectories()
                        subDirectory.Delete(True)
                    Next

                    directory.Delete(True)
                    Watch.[Stop]()
                    TimeSpanElapsed.ElapsedTime(Watch)
                End If
            ElseIf Mediatek.Mediatek_tool.FlashOption.Method = "Bacupnvram" Then
                Dim str As String = Mediatek.Mediatek_tool.Mediatek.Savepartition & "\" + Mediatek.Mediatek_tool.FlashOption.NV_save.Replace(",", "," & Mediatek.Mediatek_tool.Mediatek.Savepartition & "\")
                Console.WriteLine(str)

                ''If XtraMain.DelegateFunction.CheckboxEMI.Checked Then
                ''Mediatek.Authentication.Python.Command(String.Concat(New String() {"r", " ", Mediatek.Mediatek_tool.FlashOption.NV_save.Replace(".img", ""), " ", str, " ", "--preloader=", Mediatek.Mediatek_tool.Mediatek.PreloaderEmi}), XtraMain.DelegateFunction.BgwFlashfirmware, e)
                ''Else
                Mediatek.Authentication.Python.Command(String.Concat(New String() {"r", " ", Mediatek.Mediatek_tool.FlashOption.NV_save.Replace(".img", ""), " ", str}), XtraMain.DelegateFunction.BgwFlashfirmware, e)
                ''End If

                If XtraMain.DelegateFunction.BgwFlashfirmware.CancellationPending Then

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    TimeSpanElapsed.ElapsedPending(Watch)
                    e.Cancel = True
                    Return
                Else

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    Watch.[Stop]()
                    TimeSpanElapsed.ElapsedTime(Watch)
                End If
            ElseIf Mediatek.Mediatek_tool.FlashOption.Method = "Erasenvram" Then

                ''If XtraMain.DelegateFunction.CheckboxEMI.Checked Then
                ''Mediatek.Authentication.Python.Command(String.Concat(New String() {"e", " ", Mediatek.Mediatek_tool.FlashOption.NV_save.Replace(".img", ""), " ", "--preloader=", Mediatek.Mediatek_tool.Mediatek.PreloaderEmi}), XtraMain.DelegateFunction.BgwFlashfirmware, e)
                ''Else
                Mediatek.Authentication.Python.Command(String.Concat(New String() {"e", " ", Mediatek.Mediatek_tool.FlashOption.NV_save.Replace(".img", "")}), XtraMain.DelegateFunction.BgwFlashfirmware, e)
                ''End If

                If XtraMain.DelegateFunction.BgwFlashfirmware.CancellationPending Then

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    TimeSpanElapsed.ElapsedPending(Watch)
                    e.Cancel = True
                    Return
                Else

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    Watch.[Stop]()
                    TimeSpanElapsed.ElapsedTime(Watch)
                End If
            ElseIf Mediatek.Mediatek_tool.FlashOption.Method = "Restorenvram" Then
                Dim directoryInfo As DirectoryInfo = New DirectoryInfo(Mediatek.Mediatek_tool.Mediatek.Savepartition)
                Dim name As List(Of String) = New List(Of String)()

                For Each fileInfo As FileInfo In directoryInfo.EnumerateFiles()
                    name.Add(fileInfo.Name & ",")
                Next

                Dim stringBuilder As StringBuilder = New StringBuilder()

                For Each Build As String In name
                    stringBuilder.Append(Build)
                Next

                Dim text As String = stringBuilder.ToString().Remove(stringBuilder.ToString().Length - 1)
                Console.WriteLine(text)
                Dim text2 As String = """" & Path.GetDirectoryName(Mediatek.Mediatek_tool.Mediatek.Savepartition) & """\" & text.Replace(",", "," & """" & Path.GetDirectoryName(Mediatek.Mediatek_tool.Mediatek.Savepartition) & """\")
                Console.WriteLine(text2)

                ''If XtraMain.DelegateFunction.CheckboxEMI.Checked Then
                ''Mediatek.Authentication.Python.Command(String.Concat(New String() {"w", " ", text.Replace(".img", ""), " ", text2, " ", "--preloader=", Mediatek.Mediatek_tool.Mediatek.PreloaderEmi}), XtraMain.DelegateFunction.BgwFlashfirmware, e)
                ''Else
                Mediatek.Authentication.Python.Command(String.Concat(New String() {"w", " ", text.Replace(".img", ""), " ", text2}), XtraMain.DelegateFunction.BgwFlashfirmware, e)
                ''End If

                If XtraMain.DelegateFunction.BgwFlashfirmware.CancellationPending Then

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    TimeSpanElapsed.ElapsedPending(Watch)
                    e.Cancel = True
                    Return
                Else

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    Watch.[Stop]()
                    TimeSpanElapsed.ElapsedTime(Watch)
                End If
            ElseIf Mediatek.Mediatek_tool.FlashOption.Method = "BypassAuth" Then
                Mediatek.Authentication.Python.Command("payload", XtraMain.DelegateFunction.BgwFlashfirmware, e)

                If XtraMain.DelegateFunction.BgwFlashfirmware.CancellationPending Then
                    e.Cancel = True
                Else
                    Watch.[Stop]()
                    TimeSpanElapsed.ElapsedTime(Watch)
                End If
            ElseIf Mediatek.Mediatek_tool.FlashOption.Method = "ReadRPMB" Then
                Mediatek.Authentication.Python.Command(String.Concat(New String() {"da", " ", "rpmb", " ", "r"}), XtraMain.DelegateFunction.BgwFlashfirmware, e)

                If XtraMain.DelegateFunction.BgwFlashfirmware.CancellationPending Then

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    TimeSpanElapsed.ElapsedPending(Watch)
                    e.Cancel = True
                    Return
                Else

                    If File.Exists("rpmb.bin") Then
                        File.Move("rpmb.bin", Mediatek.Mediatek_tool.FlashOption.RPMB)
                    End If

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    Watch.[Stop]()
                    TimeSpanElapsed.ElapsedTime(Watch)
                End If
            ElseIf Mediatek.Mediatek_tool.FlashOption.Method = "WriteRPMB" Then
                Mediatek.Authentication.Python.Command(String.Concat(New String() {"da", " ", "rpmb", " ", "w", " ", Mediatek.Mediatek_tool.FlashOption.RPMB}), XtraMain.DelegateFunction.BgwFlashfirmware, e)

                If XtraMain.DelegateFunction.BgwFlashfirmware.CancellationPending Then

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    TimeSpanElapsed.ElapsedPending(Watch)
                    e.Cancel = True
                    Return
                Else

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    Watch.[Stop]()
                    TimeSpanElapsed.ElapsedTime(Watch)
                End If
            ElseIf Mediatek.Mediatek_tool.FlashOption.Method = "EraseRPMB" Then
                Mediatek.Authentication.Python.Command(String.Concat(New String() {"da", " ", "rpmb", " ", "e"}), XtraMain.DelegateFunction.BgwFlashfirmware, e)

                If XtraMain.DelegateFunction.BgwFlashfirmware.CancellationPending Then

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    TimeSpanElapsed.ElapsedPending(Watch)
                    e.Cancel = True
                    Return
                Else

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    Watch.[Stop]()
                    TimeSpanElapsed.ElapsedTime(Watch)
                End If
            ElseIf Mediatek.Mediatek_tool.FlashOption.Method = "UniversalUnlock" Then
                Mediatek.Authentication.Python.Command(String.Concat(New String() {"da", " ", "seccfg", " ", "unlock"}), XtraMain.DelegateFunction.BgwFlashfirmware, e)

                If XtraMain.DelegateFunction.BgwFlashfirmware.CancellationPending Then

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    TimeSpanElapsed.ElapsedPending(Watch)
                    e.Cancel = True
                    Return
                Else

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    Watch.[Stop]()
                    TimeSpanElapsed.ElapsedTime(Watch)
                End If
            ElseIf Mediatek.Mediatek_tool.FlashOption.Method = "Universalrelock" Then
                Mediatek.Authentication.Python.Command(String.Concat(New String() {"da", " ", "seccfg", " ", "lock"}), XtraMain.DelegateFunction.BgwFlashfirmware, e)

                If XtraMain.DelegateFunction.BgwFlashfirmware.CancellationPending Then

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    TimeSpanElapsed.ElapsedPending(Watch)
                    e.Cancel = True
                    Return
                Else

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    Watch.[Stop]()
                    TimeSpanElapsed.ElapsedTime(Watch)
                End If
            ElseIf Mediatek.Mediatek_tool.FlashOption.Method = "Universalfrp" Then
                Mediatek.Authentication.Python.Command(String.Concat(New String() {"e", " ", "frp"}), XtraMain.DelegateFunction.BgwFlashfirmware, e)

                If XtraMain.DelegateFunction.BgwFlashfirmware.CancellationPending Then

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    TimeSpanElapsed.ElapsedPending(Watch)
                    e.Cancel = True
                    Return
                Else

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    Watch.[Stop]()
                    TimeSpanElapsed.ElapsedTime(Watch)
                End If
            ElseIf Mediatek.Mediatek_tool.FlashOption.Method = "EraseSAMfrp" Then
                Mediatek.Authentication.Python.Command(String.Concat(New String() {"e", " ", "persistent"}), XtraMain.DelegateFunction.BgwFlashfirmware, e)

                If XtraMain.DelegateFunction.BgwFlashfirmware.CancellationPending Then

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    TimeSpanElapsed.ElapsedPending(Watch)
                    e.Cancel = True
                    Return
                Else

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    Watch.[Stop]()
                    TimeSpanElapsed.ElapsedTime(Watch)
                End If
            ElseIf Mediatek.Mediatek_tool.FlashOption.Method = "ErasefrpOEM" Then
                Dim str As String = """" & Mediatek.Mediatek_tool.ExSamsung.persistent & """"
                Mediatek.Authentication.Python.Command(String.Concat(New String() {"w", " ", "persistent", " ", str}), XtraMain.DelegateFunction.BgwFlashfirmware, e)

                If XtraMain.DelegateFunction.BgwFlashfirmware.CancellationPending Then

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    TimeSpanElapsed.ElapsedPending(Watch)
                    e.Cancel = True
                    Return
                Else

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    Watch.[Stop]()
                    TimeSpanElapsed.ElapsedTime(Watch)
                End If
            ElseIf Mediatek.Mediatek_tool.FlashOption.Method = "ErasefrpLOST" Then
                Dim str As String = """" & Mediatek.Mediatek_tool.ExSamsung.param & """"
                Mediatek.Authentication.Python.Command(String.Concat(New String() {"w", " ", "param", " ", str}), XtraMain.DelegateFunction.BgwFlashfirmware, e)

                If XtraMain.DelegateFunction.BgwFlashfirmware.CancellationPending Then

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    TimeSpanElapsed.ElapsedPending(Watch)
                    e.Cancel = True
                    Return
                Else

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    Watch.[Stop]()
                    TimeSpanElapsed.ElapsedTime(Watch)
                End If
            ElseIf Mediatek.Mediatek_tool.FlashOption.Method = "Universalformat" Then
                Mediatek.Authentication.Python.Command(String.Concat(New String() {"e", " ", "userdata"}), XtraMain.DelegateFunction.BgwFlashfirmware, e)

                If XtraMain.DelegateFunction.BgwFlashfirmware.CancellationPending Then

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    TimeSpanElapsed.ElapsedPending(Watch)
                    e.Cancel = True
                    Return
                Else

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    Watch.[Stop]()
                    TimeSpanElapsed.ElapsedTime(Watch)
                End If
            ElseIf Mediatek.Mediatek_tool.FlashOption.Method = "Universalmierse" Then
                Mediatek.Authentication.Python.Command(String.Concat(New String() {"e", " ", "persist"}), XtraMain.DelegateFunction.BgwFlashfirmware, e)

                If XtraMain.DelegateFunction.BgwFlashfirmware.CancellationPending Then

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    TimeSpanElapsed.ElapsedPending(Watch)
                    e.Cancel = True
                    Return
                Else

                    If File.Exists(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock) Then
                        File.Delete(Mediatek.Mediatek_tool.Mediatek.Preloaderunlock)
                    End If

                    Watch.[Stop]()
                    TimeSpanElapsed.ElapsedTime(Watch)
                End If
            End If
        End If
    End Sub

    Public Sub BgwFlashfirmware_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        ProcessKill()
    End Sub
    Private Class CSharpImpl
        <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
        Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
            target = value
            Return value
        End Function
    End Class

End Module
