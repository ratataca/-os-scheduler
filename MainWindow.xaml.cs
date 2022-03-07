using MsOSProgram.core.Algorithm;
using MsOSProgram.ui.components.Workspace;
using MsOSProgram.ui.layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MsOSProgram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region 생성자
        public MainWindow()
        {
            InitializeComponent();

        }

        #endregion

        private void workSpace_StartBtnValueChangedEventHandler(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine(">>> Receive from WorkSpace xaml | Start Button");
            //Console.WriteLine("Receive from PlayGround xaml");

            BtnStartRouteEvent eventArgs = (BtnStartRouteEvent)e;

            int nowTimeQuantum = eventArgs.timeQuantum;
            
            List<ProcessTableitem> processTableItem = workSpace.getProcessTableItemList();

            if(processTableItem == null || processTableItem.Count == 0)
            {
                MessageBox.Show("프로세스를 추가한 후 다시 시작 버튼을 눌려주세요");
                return;
            }

            //playground.Visibility = Visibility.Visible;
            GridOnlyStartBtnClick.Visibility = Visibility.Hidden;

            //Console.Write(">>> Process Table Item Count : ");
            //Console.WriteLine(processTableItem.Count);
            
            // playground로 권한 넘기기 
            playground.PreStartButton(processTableItem, nowTimeQuantum);


            
            // cpu 수 
            //workSpace.getInformation();
        }

        //#region PlayGround에서 start 클릭 이벤트 발생 ->  메인 -> Workspace에 보내기
        //private void PlayGround_SimulationStartClick(object sender, RoutedEventArgs e)
        //{
          

        //}
        //#endregion

        //private void workSpace_numericUpDownCPUValueChangedEventHandler(object sender, RoutedEventArgs e)
        //{
        //    Console.WriteLine(">>> Receive from WorkSpace xaml");

        //    UpDownBtnRouteEvent eventArgs = (UpDownBtnRouteEvent)e;
        //    Console.WriteLine(">>> Processor Number : " + eventArgs.ProcessorNum.ToString());

        //    //playground.fromProcessorCountMakeGrid(Convert.ToInt32(eventArgs.ProcessorNum.ToString()));
        //}
    }

};
