using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScenarioController : MonoBehaviour
{

    // シナリオファイル名(拡張子不要)
    [SerializeField]
    private string ScenarioName = "CSV/textsample";
    // Textコンポーネント
    [SerializeField]
    private Text LineText;
    // 表示速度
    [SerializeField]
    private float TextSpeed = 0.05f;
    // 表示し終えた後の待ち時間
    [SerializeField]
    private float CompleteLineDelay = 0.3f;

    // シナリオデータ
    private string[] ScenarioData;
    // 文字読み出しライン
    private string CurrentLine;
    // 文字読み出しラインインデックス
    private int LineIndex;
    // 文字読み出しインデックス
    private int CurrentIndex;
    // 太文字表示中か
    private bool isBold = false;
    // イタリック表示中か
    private bool isItalic = false;


    private void Start()
    {
        // シナリオデータ読み込み
        FileReader fileReader = new FileReader();
        ScenarioData = fileReader.Read(ScenarioName).ToArray();

        // シナリオ処理開始
        StartCoroutine(ScenarioParser());
    }

    // シナリオ処理
    private IEnumerator ScenarioParser()
    {
        foreach (string data in ScenarioData)
        {
            CurrentIndex = 0;
            CurrentLine = data;
            string line = string.Empty;
            while (CurrentIndex < CurrentLine.Length)
            {
                // 1文字取り出す
                string character = GetNextCharacter();
                // 特殊な文字処理か判定し結果を格納
                line += EffectCharacter(character);

                // Text表示用の閉じタグ
                string option = isBold ? "</b>" : "";
                option += isItalic ? "</i>" : "";
                LineText.text = line + option;

                // ボタンが押されていた時はウエイトなし
                if (!Input.GetMouseButton(0))
                {
                    yield return new WaitForSeconds(TextSpeed);
                }
            }
            // 表示し終えた後の待ち処理
            yield return new WaitForSeconds(CompleteLineDelay);

            // ボタンが押されるまで待つ処理実行
            yield return StartCoroutine(WaitButtoin());
        }
    }

    // ボタンが押されるまで待つ
    private IEnumerator WaitButtoin()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
    }

    // シナリオより1文字取り出す
    private string GetNextCharacter()
    {
        string character = CurrentLine.Substring(CurrentIndex, 1);
        CurrentIndex++;
        return character;
    }

    // 指定された文字まで取得。主に閉じタグの検索に使用
    private string GetNextCharacter(string commandTag)
    {
        string command = CurrentLine.Substring(CurrentIndex, CurrentLine.IndexOf(commandTag, CurrentIndex) - CurrentIndex);
        CurrentIndex += command.Length + 1;
        return command;
    }

    // 特殊文字処理
    private string EffectCharacter(string character)
    {
        // \文字処理
        if (character == @"\")
        {
            switch (GetNextCharacter())
            {
                case "n":
                    return "\n";
                default:

                    break;
            }
        }


        // タグ処理
        if (character == "<")
        {
            string command = GetNextCharacter(">");
            switch (command.ToLower())
            {
                case "b":
                    isBold = true;
                    return "<b>";
                case "/b":
                    isBold = false;
                    return "</b>";
                case "i":
                    isItalic = true;
                    return "<i>";
                case "/i":
                    isItalic = false;
                    return "</i>";
                default:
                    break;
            }
        }
 
        // グラフィックタグ処理
        if (character == "[")
        {
            char[] spliter = { '=', '\"', ',' };
            string tag = GetNextCharacter("]");
            string[] tags = tag.ToLower().Split(spliter);
            switch (tags[0])
            {
                case "character":
                    CharacterManager.Instance.SetCharacter(int.Parse(tags[1]));
                    return string.Empty;
                case "background":
                    BackgroundManager.Instance.SetBackground(int.Parse(tags[1]));
                    return string.Empty;
                default:
                    return string.Empty;
            }

        }

        return character;
    }

}