using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace MsOSProgram.ui.route_event
{
    class SimulationStartRouteEvent : RoutedEventArgs
    {
        public SimulationStartRouteEvent(RoutedEvent routedEvent) : base(routedEvent)
        {
        }
    }
}
