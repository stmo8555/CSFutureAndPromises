using Futures;
using System.Threading;
using System;

var promise = new Promise<int>();
var future = promise.GetFuture();

var t = new Thread(() => f(promise));
t.Start();

var result = future.Get();

Console.WriteLine("Result from promise: " + result);

t.Join();

System.Console.WriteLine("Thread done");

static void f(Promise<int> promise)
{
    int i = 0;
    while (i < 5)
    {
        Console.WriteLine("val = " + i);
        i++;
        Thread.Sleep(1000);
    }

    promise.SetValue(i);
}