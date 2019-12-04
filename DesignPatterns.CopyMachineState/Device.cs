using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatterns.CopyMachineState
{
    public interface IDevice
    {
        PrintDocument GetDocument(string documentName);
    }

    public class UsbDevice : IDevice
    {
        private PrintDocument[] documents;

        public UsbDevice(PrintDocument[] documents)
        {
            this.documents = documents;
        }

        public PrintDocument GetDocument(string documentName)
        {
            return documents.FirstOrDefault(d => d.name.Equals(documentName, StringComparison.OrdinalIgnoreCase));
        }
    }

    public class PrintDocument
    {
        public string name { get; set; }
        public string data { get; set; }
    }

    public class WiFiDevice : IDevice
    {
        private Dictionary<string, PrintDocument> documents;

        public WiFiDevice(Dictionary<string, PrintDocument> documents)
        {
            this.documents = documents;
        }

        public PrintDocument GetDocument(string documentName)
        {
            if (documents.TryGetValue(documentName, out var doc))
                return doc;
            throw new Exception("Документ не найден");
        }
    }

}