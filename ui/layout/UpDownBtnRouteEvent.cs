using System.Windows;

namespace MsOSProgram.ui.layout
{
    internal class UpDownBtnRouteEvent : RoutedEventArgs
    {
        public int ProcessorNum { get; set; }

        public UpDownBtnRouteEvent(RoutedEvent routedEvent, int ProcessorNum) : base(routedEvent)
        {
            this.ProcessorNum = ProcessorNum;
        }
    }
}