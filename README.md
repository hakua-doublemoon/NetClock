# About
「NICT インターネット時刻供給サービス」を利用するUnity(2018.4.24f1)製のデスクトップ時計。
Windows8.1 or 10で動きます。他のOSでの動作は知りません。
ビルドしたものはReleaseフォルダにzipで固めてあるのでそのまま使えると思います。
（ビルド物の再配布は禁止します。ソースコードはMITライセンスで使用可能です。）

# Details
* 「NICT インターネット時刻供給サービス」からGetメソッドで時間を取得します。
  + 普通はWindowsやOSのNTP機能で事足りるあれですが、何らかの事由で時計が狂うときに使えます。
  + タイムゾーンは日本（JST/UTC+0900）固定です。
  + 15分に1回、ランダムなタイミングで時刻合わせします。
* 時計盤をクリックすることで手動で時刻合わせします。
* ２時間に一回、「ついなちゃん」による時報があります。
  + 時報を停止する方法はありません。

# Note
* Assetの画像は下記より拝借。（色やサイズなどは変えてあります）
  + https://www.pngwave.com/png-clip-art-opabg
  + https://www.pngwave.com/png-clip-art-cacja
  + https://www.pngwave.com/png-clip-art-kzgxy
* 音声データは「VOICEROID2 ついなちゃん」で作成しています。
  + 基本的な利用規約はVOICEROIDに従いますが、改変（翻訳）や再配布などの場合はクレジットに私、hakua-doublemoonを含めてください。
  + https://www.ah-soft.com/voiceroid/tsuina/#faq
