using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

[Serializable]
public class ObjectPool<T>
{
    private readonly Queue<T> pool = new Queue<T>();
    private readonly HashSet<T> activeObjects = new HashSet<T>();
    private readonly Dictionary<T, CancellationTokenSource> activeTokens = new Dictionary<T, CancellationTokenSource>();
    private readonly Func<T> createFunc;
    private readonly Action<T> onGet;
    private readonly Action<T> onRelease;

    // IEnumerable<T>型で公開するとEnqueue/Dequeueできない
    public IEnumerable<T> Pool => pool;

    public ObjectPool(Func<T> createFunc, Action<T> onGet = null, Action<T> onRelease = null, int initialSize = 0)
    {
        this.createFunc = createFunc ?? throw new ArgumentNullException(nameof(createFunc));
        this.onGet = onGet;
        this.onRelease = onRelease;

        for (int i = 0; i < initialSize; i++)
        {
            pool.Enqueue(createFunc());
        }
    }

    /// <summary>
    /// オブジェクトを取得
    /// </summary>
    /// <param name="autoReleaseDelay">自動でReleaseするまでの秒数（<=0 の場合は自動リリースなし）</param>
    public T Get(float autoReleaseDelay = 0f)
    {
        T obj = default;

        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
        }
        else
        {
            obj = createFunc();
        }

        activeObjects.Add(obj);
        onGet?.Invoke(obj);

        if (autoReleaseDelay > 0f)
        {
            var cts = new CancellationTokenSource();
            activeTokens[obj] = cts;
            AutoReleaseAsync(obj, autoReleaseDelay).Forget();
        }

        return obj;
    }

    /// <summary>
    /// オブジェクトを手動で返却
    /// </summary>
    public void Release(T obj)
    {
        if (!activeObjects.Contains(obj)) return;

        // キャンセルトークンをキャンセル
        if (activeTokens.TryGetValue(obj, out var cts))
        {
            cts?.Cancel();
            cts?.Dispose();
            activeTokens.Remove(obj);
        }

        activeObjects.Remove(obj);
        onRelease?.Invoke(obj);
        pool.Enqueue(obj);
    }

    /// <summary>
    /// 一定時間後に自動でリリースする処理
    /// </summary>
    private async UniTaskVoid AutoReleaseAsync(T obj, float delaySeconds)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(delaySeconds));
        Release(obj);
    }

    /// <summary>
    /// すべてのアクティブなオブジェクトをクリーンアップ
    /// </summary>
    public void Cleanup()
    {
        foreach (var cts in activeTokens.Values)
        {
            cts?.Cancel();
            cts?.Dispose();
        }
        activeTokens.Clear();
        activeObjects.Clear();
        pool.Clear();
    }
}
