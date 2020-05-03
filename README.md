# Mahjong

## usage

### server

デフォルトの localhost:9999 で起動する場合

```
$ dotnet build
$ dotnet run --server
```

ホストとポートを指定できるので  
例えば4646ポートで外部公開する場合には下記のようになる。

```
$ dotnet run --server --hostname 0.0.0.0 --port 4646
```

### client

最終的にUnityにクライアントのHubインターフェイスを持っていって  
本来のMagicOnionよくある使い方にする想定だが、とりあえず動作確認用として  
`--server` 引数なし起動の時にはクライアントとして振る舞うようにしてある。

```
C ... Connect         // Grpcチャネル接続
J ... Join            // MagicOnionのJoinAsync
E ... Create Room     // ルームを作成して入室
F ... Enter Room      // ルームに参加
M ... Message(Global) // 全体に向けてメッセージ送信 
RM .. Message(Room)   // ルーム内に向けてメッセージ送信
RS .. Room Sit/Stand  // ルーム内で着席/離席する
RR .. Refresh Room    // ルーム情報更新
S  .. Start           // 対局を開始(3人以上が着席した状態)
```

コンソール上で雑にこういったコマンドを投げて動作確認できる。
