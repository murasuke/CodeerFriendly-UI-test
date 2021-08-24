# カバレッジ測定ツール[AxoCover](https://marketplace.visualstudio.com/items?itemName=axodox1.AxoCover)をVisual Studio 2019で利用する方法

## 1.前書き

Visual Studioアドイン[AxoCover](https://marketplace.visualstudio.com/items?itemName=axodox1.AxoCover)はVisual Studio 2012 ～ 2017で利用できる、カバレッジ測定ツールです。Visual Studio Enterpriseであればカバレッジ測定機能が組み込まれていますがとても高額です。
Community版でも組み込める無償ツールはとてもありがたいです。

**ツールの特徴**

* ユニットテストを自動で検出します。
* ユニットテストを実行すると、クラス、メソッド毎にカバレッジ率を表示することができる。
* カバレッジ測定自体はコマンドラインツールの[opencover](https://github.com/OpenCover/opencover)を利用しているようです。アドオンとして統合されているAxoCoverを使えば、細かい設定を意識せずに測定ができます。


  ![AxoCover0.png](./img/AxoCover0.png)

---

## ただし、困ったことにVS2019にはインストールできません

---

なぜならAxoCoverはここ数年、開発が止まっているからです。

それでも[githubのissue上でVS2019対応](https://github.com/axodox/AxoCover/pull/206)について議論がなされており、pre-releaseとして[master-1.1.400](https://github.com/axodox/AxoCover/releases/tag/master-1.1.400)が公開されています。

VS2019で非推奨となったAPIを利用しているため起動時に警告が表示されますが、動作自体に問題はないようです。

(起動時に同期ロードAPIを利用しているため、VisualStudio起動時のパフォーマンスに影響があるようです)


## 2.AxoCoverインストール方法
Visual Studio 2019の「拡張機能の管理」からはインストールができないため、拡張機能(.vsix)をgithubからダウンロードします。


* 1.`AxoCover.vsix`のダウンロード

  [AxoCover.vsix](https://github.com/axodox/AxoCover/releases/download/master-1.1.400/AxoCover.vsix)を、githubからダウンロードします。

* 2.インストール
  
  ダウンロードしたファイルを右クリックして、インストール(開く)を行います。

  ![AxoCover1.png](./img/AxoCover1.png)

* インストール対象のバージョンを選択して「Install」をクリック

  ![AxoCover2.png](./img/AxoCover2.png)

* Visual Studioが起動していると、インストールを妨げているプロセスを終了してよいか確認する画面が表示されます

  ![AxoCover3.png](./img/AxoCover3.png)

  Visual Studioを手動で終了すると次へ進みます(もしくはEnd Tasksで強制終了して次へ進みます)

* インストール中

  ![AxoCover4.png](./img/AxoCover4.png)

* インストール完了画面

  ![AxoCover5.png](./img/AxoCover5.png)


---

インストール自体はこれで完了ですが、VisualStudio起動時にアドイン読み込みエラーが表示されます。

* Visual Studio 2019を起動後の警告表示

  ![AxoCover6.png](./img/AxoCover6.png)

  ここでは「同期自動読み込みを許可」をクリックして、アドオンの読み込みを継続します。

* AxoCoverの表示

  ![AxoCover8.png](./img/AxoCover8.png)

  「ツール」メニューから選択して表示します


* 初回起動時の確認画面

  ![AxoCover7.png](./img/AxoCover7.png)

  エラー時にtelemetryを送るかどうか選択して「OK」をクリックします。

* AxoCoverアドオン画面(UnitTestが存在しない場合)

  ソリューション内にUnitTestがない場合、テストコードが見つかりませんよ？という確認の画面が表示されます。

  ![AxoCover9.png](./img/AxoCover9.png)



* AxoCoverアドオン画面(UnitTestが存在する場合)

  UnitTestがある場合は自動的に検出されて、テストがツリー表示されます。

  ![AxoCover10.png](./img/AxoCover10.png)



## 3.簡単な動作確認手順

動作確認のため、ごく単純なテスト対象プロジェクト(CalcLogic.cs)と、UnitTestプロジェクトを作成します。

### 3-1. テスト対象プロジェクトの作成

* .NET Frameworkで適当にプロジェクト(exeでもdllでもどちらも可)を作成してテスト対象ソースき下記コードを記載します。

  分岐が1つだけの簡単なロジックです。

```csharp
    public class CalcLogic
    {
        public static decimal Add(decimal a, decimal b)
        {
            return a + b;
        }

        public static decimal Sub(decimal a, decimal b)
        {
            return a - b;
        }

        public static decimal Mul(decimal a, decimal b)
        {
            return a * b;
        }

        public static decimal Div(decimal a, decimal b)
        {
            if (b == 0)
            {
                return 0;
            }

            return a / b;
        }
    }
```



### 3-2. ユニットテストの作成

* ソリューションを右クリックして「単体テストプロジェクト」を追加します

  ![AddTestProject.png](./img/AddTestProject.png)

* 適当にプロジェクト名を決めて(UnitTestProject)作成します


```csharp
    [TestClass]
    public class TestCalcLogic
    {
        [TestMethod]
        public void TestAdd()
        {
            Assert.AreEqual(CalcLogic.Add(1, 2), 3);
        }

        [TestMethod]
        public void TestSub()
        {
            Assert.AreEqual(CalcLogic.Sub(1, 2), -1);
        }

        [TestMethod]
        public void TestMul()
        {
            Assert.AreEqual(CalcLogic.Mul(1, 2), 2);
        }

        [TestMethod]
        public void TestDiv()
        {
            Assert.AreEqual(CalcLogic.Div(1, 2), 0.5M);
            //Assert.AreEqual(CalcLogic.Div(1, 0), 0M); // 分岐を100%にしないためにコメントアウト
        }
    }
```

### 3-3. AxoCoverでカバレッジ測定

  AxoCoverを表示すると、UnitTestがツリー状に表示されます。赤枠で囲った「Cover」をクリックすると、選択されている部分のUnitTestが行われてから、カバレッジ測定の結果が表示されます。

* 測定前

  ![AxoCover10.png](./img/AxoCover10.png)



* 測定後（UnitTest実行結果）

  全てのテストが正常に終了。

  ![result1.png](./img/result1.png)


* 測定後（Reportタブ）

  テスト対象のうち「`Div()`」メソッドの分岐カバレッジが100%になっていません。

  ![result2.png](./img/result2.png)

  右下の「`Source`」をクリックすることで、該当ソースを開いてカバレッジの詳細を確認することができます。

  `b == 0`の場合の分岐が通っていないことが一目瞭然です。
  
  ![result3.png](./img/result3.png)


### 3-4. UnitTestを修正して再確認

* TestDiv()メソッドのコメントを外して、再度UnitTestを実行します

```csharp
    [TestClass]
    public class TestCalcLogic
    {

        [TestMethod]
        public void TestDiv()
        {
            Assert.AreEqual(CalcLogic.Div(1, 2), 0.5M);
            Assert.AreEqual(CalcLogic.Div(1, 0), 0M); // 分岐を網羅するためコメントアウトを削除
        }
    }
```

* 無事、分岐網羅率が100%になりました(C1網羅)

  ![result4.png](./img/result4.png)

## その他調査


|  確認内容  |  結果  | 補足 |
| ---- | ---- | ---- |
| テスト対象が.NET2.0、UnitTestが.NET4.8の場合でも動作するか？ | 動作する  | |
| UnitTestは.NET4.0でも動作するか？  |  動作しない  |　MSTest.TestFrameworkが.NET4.5以降を要求するため |
| プロジェクト参照、通常のdll参照どちらでも動作するか？ | 動作する |  |
