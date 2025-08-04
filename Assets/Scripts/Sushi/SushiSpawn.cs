using UnityEngine;
using System.Collections.Generic;

public class SushiSpawn : MonoBehaviour
{
    [SerializeField] List<GameObject> SushiPrefabs;                     // 寿司プレハブのリスト（インスペクターで設定）
    [SerializeField, Range(1f, 10f)] public float SpawnInterval = 2f;   // 出現間隔（秒）
    [SerializeField] public Transform SpawnPoint;                       // 出現位置（未設定なら自分の位置）

    public SushiManager sushiManager;                                   // SushiManager参照
    public float Timer = 0;                                             // スポーン用タイマー
    public bool IsSpawning = false;                                     // 現在スポーン中かどうか
    public List<GameObject> SushiList = new();                          // 生成済み寿司のリスト

    void Start()
    {
        sushiManager = Object.FindAnyObjectByType<SushiManager>();      // シーン内から SushiManager を取得
        if (sushiManager == null)
        {
            Debug.LogError("SushiManagerが見つかりません");            // 見つからない場合エラーログ
        }
    }

    void Update()
    {
        if (sushiManager == null) return;                               // マネージャーがなければ何もしない

        /*
        if (sushiManager.CanSpawn())                                    // スポーン条件を満たすか
        {
            if (!IsSpawning)
            {
                IsSpawning = true;                                      // スポーン開始
                Timer = 0;
            }

            Timer += Time.deltaTime;                                    // タイマー加算
            if (Timer >= SpawnInterval)
            {
                Timer = 0;
                SpawnSushi();                                           // 寿司をスポーン
            }

            CleanUpOffSetScreenSushis();                                // 画面外の寿司を削除
        }
        else
        {
            if (IsSpawning)
            {
                IsSpawning = false;                                     // スポーン終了
            }
        }
        */
    }

    void SpawnSushi()
    {
        if (SushiPrefabs.Count == 0) return;                            // プレハブが空なら何もしない

        int index = Random.Range(0, SushiPrefabs.Count);                // ランダムで寿司を選ぶ
        Vector3 pos;

        // スポーン位置の決定（SpawnPoint が設定されていればその位置、なければ自分の位置）
        if (SpawnPoint != null)
        {
            pos = SpawnPoint.position;
        }
        else
        {
            pos = transform.position;
        }

        GameObject Sushi = Instantiate(SushiPrefabs[index], pos, Quaternion.identity); // 寿司を生成
        SushiList.Add(Sushi);                                           // リストに追加
    }

    void CleanUpOffSetScreenSushis()
    {
        Camera cam = Camera.main;                                       // メインカメラ取得
        if (cam == null) return;

        for (int i = SushiList.Count - 1; i >= 0; i--)                  // 後ろからループ（削除に備える）
        {
            GameObject sushi = SushiList[i];

            if (sushi == null)
            {
                SushiList.RemoveAt(i);                                  // null の場合はリストから除去
                continue;
            }

            SpriteRenderer sr = sushi.GetComponent<SpriteRenderer>();   // SpriteRenderer取得
            if (sr == null) continue;

            Bounds bounds = sr.bounds;                                  // スプライトの境界
            Vector3 min = cam.WorldToViewportPoint(bounds.min);         // スプライトの左下を画面座標に変換
            Vector3 max = cam.WorldToViewportPoint(bounds.max);         // スプライトの右上を画面座標に変換

            // スプライトが画面の左・右・上・下いずれかに完全に外れているか判定
            bool fullyOutOfScreen = max.x < 0 || min.x > 1 || max.y < 0 || min.y > 1;

            if (fullyOutOfScreen)
            {
                Destroy(sushi);                                         // 寿司オブジェクトを削除
                SushiList.RemoveAt(i);                                  // リストからも削除
            }
        }
    }
}
