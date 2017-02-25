using System.IO;
using UnityEngine;
using System.Collections.Generic;


public class FileReader
{

    // ファイルデータ格納List
    private List<string> FileData = new List<string>();


    // ファイル読み込み
    public List<string> Read(string filename)
    {
        // Resourcesフォルダからファイル読み込み
        TextAsset textAsset = Resources.Load(filename) as TextAsset;

        // StringReader生成
        StringReader reader = new StringReader(textAsset.text);

        // 一行ずつ読み込む
        while (reader.Peek() > -1)
        {
            // 一行読み込み
            string line = reader.ReadLine();
            // ファイルデータ格納Listに追加
            FileData.Add(line);
        }

        return FileData;
    }

}