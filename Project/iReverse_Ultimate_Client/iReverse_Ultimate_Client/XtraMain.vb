Imports Newtonsoft.Json
Imports PusherClient
Imports System.Collections.Specialized
Imports System.Net

Public Class XtraMain

    Public Shared DelegateFunction As XtraMain

    Public Shared mychannel As String = "private-channel-ireverse"
    Public Shared myEvent As String = "client-ireverse-001"
    Public Shared toEvent As String = "client-User-Web"

    Public Shared Username As String = "test-1"

    Public Shared wc As WebClient = New WebClient()
    Public Shared dataToSend As NameValueCollection = New NameValueCollection()

    Public Sub New()
        DelegateFunction = Me
        InitializeComponent()
        AddHandler MyBase.Load, AddressOf XtraMainLoad
        AddHandler MyBase.FormClosing, AddressOf XtraMainClose
    End Sub

    Public Sub XtraMainLoad()
        Dim connectResult = Task.Run(Function() InitPusher())
        Task.WaitAll(connectResult)
        Dim line As String = ""
        Do
            line = Console.ReadLine()

            If line = "quit" Then
                Exit Do
            End If
        Loop While line IsNot Nothing
    End Sub

    Public Sub XtraMainClose()
        ProcessKill()
        Dim channel As Channel = _pusher.GetChannel(mychannel)
        RichLogs("logged-Out", "blue", True)

        _pusher.Unbind(myEvent)
    End Sub

End Class