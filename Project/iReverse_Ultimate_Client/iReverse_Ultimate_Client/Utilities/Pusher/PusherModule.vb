Imports Newtonsoft.Json.Linq
Imports PusherClient
Public Module PusherModule

    Public _pusher As Pusher
    Public _publicChannel As Channel
    Public _privateChannel As Channel
    Public _privateEncryptedChannel As Channel

    Public AppId As String = "1604614"
    Public AppKey As String = "743996c65a2c3b504344"
    Public AppSecret As String = "9317a9ca542aef9b4343"

    Public ExampleMsg As String = "{ progressbar1:"""" 100 """", progressbar2:"""" 100 """", log:"""" success """" }"

    Public Async Function InitPusher() As Task
        _pusher = New Pusher(AppKey, New PusherOptions With {
                .ChannelAuthorizer = New HttpChannelAuthorizer("http://localhost/pusher/auth.php"),
                .Cluster = "ap1",
                .Encrypted = True,
                .TraceLogger = New TraceLogger()
                })
        AddHandler _pusher.ConnectionStateChanged, AddressOf PusherConnectionStateChanged
        AddHandler _pusher.[Error], AddressOf PusherError
        AddHandler _pusher.Subscribed, AddressOf Subscribed
        AddHandler _pusher.CountHandler, AddressOf CountHandler
        AddHandler _pusher.Connected, AddressOf Connected

        Try
            _privateChannel = Await _pusher.SubscribeAsync(XtraMain.mychannel)
            _privateChannel.Bind(XtraMain.myEvent, AddressOf EventListener)
        Catch unauthorizedException As ChannelUnauthorizedException
            XtraMain.DelegateFunction.lbstatserver.Invoke(Sub() XtraMain.DelegateFunction.lbstatserver.Text = "Unauthorized")
            Console.WriteLine($"Authorization failed for {unauthorizedException.ChannelName}. {unauthorizedException.Message}")
        End Try

        Console.WriteLine("All SubscribeAsync already called")
        Await _pusher.ConnectAsync().ConfigureAwait(False)
    End Function

    Public Sub EventListener(ByVal eventData As PusherEvent)
        Dim json As String = eventData.Data
        Dim jsonObject As JObject = JObject.Parse(json)

        If (jsonObject("action") = ("MTK_OneClick")) Then
            XtraMain.DelegateFunction.lboperation.Invoke(Sub() XtraMain.DelegateFunction.lboperation.Text = jsonObject("method"))
            MTKOneclick(jsonObject("method"))
            XtraMain.DelegateFunction.lboperation.Invoke(Sub() XtraMain.DelegateFunction.lboperation.Text = "Done")
        End If

    End Sub

    Public Sub Connected(ByVal sender As Object)
        XtraMain.DelegateFunction.lbstatserver.Invoke(Sub() XtraMain.DelegateFunction.lbstatserver.Text = "Connected")
    End Sub

    Public Sub Subscribed(ByVal sender As Object, ByVal channel As Channel)
        Console.WriteLine($"Subscribed To Channel {channel.Name}")
    End Sub

    Public Sub CountHandler(ByVal sender As Object, ByVal data As String)
        Console.WriteLine($"CountHandler {data}")
        RichLogs("logged-In", "blue", True)
    End Sub

    Public Sub PusherError(ByVal sender As Object, ByVal [error] As PusherException)
        TraceMessage(sender, $"{Environment.NewLine}Pusher Error: {[error].Message}{Environment.NewLine}{[error]}")
    End Sub

    Public Sub PusherConnectionStateChanged(ByVal sender As Object, ByVal state As ConnectionState)
        TraceMessage(sender, $"Connection state: {state}")
    End Sub

    Public Sub TraceMessage(ByVal sender As Object, ByVal message As String)
        Dim client As Pusher = TryCast(sender, Pusher)
        Console.WriteLine($"{DateTime.Now} - {client.SocketID} - {message}")
    End Sub

End Module
