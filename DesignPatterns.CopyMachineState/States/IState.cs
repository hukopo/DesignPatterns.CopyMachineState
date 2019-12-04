using System;

namespace DesignPatterns.CopyMachineState.States
{
    public class CopyMachineContext
    {
        public IState State { get; set; }
        public IDevice Device { get; set; }
        public PrintDocument Document { get; set; }

        public int Money;

        public CopyMachineContext()
        {
            State = new InitState();
        }

        public void PutMoney(int money)
        {
            State.PutMoney(this, money);
        }

        public void ChooseDevice(IDevice device)
        {
            State.ChooseDevice(this, device);
        }

        public void ChooseDocument(string documentName)
        {
            State.ChooseDocument(this, documentName);
        }

        public void PrintDocument()
        {
            State.PrintDocument(this);
        }

        public int GetChange()
        {
            return State.GetChange(this);
        }
    }


    public interface IState
    {
        void PutMoney(CopyMachineContext copyMachineContext, int money);
        void ChooseDevice(CopyMachineContext copyMachineContext, IDevice device);
        void ChooseDocument(CopyMachineContext copyMachineContext, string documentName);
        void PrintDocument(CopyMachineContext copyMachineContext);
        int GetChange(CopyMachineContext copyMachineContext);
    }


    public abstract class BaseState : IState
    {
        protected abstract void DefaultSateError();

        public virtual void PutMoney(CopyMachineContext copyMachineContext, int money)
        {
            DefaultSateError();
        }

        public virtual void ChooseDevice(CopyMachineContext copyMachineContext, IDevice device)
        {
            DefaultSateError();
        }

        public virtual void ChooseDocument(CopyMachineContext name, string documentName)
        {
            DefaultSateError();
        }

        public virtual void PrintDocument(CopyMachineContext copyMachineContext)
        {
            DefaultSateError();
        }

        public virtual int GetChange(CopyMachineContext copyMachineContext)
        {
            var money = copyMachineContext.Money;
            copyMachineContext.Money = 0;
            copyMachineContext.State = new InitState();
            return money;
        }
    }

    public class InitState : BaseState
    {
        public override void PutMoney(CopyMachineContext copyMachineContext, int money)
        {
            copyMachineContext.Money += money;
            copyMachineContext.State = new ChooseDeviceState();
        }

        protected override void DefaultSateError()
        {
            throw new Exception("Для дальнейших действий пополнитее баланс");
        }

    }

    public class ChooseDeviceState : BaseState
    {
        protected override void DefaultSateError()
        {
            throw new Exception("Для дальнейших дествий нужно выбрать устройство для печати");
        }

        public override void PutMoney(CopyMachineContext copyMachineContext, int money)
        {
            //это же странно, нет?  но я так понял задание...
            throw new Exception("На данном этапе пополнить баланс не возможно");
        }

        public override void ChooseDevice(CopyMachineContext copyMachineContext, IDevice device)
        {
            copyMachineContext.Device = device;
            copyMachineContext.State = new ChooseDocumentState();
        }

    }

    public class ChooseDocumentState : BaseState
    {
        protected override void DefaultSateError()
        {
            throw new Exception("Для дальнейших дествий нужно выбрать документ для печати");
        }

        public override void ChooseDocument(CopyMachineContext copyMachineContext, string documentName)
        {
            copyMachineContext.Document = copyMachineContext.Device.GetDocument(documentName);
            copyMachineContext.State = new PrintState();
        }

    }

    public class PrintState : BaseState
    {
        protected override void DefaultSateError()
        {
            throw new Exception("Для дальнейший действий вам необходимо распечатать документ");
        }

        public override void PrintDocument(CopyMachineContext copyMachineContext)
        {
            Console.WriteLine(copyMachineContext.Document.data);
            copyMachineContext.Money--;
            copyMachineContext.State = new ChooseDocumentState();
        }
    }
}