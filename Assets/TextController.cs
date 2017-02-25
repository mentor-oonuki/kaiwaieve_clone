using UnityEngine;
using System.Collections;
using UnityEngine.UI;  // UI要素に使用するため

public class TextController : MonoBehaviour {

	const float TEXT_SPEED = 0.5F;
	const float TEXT_SPEED_STRING = 0.05F;
	const float COMPLETE_LINE_DELAY = 0.3F;

	[SerializeField] Text lineText;       // 文字表示Text
	// [SerializeField] string[] scenarios;  // 会話内容
	string[] scenarios;  // 会話内容

	float textSpeed = 0;                            // 表示速度
	float completeLineDelay = COMPLETE_LINE_DELAY;  // 表示し終えた後の待ち時間
	int currentLine = 0;                            // 表示している行数
	string currentText = string.Empty;              // 表示している文字
	bool isCompleteLine = false;                    // 1文が全部表示されたか？



	// Use this for initialization
	void Start () {
		ReadCSV readCSV = new ReadCSV ();
		readCSV.Read();
		scenarios = readCSV.csvDatas.ToArray (); // public だから読める listという文字数が決まっていない型から配列に変換して入れている
		Show ();


	}


	/// <summary>
	/// 会話シーンの表示だよ。
	/// </summary>


	// Update is called once per frame
	void Show () {
	
		Initialize ();
		StartCoroutine (ScenarioCoroutine());
	}


	/// <summary>
	/// 初期化
	/// </summary>

	void Initialize(){
	
		isCompleteLine = false;
		lineText.text = "";
		currentText = scenarios [currentLine];

		textSpeed = TEXT_SPEED + (currentText.Length * TEXT_SPEED_STRING);

		LineUpdate();

	}


	/// <summary>
	/// 会話シーン更新
	/// </summary>
	/// <returns>The coroutine.</returns>

	IEnumerator ScenarioCoroutine(){

		while (true) {
		
			yield return null;

			// 次の内容へ
			if(isCompleteLine && Input.GetMouseButton(0)){
				yield return new WaitForSeconds (completeLineDelay);

				if(currentLine > scenarios.Length - 1){
					ScenarioEnd ();
					yield break;
				}

				Initialize ();

			}

			// 表示中にボタンが押されたら全部表示
			else if(!isCompleteLine && Input.GetMouseButton(0)){
				iTween.Stop ();
				TextUpdate (currentText.Length); // 全部表示
				TextEnd();
				yield return new WaitForSeconds (completeLineDelay);
			}

		}

	}


	/// <summary>
	/// 文字を少しずつ表示
	/// </summary>

	void LineUpdate(){
	
		iTween.ValueTo (this.gameObject, iTween.Hash (
			"from", 0,
			"to", currentText.Length,
			"time", textSpeed,
			"onupdate", "TextUpdate",
			"oncompletetarget", this.gameObject,
			"oncomplete", "TextEnd"
		));

	}


	/// <summary>
	/// 表示文字更新
	/// </summary>
	/// <param name="lineCount">Line count.</param>

	void TextUpdate(int lineCount){
		
		lineText.text = currentText.Substring (0, lineCount);

	}


	/// <summary>
	/// 表示完了
	/// </summary>

	void TextEnd(){

		Debug.Log("表示完了！");
		isCompleteLine = true;
		currentLine++;

	}


	/// <summary>
	/// 会話終了
	/// </summary>

	void ScenarioEnd(){
	
		Debug.Log ("会話終了！");
	
	}



	// Update is called once per frame
	void Update(){
		
	}
}
