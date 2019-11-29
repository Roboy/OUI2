using System.Collections.Generic;

public interface ISubscriber<T>
{
    void ReceiveUpdate(Queue<T> data);
}
