# Codeer.Friendlyを使ったUIテスト

## 1.前書き

.NETアプリケーションでユニットテストを行い、カバレッジを測定する際にネックとなるのが「UI」のテストです。(ここでは、WinFormsアプリケーションのテストを前提としています)

過去に取り組んだことのある[React](https://ja.reactjs.org/)では [jest](https://jestjs.io/ja/)  と[react-testing-library](https://testing-library.com/docs/react-testing-library/intro/) を使い(大変ではありますが)、UIのテスト＋カバレッジ測定を行うことができました。

[react-testing-library](https://testing-library.com/docs/react-testing-library/intro/)は、ユーザが行う操作をコードでエミューレートして、レンダリング結果の確認を行うことができます。

* UI画面(フォーム)を表示
* 画面に値をセット
* イベントを発生させる(クリックやラジオボタンの切替)
* 画面の値をチェック

このようなイメージで、.NETアプリケーション(WinForms)のフォームをテストしたいというのが目的です。

UIをコーディングで操作可能なツールを調べたところ、[Codeer.Friendly](https://github.com/Codeer-Software/Friendly/)が直観的かつ、使いやすそうですのでこれと、MSTestを使ってみましょう。

|ツール名| 概要|
|-|-|
|Microsoft UIオートメーション|.NET Framework 3.0以降で利用可能。[参考ページ](https://atmarkit.itmedia.co.jp/fdotnet/special/uiautomation/uiautomation_02.html)を見る限り、直観的に使える雰囲気ではない。|
|Codeer.Friendly|操作対象プロセスの内部メソッドを呼び出せるライブラリと、それを使ったGUI操作ライブラリ群。[参考ページ](https://github.com/Codeer-Software/Friendly/blob/master/README.jp.md)のとおり、直観的で使いやすそうです。|



## 2.テスト対象フォームの準備
1. また書く


## 3.ユニットテストコードの作成
---
## 3.(参考)[jest](https://jestjs.io/ja/)と[react-testing-library](https://testing-library.com/docs/react-testing-library/intro/)を使ったテストのサンプルコード


用語の説明
<dl>
    <dt>jest</dt>
    <dd> テスティングフレームワーク。.NETの<a href="https://xunit.net/">xUnit</a>や<a href="https://docs.microsoft.com/ja-jp/dotnet/core/testing/unit-testing-with-mstest">MSTest</a>に相当</dd>
    <dt><a href="https://ja.reactjs.org/docs/testing.html">react-testing-library</a></dt>
    <dd>実装の詳細に依存せずに React コンポーネントをテストすることができるツール。JavaScript のコードと同じようにテストできる。</dd>
</dl>

#### テスト対象コンポーネント

  Helloコンポーネントの引数.nameが空なら`Hey, stranger`、空でない場合は`Hello, 【引数.name】`と出力されると理解できれば問題ありません。

```js
// hello.js  
import React from "react";

export default function Hello(props) {
  if (props.name) {
    return <h1>Hello, {props.name}!</h1>;
  } else {
    return <span>Hey, stranger</span>;
  }
}
```
#### ユニットテストコード
[react-testing-library](https://testing-library.com/docs/react-testing-library/intro/)は、コンポーネントを(仮想的に)描画してから、描画結果に含まれる文字や、タグの存在チェックを行うことができます。

```js
// hello.test.js jestを利用したテストコード

import React from "react";
import { render, unmountComponentAtNode } from "react-dom";
import { act } from "react-dom/test-utils";

// ～～～テスト前準備、後片付けコードは省略～～～

it("renders with or without a name", () => {
  // act()でコンポーネントを仮想的に描画する。描画後に表示される文字をチェックする
  act(() => {
    render(<Hello />, container);
  });
  // 引数(name)が空の場合、「Hey, stranger」と表示されていることを確認する
  expect(container.textContent).toBe("Hey, stranger");

  act(() => {
    render(<Hello name="Jenny" />, container);
  });
  // 引数(name)が"Jenny"の場合、「Hello, Jenny!」と表示されていることを確認する
  expect(container.textContent).toBe("Hello, Jenny!");
});
```

上記ソースはReact本家サイトの[テストのレシピ集](https://ja.reactjs.org/docs/testing-recipes.html#rendering)から引用




