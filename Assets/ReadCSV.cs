using UnityEngine;
using System.Collections;
using System.Collections.Generic; // 
using System.IO; //IO は inout と output の略 StringReaderの方
public class  ReadCSV : MonoBehaviour {

	private string musicName; // 読み込むデータの名前
	// private string level; // 難易度
	private TextAsset csvFile; // CSVファイル
	public List<string> csvDatas = new List<string>(); // CSVの中身を入れるリスト
	private int height = 0; // CSVの行数

	public void Read(){
		musicName = "textsample"; // ファイル名
		// level = "0"; // 難易度
		csvFile = Resources.Load("CSV/" + musicName ) as TextAsset; /* Resouces/CSV下のCSV読み込み */
		StringReader reader = new StringReader(csvFile.text);

		while(reader.Peek() > -1) {
			string line = reader.ReadLine(); //１行づつ読み取る
			// csvDatas.Add(line.Split(',')); // リストに入れる
			csvDatas.Add(line); // CSVに格納される
			height++; // 行数加算
		}

	}


}
