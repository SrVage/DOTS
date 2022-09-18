namespace Code.Components.Interfaces
{
    public interface IPool<T> where T:class
    {
        T GetObject();
        void ReturnToPool(T transform);
    }
}