using MsOSProgram.ui.components.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MsOSProgram.ui.unit.Playground.Table
{
    /// <summary>
    /// CpuVisualizationItem.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CpuVisualizationItem : UserControl
    {
        const int CPU_GRAPH_UNIT_INTERVAL = 30;
        int currentCpuGraphUnitInterval = 0;

        public CpuVisualizationItem()
        {
            InitializeComponent();
        }

        public void drawProcess(string pid, int time, SolidColorBrush brush)
        {
            
            //currentCpuGraphUnitInterval += CPU_GRAPH_UNIT_INTERVAL;
            // TODO - pid 색을 고려 하여 넣기 Width = time * CPU_GRAPH_UNIT_INTERVAL,
            PlaygroundGraphPart endWorkProcess = new PlaygroundGraphPart()
            {
                Width = 30,
                Margin = new Thickness(CPU_GRAPH_UNIT_INTERVAL * time, 0, 0, 0),

                HorizontalAlignment = HorizontalAlignment.Left,
            };
            
            //endWorkProcess.GridGrapWidthName.Margin = new Thickness(graphMarginLeft + 5, 0, 0, 0);
            //endWorkProcess.GridGrapWidth.Margin = new Thickness(graphMarginLeft + 5, 0, 0, 0);

            endWorkProcess.init(pid, time ,brush);

            // ((time - arrTime) * CPU_GRAPH_UNIT_INTERVAL);


            // 실제 그림을 그리는 곳,,
            gridCpuGraph.Children.Add(endWorkProcess);
        }

        public void clear()
        {
            gridCpuGraph.Children.Clear();

            currentCpuGraphUnitInterval = 0;
        }
    }
}
