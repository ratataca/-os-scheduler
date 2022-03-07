using MsOSProgram.core.Algorithm;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
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

namespace MsOSProgram.ui.components.Workspace
{
    /// <summary>
    /// ProcessTableitem.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ProcessTableitem : UserControl 
    {
        public string proId = "";
        public int AT = 0;
        public int BT = 0;
        List<SolidColorBrush> colorTable = new List<SolidColorBrush>();

        public ProcessTableitem()
        {
            InitializeComponent();
            
            colorTable.Add(new SolidColorBrush(Color.FromRgb(255, 0, 0)));
            //colorTable.Add(new SolidColorBrush(Color.FromRgb(255, 162, 0)));
            colorTable.Add(new SolidColorBrush(Color.FromRgb(255, 251, 0)));
            colorTable.Add(new SolidColorBrush(Color.FromRgb(183, 255, 0)));
            colorTable.Add(new SolidColorBrush(Color.FromRgb(0, 157, 255)));
            colorTable.Add(new SolidColorBrush(Color.FromRgb(0, 60, 255)));
            colorTable.Add(new SolidColorBrush(Color.FromRgb(0, 19, 255)));
            colorTable.Add(new SolidColorBrush(Color.FromRgb(93, 0, 255)));
            colorTable.Add(new SolidColorBrush(Color.FromRgb(187, 0, 255)));
            colorTable.Add(new SolidColorBrush(Color.FromRgb(255, 0, 251)));
            colorTable.Add(new SolidColorBrush(Color.FromRgb(255, 0, 106)));
            
        }

        public void init(string processId, string atInput, string btInput)
        {
            // process들의
            int myProcessId = Convert.ToInt32(processId.Split("p")[1]);
            RectangleProcessColor.Fill = colorTable[myProcessId - 1];
            
            TextBlockProcessId.Text = processId;
            
            TextBlockProcessAT.Text = atInput;
            TextBlockProcessBT.Text = btInput;

            proId = processId;
            AT = Convert.ToInt32(atInput);
            BT = Convert.ToInt32(btInput);
        }
    }
}
