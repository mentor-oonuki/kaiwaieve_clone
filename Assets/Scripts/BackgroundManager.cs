using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : SingletonMonoBehaviour<BackgroundManager>
{

    // 背景用Imageコンポーネント
    [SerializeField]
    private Image BackgroundImage;
    // 背景リスト
    [SerializeField]
    List<Sprite> BackgroundList = new List<Sprite>();


    // 背景表示
    public void SetBackground(int number)
    {
        BackgroundImage.sprite = BackgroundList[number];
    }

}
