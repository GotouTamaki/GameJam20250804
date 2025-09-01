using System.Collections.Generic;
using UnityEngine;

public class SushiMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List<SushiType> allSushis = new List<SushiType>((SushiType[])System.Enum.GetValues(typeof(SushiType))); //全ての寿司をリストに変換
        //Shuffle(allSushis);                                                                     //シャッフルし、順番をランダムに
        List<SushiType> selected = allSushis.GetRange(0, 4);                                        //最初の４つを選択

        foreach (SushiType s in selected)                                                           //選ばれた寿司を１つずつ処理
        {
            switch (s)                                                                          //寿司の種類ごとに分岐
            {
                case SushiType.Squid:
                    Debug.Log("Squidが選ばれた");
                    break;

                case SushiType.Shrimp:
                    Debug.Log("Shrimpが選ばれた");
                    break;

                case SushiType.Tuna:
                    Debug.Log("Tunaが選ばれた");
                    break;

                case SushiType.Parfait:
                    Debug.Log("Parfaitが選ばれた");
                    break;

                case SushiType.Omelet:
                    Debug.Log("Omeletが選ばれた");
                    break;

                case SushiType.Bonito:
                    Debug.Log("Bonitoが選ばれた");
                    break;

                case SushiType.SalmonRoe:
                    Debug.Log("SalmonRoeが選ばれた");
                    break;

                case SushiType.Kappamaki:
                    Debug.Log("Kappamakiが選ばれた");
                    break;

                case SushiType.Octopus:
                    Debug.Log("Octopusが選ばれた");
                    break;

                case SushiType.Negitoro:
                    Debug.Log("Negitoroが選ばれた");
                    break;
            }
        }
    }

    public void SelectSushiType()
    {

    }

    //void Shuffle<T>(List<T> list)                     //リストの要素をランダムな順序に並び替える
    //{
    //    for(int i = list.Count - 1; i > 0; i--)       //逆順でループ
    //    {
    //        int r = Random.Range(0, i + 1);           //0からiの範囲でランダムなインデックスを取得
    //        (list[i], list[r]) = (list[r], list[i]);  //要素を入れ替える
    //    }
    //}
}
