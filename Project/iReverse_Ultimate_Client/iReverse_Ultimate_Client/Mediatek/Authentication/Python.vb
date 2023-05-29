Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Diagnostics
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports Microsoft.VisualBasic

Namespace Mediatek.Authentication
    Public Class Python
        Public Shared Property Authentication As String
        Public Shared Property Emmcid As String
        Private Shared Property Result As Object
        Public Shared DataTable As DataTable = New DataTable()

        Public Shared Sub Exploits(cmd As String, worker As BackgroundWorker, ee As DoWorkEventArgs)
            Dim startInfo As ProcessStartInfo = New ProcessStartInfo(Pythonfile.Python, cmd) With {
                .CreateNoWindow = True,
                .WindowStyle = ProcessWindowStyle.Hidden,
                .UseShellExecute = False,
                .Verb = "runas",
                .WorkingDirectory = Pythonfile.Broomwork,
                .RedirectStandardError = True,
                .RedirectStandardOutput = True
            }

            Using process As Process = Process.Start(startInfo)
                Console.WriteLine(cmd)
                process.BeginOutputReadLine()
                process.BeginErrorReadLine()

                If worker.CancellationPending Then
                    process.Dispose()
                    ee.Cancel = True
                    Return
                Else
                    AddHandler process.OutputDataReceived, Sub(sender As Object, e As DataReceivedEventArgs)
                                                               Dim args As String = If(e.Data, String.Empty)

                                                               If args <> String.Empty Then

                                                                   If args.Contains("Found device:") Then
                                                                       RichLogs(" Found devices : ", "black", True, False)
                                                                       RichLogs(Mediatek_list.Listport.MtkComport, "yellow", False, True)
                                                                   End If

                                                                   If args.Contains("Device hw code:") Then
                                                                       RichLogs(" hw code : ", "black", True, False)
                                                                       Dim text As String = args.Substring(args.IndexOf("]") + 2)
                                                                       RichLogs(text.Substring(text.IndexOf(":") + 2), "black", False, True)
                                                                   End If

                                                                   If args.Contains("Device hw sub code:") Then
                                                                       RichLogs(" hw sub code : ", "black", True, False)
                                                                       Dim text As String = args.Substring(args.IndexOf("]") + 2)
                                                                       RichLogs(text.Substring(text.IndexOf(":") + 2), "black", False, True)
                                                                   End If

                                                                   If args.Contains("Device hw version:") Then
                                                                       RichLogs(" hw version : ", "black", True, False)
                                                                       Dim text As String = args.Substring(args.IndexOf("]") + 2)
                                                                       RichLogs(text.Substring(text.IndexOf(":") + 2), "black", False, True)
                                                                   End If

                                                                   If args.Contains("Device sw version:") Then
                                                                       RichLogs(" sw version : ", "black", True, False)
                                                                       Dim text As String = args.Substring(args.IndexOf("]") + 2)
                                                                       RichLogs(text.Substring(text.IndexOf(":") + 2), "black", False, True)
                                                                   End If

                                                                   If args.Contains("Device secure boot:") Then
                                                                       RichLogs(" scb enable : ", "black", True, False)
                                                                       Dim text As String = args.Substring(args.IndexOf("]") + 2)
                                                                       RichLogs(text.Substring(text.IndexOf(":") + 2), "black", False, True)
                                                                   End If

                                                                   If args.Contains("Device serial link authorization:") Then
                                                                       RichLogs(" sla enable : ", "black", True, False)
                                                                       Dim text As String = args.Substring(args.IndexOf("]") + 2)
                                                                       RichLogs(text.Substring(text.IndexOf(":") + 2), "black", False, True)
                                                                   End If

                                                                   If args.Contains("Device download agent authorization:") Then
                                                                       RichLogs(" DAA enable : ", "black", True, False)
                                                                       Dim text As String = args.Substring(args.IndexOf("]") + 2)
                                                                       RichLogs(text.Substring(text.IndexOf(":") + 2), "black", False, True)
                                                                   End If

                                                                   If args.Contains("Disabling watchdog timer") Then
                                                                       RichLogs(" Disabling watchdog timer ...", "black", False, True)
                                                                   End If

                                                                   If args.Contains("Disabling protection") Then
                                                                       RichLogs(" Disabling protection ... ", "black", True, False)
                                                                   End If

                                                                   If args.Contains("Protection disabled") Then
                                                                       RichLogs("done ✓", "lime", True, True)
                                                                   End If

                                                                   If args.Contains("Insecure device") Then
                                                                       RichLogs(" Disabling protection ... ", "black", True, False)
                                                                       RichLogs(" Error", "red", True, True)
                                                                   End If

                                                                   If args.Contains("Found send_dword") Then
                                                                       RichLogs(vbLf & " please reconect device ...", "yellow", False, True)
                                                                   End If

                                                                   If args.Contains("Payload did not reply") Then
                                                                       RichLogs("failed", "red", False, True)
                                                                       worker.CancelAsync()
                                                                       ee.Cancel = True
                                                                       Return
                                                                   End If
                                                               End If
                                                           End Sub

                    process.WaitForExit()
                End If
            End Using
        End Sub

        Public Shared Sub Command(cmd As String, worker As BackgroundWorker, ee As DoWorkEventArgs)
            Dim str As String = String.Format("{0}/{1}", Path.GetDirectoryName(Pythonfile.mtk), Pythonfile.arg)
            Dim processStartInfo As ProcessStartInfo = New ProcessStartInfo(Pythonfile.Python, str & " " & cmd) With {
                .CreateNoWindow = True,
                .WindowStyle = ProcessWindowStyle.Hidden,
                .UseShellExecute = False,
                .Verb = "runas",
                .RedirectStandardError = True,
                .RedirectStandardOutput = True
            }

            Using process As Process = Process.Start(processStartInfo)
                Console.WriteLine(str & " " & cmd)
                process.BeginOutputReadLine()
                process.BeginErrorReadLine()

                If worker.CancellationPending Then
                    process.Dispose()
                    ee.Cancel = True
                    Return
                Else
                    AddHandler process.OutputDataReceived, Sub(sender As Object, e As DataReceivedEventArgs)
                                                               Dim text As String = If(e.Data, String.Empty)

                                                               If text <> String.Empty Then
                                                                   Console.WriteLine(text)

                                                                   If text.Contains("Preloader") Then

                                                                       If text.Contains("CPU") Then

                                                                           If Mediatek_list.Listport.MtkComport = "" Then
                                                                               RichLogs(" Found devices: ", "black", True, False)
                                                                               RichLogs("Found", "yellow", False, True)
                                                                           Else
                                                                               RichLogs(" Found devices: ", "black", True, False)
                                                                               RichLogs(Mediatek_list.Listport.MtkComport, "yellow", False, True)
                                                                           End If

                                                                           RichLogs(" Hardware Information ... ", "black", False, True)
                                                                           RichLogs(" Hardware: ", "black", True)
                                                                           RichLogs(text.Substring(text.IndexOf(":") + 2), "black", False, True)
                                                                       End If

                                                                       If text.Contains("WDT") Then
                                                                           RichLogs(" WDT: ", "black", True)
                                                                           RichLogs(text.Substring(text.IndexOf(":") + 2), "cyan", False, False)
                                                                       End If

                                                                       If text.Contains("Uart:") Then
                                                                           RichLogs(" Uart: ", "black", True)
                                                                           RichLogs(text.Substring(text.IndexOf(":") + 2), "yellow", False, True)
                                                                       End If

                                                                       If text.Contains("Brom payload") Then
                                                                           RichLogs(" Brom Addr: ", "black", True)
                                                                           RichLogs(text.Substring(text.IndexOf(":") + 2), "orange", False, False)
                                                                       End If

                                                                       If text.Contains("DA payload") Then
                                                                           RichLogs(" DA Addr: ", "black", True)
                                                                           RichLogs(text.Substring(text.IndexOf(":") + 2), "green", False, True)
                                                                       End If

                                                                       If text.Contains("CQ_DMA") Then
                                                                           RichLogs(" CQDMA Addr: ", "black", True)
                                                                           RichLogs(text.Substring(text.IndexOf(":") + 2), "cyan", False, False)
                                                                       End If

                                                                       If text.Contains("Var1") Then
                                                                           RichLogs(" Var Addr: ", "black", True)
                                                                           RichLogs(text.Substring(text.IndexOf(":") + 2), "fuchsia", False, True)
                                                                       End If

                                                                       If text.Contains("Disabling Watchdog") Then
                                                                           RichLogs(" Disabling watchdog timer ... ", "black", False, True)
                                                                       End If

                                                                       If text.Contains("ME_ID") Then
                                                                           RichLogs(" MEID: ", "black", True)
                                                                           RichLogs(text.Substring(text.IndexOf(":") + 2), "black", False, True)
                                                                       End If

                                                                       If text.Contains("SOC_ID") Then
                                                                           RichLogs(" SOCID: ", "black", True)
                                                                           RichLogs(text.Substring(text.IndexOf(":") + 2), "black", False, True)
                                                                       End If

                                                                       If text.Contains("Handshake failed") Then
                                                                           ee.Cancel = True
                                                                       End If
                                                                   End If

                                                                   If text.Contains("PLTools") Then

                                                                       If text.Contains("Loading payload") Then
                                                                           RichLogs(" Loading payload: ", "black", True)
                                                                           RichLogs(text.Substring(text.IndexOf(":") + 2).Replace(text.Substring(text.LastIndexOf(",")), ""), "black", False, True)
                                                                       End If

                                                                       If text.Contains("Successfully sent payload:") Then
                                                                           RichLogs(" done ✓", "lime", True, True)
                                                                       End If
                                                                   End If

                                                                   If text.Contains("Kamakiri") Then

                                                                       If text.Contains("Trying kamakiri2") Then
                                                                           RichLogs(" Disabling protection ... ", "black", True)
                                                                       End If
                                                                   End If

                                                                   If text.Contains("DAXFlash") Then

                                                                       If text.Contains("Uploading xflash") Then
                                                                           RichLogs(" Sending DA: ", "black", True)
                                                                           RichLogs(text.Substring(text.IndexOf(":") + 2) & " ... ", "black", True)
                                                                       End If

                                                                       If text.Contains("Successfully received DA") Then
                                                                           RichLogs("done ✓", "lime", True, True)
                                                                       End If

                                                                       If text.Contains("Error jumping to DA") Or text.Contains("Error on jumping to DA") Or text.Contains("Error on sending DA") Then
                                                                           RichLogs("failed", "red", False, True)
                                                                       End If

                                                                       If text.Contains("Sending emi data ...") Then
                                                                           RichLogs(" Sending EMI data ... ", "black", True)
                                                                       End If

                                                                       If text.Contains("Sending emi data succeeded") Then
                                                                           RichLogs("  done ✓", "lime", True, True)
                                                                       End If

                                                                       If text.Contains("Error on sending emi") Then
                                                                           RichLogs("failed", "red", False, True)
                                                                       End If

                                                                       If text.Contains("Uploading stage 2") Then
                                                                           RichLogs(" Uploading stage II ... ", "black", True)
                                                                       End If

                                                                       If text.Contains("Successfully uploaded stage 2") Then
                                                                           RichLogs("succes ✓✓", "cyan", False, True)
                                                                       End If

                                                                       If text.Contains("Error on sending data") Then
                                                                           RichLogs("failed", "red", False, True)
                                                                       End If

                                                                       If text.Contains("EMMC ID") Then
                                                                           RichLogs(" EMMCID" & vbTab & ": ", "black", True)
                                                                           RichLogs(text.Substring(text.IndexOf(":") + 2), "gold", False, True)
                                                                       End If

                                                                       If text.Contains("EMMC CID") Then
                                                                           Emmcid = text.Substring(text.IndexOf(":") + 2)
                                                                       End If

                                                                       If text.Contains("EMMC Boot1") Then
                                                                           RichLogs(" Boot1  : ", "black", True)
                                                                           RichLogs(GetFileCalc(ParseHexString(text.Substring(text.IndexOf(":") + 2)) / 1024.0), "yellow", False, False)
                                                                       End If

                                                                       If text.Contains("EMMC Boot2") Then
                                                                           RichLogs(" Boot2 : ", "black", True)
                                                                           RichLogs(GetFileCalc(ParseHexString(text.Substring(text.IndexOf(":") + 2)) / 1024.0), "fuchsia", False, False)
                                                                       End If

                                                                       If text.Contains("EMMC RPMB") Then
                                                                           RichLogs(" RPMB : ", "black", True)
                                                                           RichLogs(GetFileCalc(ParseHexString(text.Substring(text.IndexOf(":") + 2)) / 1024.0), "cyan", False, True)
                                                                       End If

                                                                       If text.Contains("EMMC USER") Then
                                                                           RichLogs(" Userarea: ", "black", True)
                                                                           RichLogs(GetFileCalc(ParseHexString(text.Substring(text.IndexOf(":") + 2)) / 1024.0), "black", False, True)
                                                                           RichLogs(" EMMC CID: ", "black", True)
                                                                           RichLogs(Emmcid, "black", False, True)
                                                                       End If

                                                                       If text.Contains("UFS ID") Then
                                                                           RichLogs(" UFSID" & vbTab & ": ", "black", True)
                                                                           RichLogs(text.Substring(text.IndexOf(":") + 2), "gold", False, True)
                                                                       End If

                                                                       If text.Contains("UFS CID") Then
                                                                           Emmcid = text.Substring(text.IndexOf(":") + 2)
                                                                       End If

                                                                       If text.Contains("UFS LU2 Size") Then
                                                                           RichLogs(" Boot1" & vbTab & ": ", "black", True)
                                                                           RichLogs(GetFileCalc(ParseHexString(text.Substring(text.IndexOf(":") + 2)) / 1024.0), "black", False, True)
                                                                       End If

                                                                       If text.Contains("UFS LU1 Size") Then
                                                                           RichLogs(" Boot2" & vbTab & ": ", "black", True)
                                                                           RichLogs(GetFileCalc(ParseHexString(text.Substring(text.IndexOf(":") + 2)) / 1024.0), "black", False, True)
                                                                       End If

                                                                       If text.Contains("UFS LU0 Size") Then
                                                                           RichLogs(" Userarea: ", "black", True)
                                                                           RichLogs(GetFileCalc(ParseHexString(text.Substring(text.IndexOf(":") + 2)) / 1024.0), "black", False, True)
                                                                           RichLogs(" UFS CID : ", "black", True)
                                                                           RichLogs(Emmcid, "black", False, True)
                                                                       End If
                                                                   End If

                                                                   If text.Contains("DA_handler") Then

                                                                       If text.Contains("Device in BROM mode") Then
                                                                           RichLogs(" Device connected in BROM mode ...", "black", False, True)
                                                                       End If

                                                                       If text.Contains("Trying to dump preloader") Then
                                                                           RichLogs(" Waiting dump preloader ...", "black", False, True)
                                                                       End If

                                                                       If text.Contains("No preloader given") Then
                                                                           RichLogs(" No preloader given form brom ...", "black", False, True)
                                                                       End If

                                                                       If text.Contains("trying dump preloader from ram") Then
                                                                           RichLogs(" trying dump preloader from ram ...", "black", False, True)
                                                                       End If
                                                                   End If

                                                                   If text.Contains("Successfully extracted preloader") Then
                                                                       RichLogs(" successfully dump : ", "black", True, False)
                                                                       Mediatek_tool.Mediatek.Preloaderunlock = text.Substring(text.IndexOf(":") + 2)
                                                                       RichLogs(Mediatek_tool.Mediatek.Preloaderunlock, "black", False, True)
                                                                   End If

                                                                   If text.Contains("DALegacy") Then

                                                                       If text.Contains("Uploading da") Then
                                                                           RichLogs(" Sending DA : ", "black", True)
                                                                       End If

                                                                       If text.Contains("Uploading legacy da") Then
                                                                           RichLogs(text.Substring(text.IndexOf(":") + 2) & " ... ", "black", True)
                                                                       End If

                                                                       If text.Contains("Got loader sync !") Then
                                                                           RichLogs(" done ✓", "lime", True, True)
                                                                       End If

                                                                       If text.Contains("Reading dram nand info") Then
                                                                           RichLogs(" Reading dram nand info ... ", "black", True)
                                                                       End If

                                                                       If text.Contains("Error on sending brom") Then
                                                                           RichLogs("failed", "red", False, True)
                                                                       End If

                                                                       If text.Contains("Successfully uploaded stage 2") Then
                                                                           RichLogs("done ✓", "lime", True, True)
                                                                       End If

                                                                       If text.Contains("Connected to preloader step1") Then
                                                                           RichLogs(" Connected to preloader ... ", "black", True, False)
                                                                       End If

                                                                       If text.Contains("Connected to preloader step2") Then
                                                                           RichLogs("success ✓✓", "cyan", False, True)
                                                                       End If
                                                                   End If

                                                                   If text.Contains("m_ext_ram_size =") Then
                                                                       RichLogs(" Ram : ", "black", True)
                                                                       RichLogs(GetFileCalc(ParseHexString(text.Substring(text.IndexOf("=") + 2)) / 1024.0), "orange", False, False)
                                                                   End If

                                                                   If text.Contains("m_emmc_") Then

                                                                       If text.Contains("m_emmc_boot1_size =") Then
                                                                           RichLogs(" Boot1 : ", "black", True)
                                                                           RichLogs(GetFileCalc(ParseHexString(text.Substring(text.IndexOf("=") + 2)) / 1024.0), "yellow", False, False)
                                                                       End If

                                                                       If text.Contains("m_emmc_boot2_size =") Then
                                                                           RichLogs(" Boot2 : ", "black", True)
                                                                           RichLogs(GetFileCalc(ParseHexString(text.Substring(text.IndexOf("=") + 2)) / 1024.0), "fuchsia", False, False)
                                                                       End If

                                                                       If text.Contains("m_emmc_rpmb_size =") Then
                                                                           RichLogs(" RPMB : ", "black", True)
                                                                           RichLogs(GetFileCalc(ParseHexString(text.Substring(text.IndexOf("=") + 2)) / 1024.0), "cyan", False, True)
                                                                       End If

                                                                       If text.Contains("m_emmc_ua_size =") Then
                                                                           RichLogs(" Userarea: ", "black", True)
                                                                           RichLogs(GetFileCalc(ParseHexString(text.Substring(text.IndexOf("=") + 2)) / 1024.0), "black", False, True)
                                                                       End If

                                                                       If text.Contains("m_emmc_cid =") Then
                                                                           RichLogs(" EMMC CID: ", "black", True)
                                                                           RichLogs(text.Substring(text.IndexOf("=") + 2), "black", False, True)
                                                                       End If
                                                                   End If

                                                                   If Mediatek_tool.FlashOption.Method = "Universalreadinfo" Then

                                                                       If text.Contains("Requesting available partitions") Then
                                                                           Progressbar1(0)
                                                                           Progressbar2(0)
                                                                       End If

                                                                       If text.Contains("Dumping partition") Then
                                                                           RichLogs(" Reading Device info ... ", "black", False, False)
                                                                           Progressbar2(0)
                                                                           For i As Integer = 1 To 10
                                                                               Progressbar2(i + 90)
                                                                           Next
                                                                           Return
                                                                       End If

                                                                       If text.Contains("Progress: |") Then
                                                                           Dim msg As String = text.Remove(0, text.IndexOf("| ")).Replace("| ", "").Replace(".", vbTab)
                                                                           Dim array2 As String() = msg.Split(CType(Nothing, String()), StringSplitOptions.RemoveEmptyEntries)
                                                                           Progressbar1(Integer.Parse(array2(0)))
                                                                           Mediatek_tool.FlashOption.progres = Integer.Parse(array2(0))
                                                                       End If

                                                                       If text.Contains("Dumped sector") Then
                                                                           Mediatek_tool.Android.AndroidUnpact(Path.GetFileName(Mediatek_tool.Sourcefile.Dumped), Path.GetDirectoryName(Mediatek_tool.Sourcefile.Andoidpath) & "\initrd\", worker, ee)
                                                                       End If
                                                                   End If

                                                                   If Mediatek_tool.FlashOption.Method = "UniversalUnlock" Then

                                                                       If text.Contains("DA Extensions successfully added") Then
                                                                           Progressbar1(0)
                                                                           Progressbar2(0)
                                                                           RichLogs(Environment.NewLine & " Unlocked Bootloader ... ", "black", True)
                                                                       End If

                                                                       If text.Contains("Done") Then

                                                                           For i As Integer = 1 To 10
                                                                               Progressbar2(i + 90)
                                                                           Next

                                                                       End If

                                                                       If text.Contains("Progress: |") Then
                                                                           Dim msg As String = text.Remove(0, text.IndexOf("| ")).Replace("| ", "").Replace(".", vbTab)
                                                                           Dim array2 As String() = msg.Split(CType(Nothing, String()), StringSplitOptions.RemoveEmptyEntries)
                                                                           Progressbar1(Integer.Parse(array2(0)))
                                                                       End If

                                                                       If text.Contains("Successfully wrote seccfg") Then
                                                                           RichLogs("done ✓", "chartreuse", True, True)
                                                                       End If

                                                                       If text.Contains("Error on writing seccfg") Or text.Contains("Couldn't detect existing seccfg") Or text.Contains("Unknown seccfg partition header") Then
                                                                           RichLogs(Environment.NewLine & " Unlocked Bootloader ... ", "black", True, False)
                                                                           RichLogs("failed", "red", False, True)
                                                                           worker.CancelAsync()
                                                                           ee.Cancel = True
                                                                           Return
                                                                       End If

                                                                       If text.Contains("Device has is either already unlocked") Then
                                                                           RichLogs(Environment.NewLine & " Devices already unlocked ...!  ", "gold", True, False)
                                                                           RichLogs(" [Process auto aborted !!!]", "orange", True, False)
                                                                           worker.CancelAsync()
                                                                           ee.Cancel = True
                                                                           Return
                                                                       End If
                                                                   End If

                                                                   If Mediatek_tool.FlashOption.Method = "Universalrelock" Then

                                                                       If text.Contains("DA Extensions successfully added") Then
                                                                           Progressbar1(0)
                                                                           Progressbar2(0)
                                                                           RichLogs(Environment.NewLine & " locked Bootloader ... ", "black", True)
                                                                       End If

                                                                       If text.Contains("Done") Then

                                                                           For i As Integer = 1 To 10
                                                                               Progressbar2(i + 90)
                                                                           Next
                                                                       End If

                                                                       If text.Contains("Progress: |") Then
                                                                           Dim msg As String = text.Remove(0, text.IndexOf("| ")).Replace("| ", "").Replace(".", vbTab)
                                                                           Dim array2 As String() = msg.Split(CType(Nothing, String()), StringSplitOptions.RemoveEmptyEntries)
                                                                           Progressbar1(Integer.Parse(array2(0)))
                                                                       End If

                                                                       If text.Contains("Successfully wrote seccfg") Then
                                                                           RichLogs("done ✓", "chartreuse", True, True)
                                                                       End If

                                                                       If text.Contains("Error on writing seccfg") Or text.Contains("Couldn't detect existing seccfg") Or text.Contains("Unknown seccfg partition header") Then
                                                                           RichLogs(Environment.NewLine & " locked Bootloader ... ", "black", True, False)
                                                                           RichLogs("failed", "red", False, True)
                                                                           worker.CancelAsync()
                                                                           ee.Cancel = True
                                                                           Return
                                                                       End If
                                                                   End If

                                                                   If Mediatek_tool.FlashOption.Method = "Universalformat" Then

                                                                       If text.Contains("check partition") Then
                                                                           Progressbar1(0)
                                                                           Progressbar2(0)
                                                                           RichLogs(Environment.NewLine & " Formatted" & vbTab & ": ", "black", True)
                                                                           RichLogs("Data", "darkorange", False, True)
                                                                       End If

                                                                       If text.Contains("done") Then
                                                                           For i As Integer = 1 To 10
                                                                               Progressbar2(i + 90)
                                                                           Next
                                                                       End If

                                                                       If text.Contains("Formatting addr:") Then
                                                                           RichLogs(" Addres_hex" & vbTab & ": ", "black", True)
                                                                           RichLogs(text.Substring(text.IndexOf(":") + 2).Replace(text.Substring(text.LastIndexOf("w")), "").Replace(" ", ""), "gold", False, True)
                                                                       End If

                                                                       If text.Contains("with length:") Then
                                                                           RichLogs(" Lenght_hex" & vbTab & ": ", "black", True)
                                                                           RichLogs(text.Substring(text.LastIndexOf(":") + 2), "gold", False, True)
                                                                       End If

                                                                       If text.Contains("Format done") Then
                                                                           For i As Integer = 1 To 10
                                                                               Progressbar2(i + 90)
                                                                           Next
                                                                           RichLogs(Environment.NewLine & " Formatted data ... ", "black", True)
                                                                           RichLogs("done ✓", "lime", True, True)
                                                                       End If

                                                                       If text.Contains("Couldn't detect partition") Then
                                                                           RichLogs(Environment.NewLine & " Formatted data ... ", "black", True)
                                                                           RichLogs("failed", "red", False, True)
                                                                           worker.CancelAsync()
                                                                           ee.Cancel = True
                                                                           Return
                                                                       End If
                                                                   End If

                                                                   If Mediatek_tool.FlashOption.Method = "Universalfrp" Then

                                                                       If text.Contains("check partition") Then
                                                                           Progressbar1(0)
                                                                           Progressbar2(0)
                                                                           RichLogs(Environment.NewLine & " Errasing" & vbTab & ": ", "black", True)
                                                                           RichLogs("FRP", "darkorange", False, True)
                                                                       End If

                                                                       If text.Contains("done") Then
                                                                           For i As Integer = 1 To 10
                                                                               Progressbar2(i + 90)
                                                                           Next
                                                                       End If

                                                                       If text.Contains("Formatting addr:") Then
                                                                           RichLogs(" Addres_hex" & vbTab & ": ", "black", True)
                                                                           RichLogs(text.Substring(text.IndexOf(":") + 2).Replace(text.Substring(text.LastIndexOf("w")), "").Replace(" ", ""), "gold", False, True)
                                                                       End If

                                                                       If text.Contains("with length:") Then
                                                                           RichLogs(" Lenght_hex" & vbTab & ": ", "black", True)
                                                                           RichLogs(text.Substring(text.LastIndexOf(":") + 2), "gold", False, True)
                                                                       End If

                                                                       If text.Contains("Format done") Then
                                                                           For i As Integer = 1 To 10
                                                                               Progressbar1(i + 90)
                                                                           Next
                                                                           RichLogs(Environment.NewLine & " Errasing FRP protection ... ", "black", True)
                                                                           RichLogs("done ✓", "lime", True, True)
                                                                       End If

                                                                       If text.Contains("Couldn't detect partition") Then
                                                                           RichLogs(Environment.NewLine & " Errasing FRP protection ... ", "black", True)
                                                                           RichLogs("failed", "red", False, True)
                                                                           worker.CancelAsync()
                                                                           ee.Cancel = True
                                                                           Return
                                                                       End If
                                                                   End If

                                                                   If Mediatek_tool.FlashOption.Method = "Universalmierse" Then

                                                                       If text.Contains("check partition") Then
                                                                           Progressbar1(0)
                                                                           Progressbar2(0)
                                                                           RichLogs(Environment.NewLine & " Errasing" & vbTab & ": ", "black", True)
                                                                           RichLogs("Mi Account", "darkorange", False, True)
                                                                       End If

                                                                       If text.Contains("done") Then
                                                                           For i As Integer = 1 To 10
                                                                               Progressbar2(i + 90)
                                                                           Next
                                                                       End If

                                                                       If text.Contains("Formatting addr:") Then
                                                                           RichLogs(" Addres_hex" & vbTab & ": ", "black", True)
                                                                           RichLogs(text.Substring(text.IndexOf(":") + 2).Replace(text.Substring(text.LastIndexOf("w")), "").Replace(" ", ""), "gold", False, True)
                                                                       End If

                                                                       If text.Contains("with length:") Then
                                                                           RichLogs(" Lenght_hex" & vbTab & ": ", "black", True)
                                                                           RichLogs(text.Substring(text.LastIndexOf(":") + 2), "gold", False, True)
                                                                       End If

                                                                       If text.Contains("Format done") Then
                                                                           For i As Integer = 1 To 10
                                                                               Progressbar1(i + 90)
                                                                           Next
                                                                           RichLogs(Environment.NewLine & " Erase Mi Account ... ", "black", True)
                                                                           RichLogs("done ✓", "lime", True, True)
                                                                       End If

                                                                       If text.Contains("Couldn't detect partition") Then
                                                                           RichLogs(Environment.NewLine & " Erase Mi Account ... ", "black", True)
                                                                           RichLogs("failed", "red", False, True)
                                                                           worker.CancelAsync()
                                                                           ee.Cancel = True
                                                                           Return
                                                                       End If
                                                                   End If

                                                                   If Mediatek_tool.FlashOption.Method = "Readgpttable" Then
                                                                       DataTable = New DataTable()
                                                                       DataTable.Rows.Clear()
                                                                       DataTable.Columns.Add("Partition")
                                                                       DataTable.Columns.Add("Offset")
                                                                       DataTable.Columns.Add("Length")
                                                                       DataTable.Columns.Add("Flags")
                                                                       DataTable.Columns.Add("UUID")
                                                                       DataTable.Columns.Add("Type")
                                                                       Dim dataRow As DataRow = DataTable.NewRow()
                                                                       DataTable.Rows.Clear()

                                                                       If text.Contains("GPT Table:") Then
                                                                           Mediatek_list.Datarows.MtkDataview.Clear()
                                                                           RichLogs(Environment.NewLine & " Reading gpt table ... ", "gold", True)
                                                                       End If

                                                                       If text.Contains("PartName") Then
                                                                           Dim array As String() = text.Split(New Char() {":"c})

                                                                           For i As Integer = 0 To array.Length - 1
                                                                               Dim text2 As String = GetFirstFromSplit(array(i), ","c).Replace("PartName", "").Replace("Flags", ",").Replace("UUID", "").Replace("Type", "").Replace(" ", "").Trim().TrimStart(Nothing).TrimEnd(Nothing)
                                                                               dataRow(i) = text2.Replace("Offset", "").Replace("Length", "").Replace(",0x", "0x")
                                                                               Console.WriteLine(text2.Replace("Offset", "").Replace("Length", "").Replace(",0x", "0x"))
                                                                           Next

                                                                           DataTable.Rows.Add(dataRow)

                                                                           If DataTable.Rows.Count > 0 Then

                                                                               For i As Integer = 0 To DataTable.Rows.Count - 1
                                                                                   ''MtkFlash.SharedUI.QlMGPTGrid.Invoke(Sub()
                                                                                   ''MtkFlash.SharedUI.QlMGPTGrid.Rows.Add(False, DataTable.Rows(i)("Partition").ToString(), DataTable.Rows(i)("Offset").ToString(), DataTable.Rows(i)("Length").ToString(), "double click ...", DataTable.Rows(i)("Flags").ToString(), DataTable.Rows(i)("UUID").ToString())
                                                                                   ''End Sub)
                                                                               Next
                                                                           End If
                                                                       End If

                                                                       If text.Contains("Total disk size") Then
                                                                           RichLogs("done ✓", "lime", True, True)
                                                                       End If
                                                                   End If

                                                                   If Mediatek_tool.FlashOption.Method = "Readinfogpt" OrElse Mediatek_tool.FlashOption.Method = "READ INFO" Then

                                                                       If text.Contains("Requesting available partitions") Then
                                                                           Progressbar1(0)
                                                                           Progressbar2(0)
                                                                       End If

                                                                       If text.Contains("Dumping partition") Then
                                                                           For i As Integer = 1 To 10
                                                                               Progressbar2(i + 90)
                                                                           Next
                                                                           RichLogs(" Reading Device info ... ", "black", False, False)
                                                                           Return
                                                                       End If

                                                                       If text.Contains("Progress: |") Then
                                                                           Dim msg As String = text.Remove(0, text.IndexOf("| ")).Replace("| ", "").Replace(".", vbTab)
                                                                           Dim array2 As String() = msg.Split(CType(Nothing, String()), StringSplitOptions.RemoveEmptyEntries)
                                                                           Progressbar1(Integer.Parse(array2(0)))
                                                                           Mediatek_tool.FlashOption.progres = Integer.Parse(array2(0))
                                                                       End If

                                                                       If text.Contains("Dumped sector") Then
                                                                           Dim texts As String = Path.GetDirectoryName(Mediatek_tool.Sourcefile.Andoidpath) & "\initrd\"
                                                                           Mediatek_tool.Android.AndroidUnpact(Path.GetFileName(Mediatek_tool.Sourcefile.Dumped), texts, worker, ee)
                                                                       End If
                                                                   End If

                                                                   If Mediatek_tool.FlashOption.Method = "Erasegpttable" Then

                                                                       If text.Contains("check partition") Then
                                                                           RichLogs(vbLf & " Formated partition Sector count ... " & Environment.NewLine, "black", False, True)
                                                                       End If

                                                                       If text.Contains("Formatted sector:") Then
                                                                           RichLogs(" Addres_hex : ", "black", True)
                                                                           RichLogs("0x" & text.Substring(text.IndexOf(":") + 2).Replace(text.Substring(text.LastIndexOf("w")), "").Replace(" ", ""), "gold", True)
                                                                           RichLogs(" Lenght_hex : ", "black", True)
                                                                           RichLogs("0x" & text.Substring(text.LastIndexOf(":") + 2).Replace(".", ""), "gold", True)
                                                                           RichLogs(" ... ", "black", True)
                                                                       End If

                                                                       If text.Contains("Format done") Then
                                                                           RichLogs("done ✓", "lime", True, True)
                                                                       End If

                                                                       If text.Contains("All partitions formatted") Then
                                                                           RichLogs(vbLf & " Formated all partition success ...!", "darkorange", False, True)
                                                                       End If
                                                                   End If

                                                                   If Mediatek_tool.FlashOption.Method = "Readpartitiontable" Then

                                                                       If text.Contains("Requesting available partitions") Then
                                                                           RichLogs(vbLf & " Requesting  partitions ... " & Environment.NewLine, "black", False, True)
                                                                       End If

                                                                       If text.Contains("Dumping partition") Then
                                                                           RichLogs(" Read partition : ", "black", True)
                                                                           text = text.Substring(text.IndexOf("""") - 1).Replace("""", "")
                                                                           RichLogs(text, "darkorange", True)
                                                                           RichLogs(" ... ", "black", True)
                                                                       End If

                                                                       If text.Contains("Dumped sector:") Then
                                                                           RichLogs("done ✓", "lime", True, True)
                                                                       End If

                                                                       If text.Contains("All partitions were dumped") Then
                                                                           RichLogs(vbLf & " Read all partition success ...!", "darkorange", False, True)
                                                                       End If

                                                                       If text.Contains("Done |") Then
                                                                           For i As Integer = 1 To 10
                                                                               Progressbar2(i + 90)
                                                                           Next
                                                                       End If

                                                                       If text.Contains("Progress: |") Then
                                                                           Dim msg As String = text.Remove(0, text.IndexOf("| ")).Replace("| ", "").Replace(".", vbTab)
                                                                           Dim array2 As String() = msg.Split(CType(Nothing, String()), StringSplitOptions.RemoveEmptyEntries)
                                                                           Progressbar1(array2(0))
                                                                       End If
                                                                   End If



                                                                   If Mediatek_tool.FlashOption.Method = "Customflash" Then

                                                                       If text.Contains("Checking available partitions") Then
                                                                           RichLogs(vbLf & " Checking partitions ..." & Environment.NewLine, "black", False, True)
                                                                       End If

                                                                       If text.Contains("Writing partition") Then
                                                                           Dim text2 As String = text.Substring(text.IndexOf(":") + 2)
                                                                           RichLogs(" Writing partition : ", "black", True)
                                                                           RichLogs(Path.GetFileName(text2), "gold", True)
                                                                           RichLogs(" ... ", "black", True)
                                                                       End If

                                                                       If text.Contains("Wrote") Then
                                                                           RichLogs("done ✓", "lime", True, True)
                                                                       End If

                                                                       If text.Contains("Data ack failed for sdmmc_write_data") Then
                                                                           RichLogs("failed", "red", False, True)
                                                                       End If

                                                                       If text.Contains("Done |") Then

                                                                           For i As Integer = 1 To 10
                                                                               Progressbar2(i + 90)
                                                                           Next
                                                                       End If

                                                                       If text.Contains("Progress: |") Then
                                                                           Dim msg As String = text.Remove(0, text.IndexOf("| ")).Replace("| ", "").Replace(".", vbTab)
                                                                           Dim array2 As String() = msg.Split(CType(Nothing, String()), StringSplitOptions.RemoveEmptyEntries)
                                                                           Progressbar1(Integer.Parse(array2(0)))
                                                                       End If
                                                                   End If






                                                                   If Mediatek_tool.FlashOption.Method = "Writepartitiontable" Then

                                                                       If text.Contains("Checking available partitions") Then
                                                                           RichLogs(vbLf & " Checking partitions ..." & Environment.NewLine, "black", False, True)
                                                                       End If

                                                                       If text.Contains("Writing partition") Then
                                                                           Dim text2 As String = text.Substring(text.IndexOf(":") + 2)
                                                                           RichLogs(" Writing partition : ", "black", True)
                                                                           RichLogs(Path.GetFileName(text2), "gold", True)
                                                                           RichLogs(" ... ", "black", True)
                                                                       End If

                                                                       If text.Contains("Wrote") Then
                                                                           RichLogs("done ✓", "lime", True, True)
                                                                       End If

                                                                       If text.Contains("Data ack failed for sdmmc_write_data") Then
                                                                           RichLogs("failed", "red", False, True)
                                                                       End If

                                                                       If text.Contains("Done |") Then

                                                                           For i As Integer = 1 To 10
                                                                               Progressbar2(i + 90)
                                                                           Next

                                                                       End If

                                                                       If text.Contains("Progress: |") Then
                                                                           Dim msg As String = text.Remove(0, text.IndexOf("| ")).Replace("| ", "").Replace(".", vbTab)
                                                                           Dim array2 As String() = msg.Split(CType(Nothing, String()), StringSplitOptions.RemoveEmptyEntries)
                                                                           Progressbar1(Integer.Parse(array2(0)))
                                                                       End If
                                                                   End If

                                                                   If Mediatek_tool.FlashOption.Method = "Bacupnvram" Then

                                                                       If text.Contains("Requesting available partitions") Then
                                                                           Progressbar1(0)
                                                                           Progressbar2(0)
                                                                           RichLogs(vbLf & " Requesting  partitions ... " & Environment.NewLine, "black", False, True)
                                                                       End If

                                                                       If text.Contains("Dumping partition") Then
                                                                           For i As Integer = 1 To 10
                                                                               Progressbar2(i + 90)
                                                                           Next
                                                                           RichLogs(" Read partition : ", "black", True)
                                                                           text = text.Substring(text.IndexOf("""") - 1).Replace("""", "")
                                                                           RichLogs(text, "darkorange", True)
                                                                           RichLogs(" ... ", "black", True)
                                                                       End If

                                                                       If text.Contains("Couldn't detect partition:") Then
                                                                           RichLogs(" Read partition : ", "black", True)
                                                                           text = text.Substring(text.IndexOf("n:") + 2).Replace("""", "")
                                                                           RichLogs(text, "darkorange", True)
                                                                           RichLogs(" ... ", "black", True)
                                                                           RichLogs(" out suport ", "red", False, True)
                                                                       End If

                                                                       If text.Contains("Dumped sector:") Then
                                                                           RichLogs("done ✓", "lime", True, True)
                                                                       End If

                                                                       If text.Contains("Progress: |") Then
                                                                           Dim msg As String = text.Remove(0, text.IndexOf("| ")).Replace("| ", "").Replace(".", vbTab)
                                                                           Dim array2 As String() = msg.Split(CType(Nothing, String()), StringSplitOptions.RemoveEmptyEntries)
                                                                           Progressbar1(Integer.Parse(array2(0)))
                                                                       End If
                                                                   End If

                                                                   If Mediatek_tool.FlashOption.Method = "Erasenvram" Then

                                                                       If text.Contains("check partition") Then
                                                                           RichLogs(vbLf & " Formated partition Sector count ... " & Environment.NewLine, "black", False, True)
                                                                       End If

                                                                       If text.Contains("Formatted sector") Then
                                                                           RichLogs(" Addres_hex : ", "black", True)
                                                                           RichLogs("0x" & text.Substring(text.IndexOf(":") + 2).Replace(text.Substring(text.LastIndexOf("w")), "").Replace(" ", ""), "gold", True)
                                                                           RichLogs(" Lenght_hex : ", "black", True)
                                                                           RichLogs("0x" & text.Substring(text.LastIndexOf(":") + 2), "gold", True)
                                                                           RichLogs(" ... ", "black", True)
                                                                       End If

                                                                       If text.Contains("Couldn't detect partition:") Then
                                                                           RichLogs(" Addres partition : ", "black", True)
                                                                           text = text.Substring(text.IndexOf("n:") + 2).Replace("""", "")
                                                                           RichLogs(text, "darkorange", True)
                                                                           RichLogs(" ... ", "black", True)
                                                                           RichLogs(" not found ", "red", False, True)
                                                                       End If

                                                                       If text.Contains("Format done") Then
                                                                           RichLogs("done ✓", "lime", True, True)
                                                                       End If

                                                                       If text.Contains("All partitions formatted") Then
                                                                           RichLogs(vbLf & " Formated all partition success ...!", "darkorange", False, True)
                                                                       End If
                                                                   End If

                                                                   If Mediatek_tool.FlashOption.Method = "Restorenvram" Then

                                                                       If text.Contains("Checking available partitions") Then
                                                                           Progressbar1(0)
                                                                           Progressbar2(0)
                                                                           RichLogs(vbLf & " Checking partitions ..." & Environment.NewLine, "black", False, True)
                                                                       End If

                                                                       If text.Contains("Writing partition") Then
                                                                           Dim text2 As String = text.Substring(text.IndexOf(":") + 2)
                                                                           RichLogs(" Writing partition : ", "black", True)
                                                                           RichLogs(Path.GetFileName(text2), "gold", True)
                                                                           RichLogs(" ... ", "black", True)
                                                                       End If

                                                                       If text.Contains("Wrote") Then
                                                                           RichLogs("done ✓", "lime", True, True)
                                                                       End If

                                                                       If text.Contains("Data ack failed for sdmmc_write_data") Then
                                                                           RichLogs("failed", "red", False, True)
                                                                       End If

                                                                       If text.Contains("Done |") Then
                                                                           For i As Integer = 1 To 10
                                                                               Progressbar2(i + 90)
                                                                           Next
                                                                       End If

                                                                       If text.Contains("Progress: |") Then
                                                                           Dim msg As String = text.Remove(0, text.IndexOf("| ")).Replace("| ", "").Replace(".", vbTab)
                                                                           Dim array2 As String() = msg.Split(CType(Nothing, String()), StringSplitOptions.RemoveEmptyEntries)
                                                                           Progressbar1(Integer.Parse(array2(0)))
                                                                       End If
                                                                   End If

                                                                   If Mediatek_tool.FlashOption.Method = "ReadRPMB" Then

                                                                       If text.Contains("DA Extensions successfully added") Then
                                                                           Progressbar1(0)
                                                                           Progressbar2(0)
                                                                           RichLogs(vbLf & " Reading rpmbkey ... ", "black", True, False)
                                                                       End If

                                                                       If text.Contains("Done |") Then
                                                                           For i As Integer = 1 To 10
                                                                               Progressbar2(i + 90)
                                                                           Next
                                                                       End If

                                                                       If text.Contains("Progress: |") Then
                                                                           Dim msg As String = text.Remove(0, text.IndexOf("| ")).Replace("| ", "").Replace(".", vbTab)
                                                                           Dim array2 As String() = msg.Split(CType(Nothing, String()), StringSplitOptions.RemoveEmptyEntries)
                                                                           Progressbar1(Integer.Parse(array2(0)))
                                                                       End If

                                                                       If text.Contains("Done reading rpmb") Then
                                                                           RichLogs("done ✓", "lime", True, True)
                                                                       End If

                                                                       If text.Contains("Couldn't read rpmb") Then
                                                                           RichLogs("failed", "red", False, True)
                                                                           process.Close()
                                                                           worker.CancelAsync()
                                                                           ee.Cancel = True
                                                                       End If

                                                                       If text.Contains("cannot run read rpmb") Then
                                                                           RichLogs(vbLf & " Reading rpmbkey ... ", "black", True)
                                                                           RichLogs("failed", "red", False, True)
                                                                           process.Close()
                                                                           worker.CancelAsync()
                                                                           ee.Cancel = True
                                                                       End If
                                                                   End If

                                                                   If Mediatek_tool.FlashOption.Method = "WriteRPMB" Then

                                                                       If text.Contains("Generating sej rpmbkey") Then
                                                                           RichLogs(vbLf & " Getting writen rpmbkey ... ", "black", True, False)
                                                                       End If

                                                                       If text.Contains("Done reading writing") Then
                                                                           RichLogs("done ✓", "lime", True, True)
                                                                       End If

                                                                       If text.Contains("Couldn't write rpmb") Then
                                                                           RichLogs("failed", "red", False, True)
                                                                           process.Close()
                                                                           worker.CancelAsync()
                                                                           ee.Cancel = True
                                                                       End If

                                                                       If text.Contains("cannot run write rpmb cmd") Then
                                                                           RichLogs(vbLf & " Getting writen rpmbkey ... ", "black", True)
                                                                           RichLogs("failed", "red", False, True)
                                                                           process.Close()
                                                                           worker.CancelAsync()
                                                                           ee.Cancel = True
                                                                       End If
                                                                   End If

                                                                   If Mediatek_tool.FlashOption.Method = "EraseRPMB" Then

                                                                       If text.Contains("Generating sej rpmbkey") Then
                                                                           RichLogs(vbLf & " Getting erase rpmbkey ... ", "black", True, False)
                                                                       End If

                                                                       If text.Contains("Done erasing rpmb") Then
                                                                           RichLogs("done ✓", "lime", True, True)
                                                                       End If

                                                                       If text.Contains("Couldn't erase rpmb") Then
                                                                           RichLogs("failed", "red", False, True)
                                                                           process.Close()
                                                                           worker.CancelAsync()
                                                                           ee.Cancel = True
                                                                       End If

                                                                       If text.Contains("cannot run erase rpmb") Then
                                                                           RichLogs(vbLf & " Getting erase rpmbkey ... ", "black", True)
                                                                           RichLogs("failed", "red", False, True)
                                                                           process.Close()
                                                                           worker.CancelAsync()
                                                                           ee.Cancel = True
                                                                       End If
                                                                   End If

                                                                   If Mediatek_tool.FlashOption.Method = "EraseSAMfrp" Then

                                                                       If text.Contains("check partition") Then
                                                                           Progressbar1(0)
                                                                           Progressbar2(0)
                                                                           RichLogs(Environment.NewLine & " Errasing" & vbTab & ": ", "black", True)
                                                                           RichLogs("FRP", "darkorange", False, True)
                                                                       End If

                                                                       If text.Contains("done") Then
                                                                           For i As Integer = 1 To 10
                                                                               Progressbar2(i + 90)
                                                                           Next
                                                                       End If

                                                                       If text.Contains("Formatting addr:") Then
                                                                           RichLogs(" Addres_hex" & vbTab & ": ", "black", True)
                                                                           RichLogs(text.Substring(text.IndexOf(":") + 2).Replace(text.Substring(text.LastIndexOf("w")), "").Replace(" ", ""), "gold", False, True)
                                                                       End If

                                                                       If text.Contains("with length:") Then
                                                                           RichLogs(" Lenght_hex" & vbTab & ": ", "black", True)
                                                                           RichLogs(text.Substring(text.LastIndexOf(":") + 2), "gold", False, True)
                                                                       End If

                                                                       If text.Contains("Format done") Then

                                                                           For i As Integer = 1 To 10
                                                                               Progressbar1(i + 90)
                                                                           Next
                                                                           RichLogs(Environment.NewLine & " Errasing FRP protection ... ", "black", True)
                                                                           RichLogs("done ✓", "lime", True, True)
                                                                       End If

                                                                       If text.Contains("Couldn't detect partition") Then
                                                                           RichLogs(Environment.NewLine & " Errasing FRP protection ... ", "black", True)
                                                                           RichLogs("failed", "red", False, True)
                                                                           worker.CancelAsync()
                                                                           ee.Cancel = True
                                                                           Return
                                                                       End If
                                                                   End If

                                                                   If Mediatek_tool.FlashOption.Method = "ErasefrpOEM" Then

                                                                       If text.Contains("Checking available partitions") Then
                                                                           RichLogs(vbLf & " Checking partitions ..." & Environment.NewLine, "black", False, True)
                                                                       End If

                                                                       If text.Contains("Writing partition") Then
                                                                           RichLogs(" Erase partition : ", "black", True)
                                                                           RichLogs("FRP [OEM]", "gold", True)
                                                                           RichLogs(" ... ", "black", True)
                                                                       End If

                                                                       If text.Contains("Wrote") Then
                                                                           RichLogs("done ✓", "lime", True, True)
                                                                       End If

                                                                       If text.Contains("Data ack failed for sdmmc_write_data") Then
                                                                           RichLogs("failed", "red", False, True)
                                                                       End If

                                                                       If text.Contains("Done |") Then

                                                                           For i As Integer = 1 To 10
                                                                               Progressbar2(i + 90)
                                                                           Next
                                                                       End If

                                                                       If text.Contains("Progress: |") Then
                                                                           Dim msg As String = text.Remove(0, text.IndexOf("| ")).Replace("| ", "").Replace(".", vbTab)
                                                                           Dim array2 As String() = msg.Split(CType(Nothing, String()), StringSplitOptions.RemoveEmptyEntries)
                                                                           Progressbar1(Integer.Parse(array2(0)))
                                                                       End If

                                                                       If text.Contains("Couldn't detect partition") Then
                                                                           RichLogs(" Erase partition : ", "black", True)
                                                                           RichLogs("FRP [OEM]", "gold", True)
                                                                           RichLogs(" ... ", "black", True)
                                                                           RichLogs("failed", "red", False, True)
                                                                           process.Close()
                                                                           worker.CancelAsync()
                                                                           ee.Cancel = True
                                                                       End If
                                                                   End If

                                                                   If Mediatek_tool.FlashOption.Method = "ErasefrpLOST" Then

                                                                       If text.Contains("Checking available partitions") Then
                                                                           RichLogs(vbLf & " Checking partitions ..." & Environment.NewLine, "black", False, True)
                                                                       End If

                                                                       If text.Contains("Writing partition") Then
                                                                           RichLogs(" Erase partition : ", "black", True)
                                                                           RichLogs("FRP LOST DATA", "gold", True)
                                                                           RichLogs(" ... ", "black", True)
                                                                       End If

                                                                       If text.Contains("Wrote") Then
                                                                           RichLogs("done ✓", "lime", True, True)
                                                                       End If

                                                                       If text.Contains("Data ack failed for sdmmc_write_data") Then
                                                                           RichLogs("failed", "red", False, True)
                                                                       End If

                                                                       If text.Contains("Done |") Then
                                                                           For i As Integer = 1 To 10
                                                                               Progressbar2(i + 90)
                                                                           Next
                                                                       End If

                                                                       If text.Contains("Progress: |") Then
                                                                           Dim msg As String = text.Remove(0, text.IndexOf("| ")).Replace("| ", "").Replace(".", vbTab)
                                                                           Dim array2 As String() = msg.Split(CType(Nothing, String()), StringSplitOptions.RemoveEmptyEntries)
                                                                           Progressbar1(Integer.Parse(array2(0)))
                                                                       End If

                                                                       If text.Contains("Couldn't detect partition") Then
                                                                           RichLogs(" Erase partition : ", "black", True)
                                                                           RichLogs("FRP LOST DATA", "gold", True)
                                                                           RichLogs(" ... ", "black", True)
                                                                           RichLogs("failed", "red", False, True)
                                                                           process.Close()
                                                                           worker.CancelAsync()
                                                                           ee.Cancel = True
                                                                       End If
                                                                   End If

                                                                   If Mediatek_tool.FlashOption.Method = "readfirmware" Then

                                                                       If text.Contains(" DA Extensions successfully added") Then
                                                                           Progressbar1(0)
                                                                           Progressbar2(0)
                                                                           RichLogs(" Getting read data  ... ", "black", False, True)
                                                                           For i As Integer = 1 To 10
                                                                               Progressbar2(i + 90)
                                                                           Next
                                                                       End If

                                                                       If text.Contains("Dumping partition") Then
                                                                           RichLogs(" read partition : ", "black", True, False)
                                                                           RichLogs(text.Substring(text.LastIndexOf("\") + 1), "orange", True, False)
                                                                           RichLogs(" ... ", "black", True, False)
                                                                       End If

                                                                       If text.Contains("Dumped partition") Then
                                                                           RichLogs("done ✓", "lime", True, True)
                                                                       End If

                                                                       If text.Contains("Progress: |") Then
                                                                           Dim msg As String = text.Remove(0, text.IndexOf("| ")).Replace("| ", "").Replace(".", vbTab)
                                                                           Dim array2 As String() = msg.Split(CType(Nothing, String()), StringSplitOptions.RemoveEmptyEntries)
                                                                           Progressbar1(Integer.Parse(array2(0)))
                                                                       End If
                                                                   End If
                                                               End If
                                                           End Sub

                    AddHandler process.ErrorDataReceived, Sub(sender As Object, e As DataReceivedEventArgs)
                                                              Console.WriteLine(e.Data)
                                                              Dim text As String = If(e.Data, String.Empty)

                                                              If text <> String.Empty Then

                                                                  If e.Data.Contains("struct.error:") Then
                                                                      RichLogs(Environment.NewLine & vbLf & " | USB Connection Failed ...! | ", "red", False, True)
                                                                      ee.Cancel = True
                                                                  End If
                                                              End If
                                                          End Sub
                End If

                process.WaitForExit()
            End Using
        End Sub

        Public Shared Function ParseHexString(hexNumber As String) As Double
            hexNumber = hexNumber.Replace("x", String.Empty)
            Dim result As Long = Nothing
            Long.TryParse(hexNumber, NumberStyles.HexNumber, Nothing, result)
            Return result
        End Function

        Public Shared Function GetFileCalc(byteCount As Double) As String
            Dim size As String = "0 Bytes"

            If byteCount >= 1073741824.0 Then
                size = String.Format("{0:##.##}", byteCount / 1073741824.0) & " TB"
            ElseIf byteCount >= 1048576.0 Then
                size = String.Format("{0:##.##}", byteCount / 1048576.0) & " GB"
            ElseIf byteCount >= 1024.0 Then
                size = String.Format("{0:##.##}", byteCount / 1024.0) & " MB"
            ElseIf byteCount > 0.0 AndAlso byteCount < 1024.0 Then
                size = byteCount.ToString() & " KB"
            End If

            Return size
        End Function

        Private Shared Function GetFirstFromSplit(input As String, delimiter As Char) As String
            Dim num As Integer = input.IndexOf(delimiter)
            Return If(num = -1, input, input.Substring(0, num))
        End Function

        Public Shared Sub Adbpython(cmd As String, worker As BackgroundWorker, e As DoWorkEventArgs)
            Dim process As Process = New Process With {
                .StartInfo = New ProcessStartInfo With {
                    .FileName = Pythonfile.Python,
                    .Arguments = cmd,
                    .Verb = "runas",
                    .UseShellExecute = False,
                    .CreateNoWindow = True,
                    .RedirectStandardOutput = True,
                    .RedirectStandardError = True
                }
            }

            If worker.CancellationPending Then
                e.Cancel = True
            Else
                process.Start()
            End If
        End Sub

        Public Shared Sub UpgradePython(cmd As String)
            Dim startInfo As ProcessStartInfo = New ProcessStartInfo(Pythonfile.Python, cmd) With {
                .CreateNoWindow = True,
                .WindowStyle = ProcessWindowStyle.Hidden,
                .UseShellExecute = False,
                .Verb = "runas",
                .RedirectStandardError = True,
                .RedirectStandardOutput = True
            }

            Using process As Process = Process.Start(startInfo)
                process.BeginOutputReadLine()
                process.BeginErrorReadLine()
                AddHandler process.OutputDataReceived, Sub(sender As Object, e As DataReceivedEventArgs)
                                                           Dim text As String = If(e.Data, String.Empty)

                                                           If text <> String.Empty Then
                                                               Console.WriteLine(text)
                                                           End If
                                                       End Sub

                AddHandler process.ErrorDataReceived, Sub(sender As Object, e As DataReceivedEventArgs)
                                                          Dim text As String = If(e.Data, String.Empty)

                                                          If text <> String.Empty Then
                                                              Console.WriteLine(text)
                                                          End If
                                                      End Sub

                process.WaitForExit()
            End Using
        End Sub
    End Class
End Namespace
