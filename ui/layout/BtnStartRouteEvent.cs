using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace MsOSProgram.ui.layout
{
    class BtnStartRouteEvent : RoutedEventArgs
    {
        public int timeQuantum { get; set; }

        public BtnStartRouteEvent(RoutedEvent routedEvent, int timeQuantum) : base(routedEvent)
        {
            this.timeQuantum = timeQuantum;
        }
    }
}
