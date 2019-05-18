namespace Fuzzy
{
    public interface ISet<T>
    {
        T Sum(T other);
        T Multiplication(T other);
    }
}
