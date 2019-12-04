public class DataPoint<T>
{
    double timestamp;
    int id;
    T value;

    public DataPoint(double timestamp, int id, T value)
    {
        this.timestamp = timestamp;
        this.id = id;
        this.value = value;
    }

    public override string ToString()
    {
        return value.ToString();
    }
}
