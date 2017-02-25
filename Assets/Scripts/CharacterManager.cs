using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterManager : SingletonMonoBehaviour<CharacterManager>
{

    // キャラクター用Imageコンポーネント
    [SerializeField]
    private Image CharacterImage;
    // キャラクターリスト
    [SerializeField]
    List<Sprite> CharacterList = new List<Sprite>();


    // キャラクター表示
    public void SetCharacter(int number)
    {
        CharacterImage.sprite = CharacterList[number];
    }

}
