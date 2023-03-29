# ChatGptTest  
# 目標  
- 学習目標  
ChatGPTとWhiperAPIで何ができるか調べる。
- 制作目標  
声で操作できる戦闘補助AIを使ったゲームを作る →　詠唱をWhisperAPIでできるゲームを作る。  

# 結果　
ChatGPTもWhiperAPIもリクエストが必要な以上スピードが求められるゲームには向かなそうなことが分かった。    
今後利用する際はそれらを意識したうえでゲームデザインをするべきだとわかった。またWhisperAPIに関しては精度がそこまでよくはないので現状はWindows.Speechの方がよさそう。  
0.002ドルとはいえもったいない。  
とはいえ、音声認識をゲームに使う方法とそれがどのようなものか知れたのはよかった。



https://user-images.githubusercontent.com/104509665/227771788-6515adbd-4f54-4971-977b-897d998645f6.mp4



# モデル図
https://drive.google.com/file/d/1t-iqkGCvtIncqHAnKnReBkwUplwQFC7E/view?usp=sharing

# メモ  
- 音声認識だけならUnityEngine.Windows.Speechのほうがよさそう
- プロンプトエンジニアリングを学べばより正確な返答を求めることができるかも(http://soysoftware.sakura.ne.jp/archives/3691)
- Too Many Requestsエラーがそこそこでてくる(もちろんUpdateで回していたりはしない)。またレスポンスもそこまで早くない。実際のゲームでつかうなら返答スピードがそこまで必要でないものの方がいいかも。
- WhisperAPIはかなり返答がはやい。なんでもChatGPTに送るよりかは決められた単語をWhisperAPIで読み取って実行の方がいいかもしれない。 ChatGPTのローカル版のAlpacaが存在するくらいなのだからWhisperAPIもローカル版がでたらより使いやすいかも
- GoogleのGenerative AI App Builderを使えば、わざわざ前提条件を送ったりしなくてもよくなるかも(https://japan.googleblog.com/2023/03/google-workspace-ai.html)
- GPT4を申し込んだ。期待
- APIキーはスクリプト上はもちろんシーンのフィールドにも書きこんでコミットしてはいけない（戒め） 

# 進捗  
### 2022/3/13


https://user-images.githubusercontent.com/104509665/224716911-ac20d768-c51e-4a12-80f6-03abf4db7741.mp4


### 2022/3/12


https://user-images.githubusercontent.com/104509665/224540993-210808cf-34eb-4122-8c71-02816c9e4572.mp4



# 参考
- https://note.com/negipoyoc/n/n88189e590ac3
- https://twitter.com/tarukosu/status/1632009079840444419  
- https://qiita.com/mfuji3326/items/c46a56549e3991926fb2  
- https://www.hanachiru-blog.com/entry/2022/08/18/120000  
- https://www.youtube.com/watch?v=6hCPb7jX8VM&ab_channel=AI%E3%82%92%E4%BD%BF%E3%81%A3%E3%81%A6%E9%9D%A9%E5%91%BD%E3%82%92

