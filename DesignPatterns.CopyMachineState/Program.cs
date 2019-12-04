using System;
using DesignPatterns.CopyMachineState.States;

namespace DesignPatterns.CopyMachineState
{
    class Program
    {
        static void Main(string[] args)
        {
            var copyMachineContext = new CopyMachineContext();
            copyMachineContext.PutMoney(8);
            copyMachineContext.ChooseDevice(new UsbDevice(new []{new PrintDocument {data = "123", name = "doc1.txt"}, new PrintDocument { data = "222", name="doc2.txt"}}));
            copyMachineContext.ChooseDocument("doc1.txt");
            copyMachineContext.PrintDocument();
            copyMachineContext.ChooseDocument("doc2.txt");
            copyMachineContext.PrintDocument();
            Console.WriteLine($"сдача {copyMachineContext.GetChange()}");
        }
    }
}