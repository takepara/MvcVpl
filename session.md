class: center, middle

<style type="text/css">
blockquote{font-size:100%; font-style:italic;}
.remark-slide-content {font-size:22px !important;}
</style>

# コンパイラ Roslynの気持ち

---
## 自己紹介

![takepara](https://qppxmq.by3302.livefilestore.com/y2pCMjYcXBWHoAOqGxoOt04Oq0JgmgfEzweeO-Rtk_vy2eMFRfJzg-TnzE2BnTGrL7b0Ta_WbfIpXMqXiADVcSJiRUbSyqLOLlSxVRAb4Nz5BqowyQCSE9EIuPpteBzfFogSlrDEZJL-Or40dsLGOT65Q/metalking.jpg?psid=1)

* http://www.commerble.com  
 株式会社Commerble で働いてます  
* http://takepara.blogspot.jp  
 ブログを書いてた時期もありました
* http://takepara.tumblr.com  
* @takepara  

---
## 今日、話したいこと

* コンパイラとは
* Roslynとは
* Roslynの気持ち

---

## 今日、話せないこと

* Visual Studio 拡張
* Features/Workspaces/Compiler API などの仕様や使い方

Roslynの派手な機能についてはふれません。ふれられません。  

---
### 参考記事

これらはMSDNにとてもいい解説があるので、そちらを参照するといいと思います。

* [働くプログラマ - Roslyn 登場 ](https://msdn.microsoft.com/ja-jp/magazine/dn818501.aspx)  
* [働くプログラマ - Roslyn 登場、第 2 部: 診断の作成](https://msdn.microsoft.com/ja-jp/magazine/dn904676.aspx)  
* [C# および Visual Basic - ロザリン ライブ コード アナライザーのために書くあなたの API 使用します。](https://msdn.microsoft.com/ja-jp/magazine/dn879356.aspx)  
* [C# - Roslyn アナライザーへのコード修正の追加](https://msdn.microsoft.com/ja-jp/magazine/dn904670.aspx)  

---
## VisualStudio拡張

コード解析やコードフィックスは、先のdonetConf2015のRoslynセッションどうぞ

![channel9](https://q5pemq.by3302.livefilestore.com/y2pIxQafALSGd5dp_mTPdZzbZcBOTMiKDxMvFSQRW5lLS7AyJqiWamz85KZYsrgdkZSMnKbMq_5fb-smfrTVtQgJ34EC-Sw8RQlufEGX914cfRk4DgWyltg5z_9p2k9Ti94W_c5fBIMaNVZNQvupzkvoA/channel9roslyn.png?psid=1)

[.NET Compiler Platform ("Roslyn"): Analyzers and the Rise of Code-Aware Libraries  | dotnetConf 2015 | Channel 9](https://channel9.msdn.com/Events/dotnetConf/2015/NET-Compiler-Platform-Roslyn-Analyzers-and-the-Rise-of-Code-Aware-Libraries)

---

## コンパイラとは

コンパイラに関するすばらしい本があるのでそちらより引用してみます。

  [コンパイラの理論と実現](http://www.amazon.co.jp/dp/432002382X)

  > 計算機は機械語で記述されたプログラムを実行する。 機械語プログラムの場合には、計算機はプログラムをそのまま実行できる。 しかし高級言語プログラムをそのままの形で実行することはできない。 コンパイラと呼ばれる翻訳プログラムによって機械語プログラムに翻訳して、それを実行する。

---

### こんな感じ
![コンパイラ](https://qppxmq.by3302.livefilestore.com/y2pOQb0vfJT8uIkZvZ_KhNRI4PNCUJsOMD_l6Mtyyop1fflysdLEls643e6EQizOMmp2qWUXGNl2SdaZUVUaiyWHMHbNrdrXCIDZp3Fwl9qsFhSXNyIYFAgnjNnF2x_3VT2V9rz0QlLQVX6W1SrmHVpgw/dn818501.Neward_Figure1_hires.png?psid=1)

https://msdn.microsoft.com/ja-jp/magazine/dn818501.aspx
---

## コンパイルとは

  > コンパイルとは、高級言語プログラムを、それと同等な機械語プログラムに変換することである。

  少し難しい言葉で表現している箇所を引用します。

  > コンパイルの対象となる高級言語プログラムを原始プログラム(source program)という。 またその言語を原始言語(source language)という。 コンパイルの結果として得られる機械語プログラムを目的プログラム(object program)あるいは目的コード(object code)という。 またその機械語を目的言語(object language)、機械を目的機械(object machine)あるいは対象マシン(target machine)という。

目的機械が仮想機械(VM)だとしても、同じ抽象概念が適用されます。

---

### 仮想機械

*  システム仮想機械

 > システム仮想機械はシステム全体を再現し、その上でOSを動かすことを可能にする。動作させるOSにいくらかの変更を加えることが必要な場合(準仮想化)もある。

*  プロセス仮想機械

 > いくつかのアプリケーション・プログラムを動作させるための仮想機械。いくつかのプログラミング言語やその実装では理論上の機械によって実行されることを想定しているので、その機械をエミュレートする場合が多い。


[仮想機械 - Wikipedia](http://ja.wikipedia.org/wiki/%E4%BB%AE%E6%83%B3%E6%A9%9F%E6%A2%B0)

CILもJVMもプロセス仮想機械です。
[LLVM](http://ja.wikipedia.org/wiki/LLVM)なんかはコンパイラ基盤として特化してます。  
情報処理を学習したことがある方は[CASL](http://ja.wikipedia.org/wiki/CASL)がイメージしやすいかもしれません。
---

## コンパイラの処理

* フロントエンド

  * 字句解析  
 変数名、定数値、演算子などをなす文字列をまとめて一つの単位とし、字句という。
  - 構文解析  
  字句の列に対して、言語の構文規則に従ってプログラムの構文的な構造をしらべ、構造を表す解析木を作る。
  * 意味解析  
  変数名などの名前を記号表に登録し、型の一貫性の検査などをする。

* バックエンド

  * コード最適化
  * コード生成  
  解析木を機械命令の列に置き換える。コード生成の前または後においてコード最適化が行われることもある。

---

### こんな感じ

![コンパイラ](https://qppxmq.by3302.livefilestore.com/y2pOW3NEXjE2yvPa_U21jOVQfAkbo8mku7FyP3iyZDO34UFD6eTimSnn6jnUuXa7qc-fPQL-XZU77_QejuO_xM3ZivisKELy9DAqQd_NcUN0AJnm56o3bxCSTDMmtTbDs_71upSZZEYap5ogSqo2w7oIA/dn818501.Neward_Figure2_hires.png?psid=1)

https://msdn.microsoft.com/ja-jp/magazine/dn818501.aspx
---

## ちなみに

.NETの場合はコンパイルのタイミングでそれほど積極的な最適化は行わないことになってます。CLRにはJITがいるので、そこで頑張ります。これからは[RyuJIT](http://blogs.msdn.com/b/dotnet/archive/2013/09/30/ryujit-the-next-generation-jit-compiler.aspx)。

[コンパイラの最適化についてすべてのプログラマが知っておくべきこと](https://msdn.microsoft.com/ja-jp/magazine/dn904673.aspx)

たとえば、こちらの例のように積極的にインライン展開せよ、と属性を示した場合なんかは 7ns早くなりました、とおっしゃってます。

[C# MethodImplOptions.AggressiveInlining](http://www.dotnetperls.com/aggressiveinlining)

---

## Roslynとは

.NET Compiler Platform です。

GitHubにソースがあるので何をやってるのか気になるようでしたら、ぜひ。

[dotnet/roslyn](https://github.com/dotnet/roslyn)

リポジトリ内にドキュメントがあって結構詳しくは書かれてます。

[.NET Compiler Platform ("Roslyn") Overview](https://github.com/dotnet/roslyn/wiki/Roslyn%20Overview)

---

## できること

1. コンパイルが出来る
2. コンパイル時に生成される語彙分析結果(内部解析木：構文木)にアクセスできる
3. 開発環境に組み込める/拡張できる

---

## 具体的には

1. Features APIにより、リファクタリングやCodeFixの機能実装が簡単にできる(VisualStudio拡張とか)
2. Workspacesを利用したソリューション/プロジェクト/ドキュメントへのアクセス手段ができる([OmniSharp](https://github.com/omnisharp)とか)
3. Compiler APIで構文木、シンボル、バインディングとフロー解析、出力ができる

これらもまたGitHub上のドキュメントにいろいろ書かれてます。

* 言語実装の話
[Languages features in C# 6 and VB 14 · dotnet/roslyn Wiki](https://github.com/dotnet/roslyn/wiki/Languages-features-in-C%23-6-and-VB-14)

* 拡張機能の話
[The C# and Visual Basic Code-Focused IDE Experience - The Visual Studio Blog - Site Home - MSDN Blogs](http://blogs.msdn.com/b/visualstudio/archive/2014/11/12/the-c-and-visual-basic-code-focused-ide-experience.aspx)

---
### API群

![機能](https://github.com/dotnet/roslyn/wiki/images/alex-api-layers.png)

https://github.com/dotnet/roslyn/wiki/Roslyn%20Overview
---
### 内部フロー

![機能](https://qppxmq.by3302.livefilestore.com/y2pG-ogJQjt0QhDepqA3JS0w6RMOeaYc5ZbNPpbk4hVgUAUQWCvtVxWbI8c7qc1nCABEAs0bcwK04-eBwbjQPiURureeBSfR8lotio7IVdiNNKrOEZ9wwFwZWQDu9FsMW4z0JoDZPzprbVnn7_VceLcBw/compiler-pipeline-lang-svc.png?psid=1)

https://github.com/dotnet/roslyn/wiki/Roslyn%20Overview
---
### Roslyn Syntax Visualizer

![](https://qppxmq.by3302.livefilestore.com/y2pZEM33PlUzTbGQZx3m4bgaNqv3mqCxQJHqvqMvZSTPjribfHYGQapQ8LcOlcLVmQTgskaFaVcSgxF2DavszrOh72XjNxbxN18DbTg2CX5hK7N7QUQhTZ8GdJ0vydLNDy2_qzERu_Sa-4xWEhawlGzSQ/dn879356.TurnerAnalyzers1314_Figure3_hires.png?psid=1)

https://msdn.microsoft.com/ja-jp/magazine/dn879356.aspx
---
### DiagnosticsとCode Fix

![](https://qppxmq.by3302.livefilestore.com/y2pNAgZxczbMVYIDenGZL2LUyex1MxD3LXkEzQeXVnojRrdkekr6RzxssfKwI-hFXvkq1keGMcbSb7PfzraVLY8lUSgC5VWKgQx561OvVqiLbqZpU6f6Bh_y6A9j9zpLFrwtU6Plh1RIfVxdLydd4OmbA/dn904676.NewawdHummelWorkProg0215_figure_5_hires%28ja-jp%2CMSDN.10%29.png?psid=1)

https://msdn.microsoft.com/ja-jp/magazine/dn904676.aspx

---
## コンパイラの実行方法

一般的なコンパイラってプロセス起動です。そのプロセスに原始プログラムを渡すと目的プログラムが出力されます。  

これをここでは ** アウトプロセスコンパイラ ** と勝手に言います。

その過程でコンパイラ内部で生成される情報にはアクセスできないです。すくなくとも.NETのコンパイラはブラックボックス。

---

## Roslynの実行方法

Roslynは** インプロセスコンパイラ **です。

NuGetで取得してプログラムの中から呼び出して利用します。  

もちろん、これまでどおり実行ファイルとしても提供されますが、その際提供される実行ファイルは起動パラメータをRoslynに引き渡す薄いものです。

* [roslyn/src/Compilers/CSharp/csc at portable-pdb · dotnet/roslyn](https://github.com/dotnet/roslyn/tree/portable-pdb/src/Compilers/CSharp/csc)
* [roslyn/src/Compilers/VisualBasic/vbc at portable-pdb · dotnet/roslyn](https://github.com/dotnet/roslyn/tree/portable-pdb/src/Compilers/VisualBasic/vbc)

---

## Roslynでのコンパイル

コンパイラの処理 でも少し触れましたが、コンパイラは原始プログラムを目的プログラムに翻訳するものです。  

コンパイラとしてのRoslynの機能は「 ** C#とVB.Net** を 原始プログラムとして ** CIL **(MSIL) を目的プログラムとして出力する」 です。  

[Standard ECMA-335](http://www.ecma-international.org/publications/standards/Ecma-335.htm)

---

### ソースとコンパイラと成果物の関係

RoslynだとC#6とVB14をパースできる、っていうことですけど、それ以前のバージョンだってもちろんパースできます。
シンタックスシュガー(usingとかvarとかforeachとか)の自動実装とCILは別物ですし。FuncデリゲートやTaskAwaiterなど特定のクラスに依存するものもあったりしますが、そこは自作でも動いたりします。

詳しい説明は以下をどうぞ。

* [C#とILとネイティブと](http://www.slideshare.net/ufcpp/compilation-29412750)  
* [今から始める、Windows 10＆新.NETへの移行戦略 | ++C++; // 未確認飛行 C ブログ](https://ufcpp.wordpress.com/2014/11/29/%e4%bb%8a%e3%81%8b%e3%82%89%e5%a7%8b%e3%82%81%e3%82%8b%e3%80%81windows-10%ef%bc%86%e6%96%b0-net%e3%81%b8%e3%81%ae%e7%a7%bb%e8%a1%8c%e6%88%a6%e7%95%a5/)
* [UfcppSample/LanguageAndFrameworkVersion at master · ufcpp/UfcppSample](https://github.com/ufcpp/UfcppSample/tree/master/LanguageAndFrameworkVersion)

※ シンタックスシュガーのイメージをつかむために時間があったらIL見てみます。

---
## .NET Frameworkのライブラリ

RoslynはC#で実装したコンパイラなので、それ自身CLRに依存してます。  
Microsoft.CodeAnalysisそれ自身Ildasmでみると

> ildasm.exe /headers /noil /text "Microsoft.CodeAnalysis.dll"
> CorFlags.exe "Microsoft.CodeAnalysis.dll"
> // Metadata version: v4.0.30319

です。.NET Framework 4.5以上、っていう条件ですけど、Roslyn 自体はVS2013でも使えます。

* [Ildasm.exe (IL 逆アセンブラー)](https://msdn.microsoft.com/ja-jp/library/f7dy01k1.aspx)  
* [読み込むランタイム バージョンの決定](https://msdn.microsoft.com/ja-jp/library/w671swch%28v=vs.110%29.aspx)


---
## Roslynの使い道

コンパイラとしてのRoslynを開発者のためだけのツールとして使うだけではなく、自分で原始プログラムを生成(コード生成)し、ソリューションの一部としてRoslynを呼び出し、実行時にコンパイルし機能提供していくというのはいかがでしょうか。

コード解析・コードフィックス・開発環境管理以外の使い道といったら** コンパイラとして使う **しかないんですけどね。

---
### コンパイルのタイミング

[コンパイラ - Wikipedia](http://ja.wikipedia.org/wiki/%E3%82%B3%E3%83%B3%E3%83%91%E3%82%A4%E3%83%A9)
にも書かれてますが、一般的にコンパイラを実行してコンパイルを行うタイミングは2つ。

* 事前コンパイラ - 実行前に事前にコンパイルする。Ahead-Of-Timeコンパイラ (AOTコンパイラ)。
* 実行時コンパイラ - 実行時にコンパイルする。Just-In-Timeコンパイラ (JITコンパイラ)。

Roslynはどっちでも使えます。  
実行ファイル(csc.exe/vbc.exe)とライブラリ(DLL)提供。

---

## コード生成

* 事前コード生成  

 T4なんかはメジャーどころでしょうか。  

* 実行時コード生成  

 RazorやWebFormsなんかみなさんよく使ってることでしょう。こちらはより動的に原始プログラムを生成します。  
 * テンプレートファイル → テンプレートエンジン → 原始プログラム
 * コンパイル → 目的プログラム → 実行

---
### DNX

DNXの話がありましたよね？
あったはずなんです。特に、ASP.NET 5のほう。
あった、という前提で話を進めます。

とくにASP.NET5と言われてるものは、アセンブリではなくソースと依存情報をデプロイして実行時にコンパイル（リクエスト時）してます。本質的に実行時コンパイルを前提としています。  

ASP.NETなんかはすべてリクエスト時にテンプレートエンジン(WebForms/Razor)からソースコード生成して、コンパイラに引き渡し(BuildManager)そこからCodeDOMでコンパイルしてます。  

※コードビハインドの部分はビルド時にコンパイルされるので、リクエスト時とは異なるタイミングになります。

* [BuildManager クラス (System.Web.Compilation)](https://msdn.microsoft.com/ja-jp/library/system.web.compilation.buildmanager%28v=vs.110%29.aspx)  
* [ASP.NET の動的コンパイルの概要](https://msdn.microsoft.com/ja-jp/library/ms366723%28v=vs.100%29.aspx)  
* [ASP.NET のプリコンパイルの概要](https://msdn.microsoft.com/ja-jp/library/bb398860%28v=vs.100%29.aspx)

---
## これまでとの違い

実行時コンパイルは今までのテクノロジでもできてたし、やってました。  
あえてRoslynを使ってそれを組み込んでみることにするのはなぜかというと

1. ILの手組Emitはアセンブリくらい表現しにくい。
2. Expressionの構築とCompileもILほどじゃないにしてもやっぱり表現しにくい。
3. CodeDOMでコンパイルは、アウトプロセスでかなり遅い。

という3つの理由があります。

---
## 測ってみる

ILとExpressionはやっぱり高速で、かなりイケてるものが作れるんですけど、意図を表現しにくい。つまりメンテナンスが驚くほど難しい。  
CodeDOMは表現はC#そのものなので意図を伝えやすい。けど、コンパイルコストがかなり高い。結構高い。そもそもアウトプロセスだし。  
なら、インプロセスのRoslynなら結構はないのかも？と思ってCodeDOMと比較してみると、なんとそこに対しては** 2倍超 ** 早い。

---
class: center, middle

# デモ

---
## コンパイル速度の問題

ちなみに 先ほど紹介したスライドにも書かれてますが、それでもIL/Expressionに比べれば2桁遅い。  

でも、コンパイルコストが問題になりにくかったり、コスト払ってでもメンテナンス性がほしい場合の解決策としての** Roslyn **。
開発者が開発時に楽になるツールとしてだけの用途ではなく、ソリューションに組み込んでしまう機能の一部としての** Roslyn **。

ASP.NETなんかでもApp_Codeなどはcsファイルをおいておくだけで起動時にコンパイルしてくれますし、ファイル変更すると自動でリサイクルがかかって、アプリケーション起動時にまたコンパイルするという流れです。  

---

## 突然ですが

![scratch](https://qppxmq.by3302.livefilestore.com/y2pA-U0vRhGpBlV25I2bVJPFYHf3IVM2BM-qG0f9tCv3xOQxfrpNGA9xWMDc37l8380KILV9ha6nfJZNMwjxOwJxMEn8k2UZmt6T0KjTLl5fIXg4MMMmxzdQhpv9Ew2m14qrtwRrbRQHJChVjKgNMqBfQ/scratch.png?psid=1)

[Scratch - 想像、プログラム、共有](https://scratch.mit.edu/)
知ってますか？  
Visual Programming Languageっていうカテゴリに入るんですけど、ブロックを組み立てるように、プログラムを作るやつです。

入力がソースコードの形式じゃない。たぶんビジュアルに組み上げてAST(abstract syntax tree [抽象構文木 - Wikipedia](http://ja.wikipedia.org/wiki/%E6%8A%BD%E8%B1%A1%E6%A7%8B%E6%96%87%E6%9C%A8))構築してると思う。

---
## コンパイラの気持ち

コンパイラの気持ちを理解したかったら、自分でそれっぽく作ってみるのが近道かなと思いました。

コンパイラの仕組みでいうところの、フロントエンドはAST直接入力し、バックエンドがCS/JSのソース生成。気分はコンパイラ。そんなようなものをを作ってみました。  

CSならサーバーサイドでRoslyn使ってコンパイルしつつ実行。  
JSならブラウザ内で実行。  

そんな感じのやつです。  

---
## フロー

![takeratch](https://qppxmq.by3302.livefilestore.com/y2pjxHdW63aMMgA4LAVFhjL2f6ZPHmEgO590hG3gD1mdmTvI6DvzLYMXLZl0AWzEre6RrTCHSdquEe-27PoQdV1d0O7UMwGInOGRKbLCKZOQ0y4ayrl6XGWhWz0Iiq3U45ZzWhrWUko6nlqHyP41-adBA/takeratch.png?psid=1)


---
## それがおれの「ストロングスタイル」だ

![takeratch](https://qppxmq.by3302.livefilestore.com/y2pwx4dAYgA8dgyboPRApNs4dvBqWcEdqh-vc9tSD9lC5290EIoZGF3An8jsr_vl1RjyTDcBk-BW9g2GpAZ7yn8YYTOCH7jAzrjppASHScmH602FjdNRP8b0WGDxQGeBti6yL645osrKkBdLGQvn-R8PQ/takeratch2.png?psid=1)

https://github.com/takepara/MvcVpl
---
class: center, middle

# デモ

---
## デモプログラムで使ってるもの

* ASP.NET MVC 5
* Roslyn RC1  
 https://github.com/dotnet/roslyn
* RazorEngine  
 https://github.com/Antaris/RazorEngine
 * RazorEngine.Roslyn
* jQuery  
 https://jquery.com/
* knockoutjs  
 http://knockoutjs.com/
* Materialize  
 http://materializecss.com/

---

## 結論

コンパイラをプロジェクトに組み込んで提供してみてはどうでしょうか。

* コードをデータ化できる

  計算式そのものをデータベースに登録したり

* 実行時コンパイル前提で、カスタムしたいコードはデプロイ対象にしない

  きちんとテストした部分と、動的に変更したい部分を設計段階で分けることで、デプロイそのものを減らせるし、運用時のデプロイをなくせる

* 文字列を認識して行っていた処理を動的生成クラスにしてしまう

  判定処理そのものをコード生成ロジックが担うことで、生成されるクラスは単純処理なので実行速度にはペナルティはない(というか普通にビルドしたものと同じ)
