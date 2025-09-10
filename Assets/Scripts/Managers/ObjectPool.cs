using System;
using System.Collections.Generic;

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

    public T Get()
    {
        var obj = pool.Count > 0 ? pool.Dequeue() : createFunc();
        onGet?.Invoke(obj);
        return obj;
    }

    public void Release(T obj)
    {
        onRelease?.Invoke(obj);
        pool.Enqueue(obj);
    }
}
