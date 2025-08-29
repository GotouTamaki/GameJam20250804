using System.Collections.Generic;
using UnityEngine;

public class SushiMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List<SushiType> allSushis = new List<SushiType>((SushiType[])System.Enum.GetValues(typeof(SushiType))); //全ての寿司をリストに変換
        Shuffle(allSushis);                                                                     //シャッフルし、順番をランダムに
        List<SushiType> selected = allSushis.GetRange(0, 4);                                        //最初の４つを選択

        foreach (SushiType s in selected)                                                           //選ばれた寿司を１つずつ処理
        {
            switch (s)                                                                          //寿司の種類ごとに分岐
            {
                case SushiType.Squid:
                    Debug.Log("aが選ばれた");
                    break;

                case SushiType.Shrimp:
                    Debug.Log("bが選ばれた");
                    break;

                case SushiType.Tuna:
                    Debug.Log("cが選ばれた");
                    break;

                case SushiType.Parfait:
                    Debug.Log("dが選ばれた");
                    break;

                case SushiType.Omelet:
                    Debug.Log("eが選ばれた");
                    break;

                case SushiType.Bonito:
                    Debug.Log("fが選ばれた");
                    break;

                case SushiType.SalmonRoe:
                    Debug.Log("gが選ばれた");
                    break;

                case SushiType.Kappamaki:
                    Debug.Log("hが選ばれた");
                    break;

                case SushiType.Octopus:
                    Debug.Log("iが選ばれた");
                    break;

                case SushiType.Negitoro:
                    Debug.Log("jが選ばれた");
                    break;
            }
        }
    }

    void Shuffle<T>(List<T> list)                     //リストの要素をランダムな順序に並び替える
    {
        for(int i = list.Count - 1; i > 0; i--)       //逆順でループ
        {
            int r = Random.Range(0, i + 1);           //0からiの範囲でランダムなインデックスを取得
            (list[i], list[r]) = (list[r], list[i]);  //要素を入れ替える
        }
    }
}
