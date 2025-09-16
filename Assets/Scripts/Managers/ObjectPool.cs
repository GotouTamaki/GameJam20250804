using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class ObjectPool<T>
{
    private readonly Queue<T> pool = new Queue<T>();
    private readonly Func<T> createFunc;
    private readonly Action<T> onGet;
    private readonly Action<T> onRelease;

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
        var obj = pool.Count > 0 ? pool.Dequeue() : createFunc();
        onGet?.Invoke(obj);

        if (autoReleaseDelay > 0f)
        {
            AutoReleaseAsync(obj, autoReleaseDelay).Forget();
        }

        return obj;
    }

    /// <summary>
    /// オブジェクトを手動で返却
    /// </summary>
    public void Release(T obj)
    {
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
}
