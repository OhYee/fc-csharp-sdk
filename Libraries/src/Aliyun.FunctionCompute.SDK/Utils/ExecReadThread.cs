using System.Net.WebSockets;

class WebsocketReadSocket<T>
{
    private T data;
    public MyThread(T data)
    {
        var ws = new ClientWebSocket();
        this.data = data;
    }
    public void ThreadChild()
    {
        Console.WriteLine("Child Thread Start! Result:{0}", data);
    }
}