using System.Collections.Generic;
using UnityEngine;

public enum Sushi //列挙型で寿司の種類を定義
{
    a, b, c, d, e, f, g, h, i, j,
}

public class SushiMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List<Sushi> allSushis = new List<Sushi>((Sushi[])System.Enum.GetValues(typeof(Sushi))); //全ての寿司をリストに変換
        Shuffle(allSushis);                                                                     //シャッフルし、順番をランダムに
        List<Sushi> selected = allSushis.GetRange(0, 4);                                        //最初の４つを選択

        foreach (Sushi s in selected)                                                           //選ばれた寿司を１つずつ処理
        {
            switch (s)                                                                          //寿司の種類ごとに分岐
            {
                case Sushi.a:                                                                   
                    Debug.Log("aが選ばれた");
                    break;

                case Sushi.b:
                    Debug.Log("bが選ばれた");
                    break;

                case Sushi.c:
                    Debug.Log("cが選ばれた");
                    break;

                case Sushi.d:
                    Debug.Log("dが選ばれた");
                    break;

                case Sushi.e:
                    Debug.Log("eが選ばれた");
                    break;

                case Sushi.f:
                    Debug.Log("fが選ばれた");
                    break;

                case Sushi.g:
                    Debug.Log("gが選ばれた");
                    break;

                case Sushi.h:
                    Debug.Log("hが選ばれた");
                    break;

                case Sushi.i:
                    Debug.Log("iが選ばれた");
                    break;

                case Sushi.j:
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
