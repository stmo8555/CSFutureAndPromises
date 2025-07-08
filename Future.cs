using System.Threading;
namespace Futures
{
    public class Future<T>
    {
        private SemaphoreSlim _signal;
        private readonly FutureState<T> _value;
        public Future(SemaphoreSlim signal, FutureState<T> val)
        {
            _signal = signal;
            _value = val;
        }

        public void Wait()
        {
            _signal.Wait();
        }
        public T Get()
        {
            Wait();
            if (_value.Value is null)
                throw new NullReferenceException();
            return _value.Value;
        }

    }

    public class Promise<T>
    {
        private readonly SemaphoreSlim _signal = new(0);
        private readonly FutureState<T> _value = new();

        public void SetValue(T val)
        {
            _value.Value = val;
            _signal.Release();
        }
        public Future<T> GetFuture()
        {
            var tmp = new Future<T>(_signal, _value);
            return tmp;
        }
    }

    public class FutureState<T>
    {
        public T? Value;
    }
}