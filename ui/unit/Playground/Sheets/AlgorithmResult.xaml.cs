using MsOSProgram.core.Algorithm;
using MsOSProgram.ui.layout;
using MsOSProgram.ui.unit.Playground.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace MsOSProgram.ui.unit.Playground.Sheets
{
    /// <summary>
    /// AlgorithmResult.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AlgorithmResult : UserControl
    {
        SolidColorBrush nullColor;
        List<SolidColorBrush> colorTable = new List<SolidColorBrush>();

        List<KeyValuePair<string, ProcessResultTableItem>> printProcessList ;
        List<CpuVisualizationItem> printCpuList = new List<CpuVisualizationItem>();
        ProcessContainer allProcessContainer;

        // CPU Visualization Item
        const int CPU_VIS_ITEM_HEIGHT = 120;
        const int CPU_VIS_ITEM_INTERVAL = 120;
        int currentCpuItemInterval = 0;

        // Table Item
        const int TABLE_ITEM_HEIGHT = 60;
        const int TABLE_ITEM_INTERVAL = 60;
        int currentTableItemInterval = 0;

        object algObject;

        System.Timers.Timer timerTick;
        int time = 0;
        int checkBt = 0;

        int processCount = 0;


        dynamic alg;

        public bool isSimulationOn = false;

        #region
        public AlgorithmResult(int cpuCount, object algorithm)
        {
            InitializeComponent();
            
            // 각 ui sheet가 가지는 알고리즘
            algObject = algorithm;
            for (int i = 0; i < cpuCount; i++) {
                this.addCpuVisualization();
            }

            timerTick = new System.Timers.Timer();
            timerTick.Interval = 1000;      // 간격
            timerTick.Elapsed += new System.Timers.ElapsedEventHandler(timerElapsed);       // 흐름


            #region 색에 관련된 정의
            colorTable.Add(new SolidColorBrush(Color.FromRgb(255, 0, 0)));
            //colorTable.Add(new SolidColorBrush(Color.FromRgb(255, 162, 0)));
            colorTable.Add(new SolidColorBrush(Color.FromRgb(255, 251, 0)));
            colorTable.Add(new SolidColorBrush(Color.FromRgb(183, 255, 0)));
            colorTable.Add(new SolidColorBrush(Color.FromRgb(0, 157, 255)));
            colorTable.Add(new SolidColorBrush(Color.FromRgb(0, 89, 255)));
            colorTable.Add(new SolidColorBrush(Color.FromRgb(0, 21, 255)));
            colorTable.Add(new SolidColorBrush(Color.FromRgb(93, 0, 255)));
            colorTable.Add(new SolidColorBrush(Color.FromRgb(187, 0, 255)));
            colorTable.Add(new SolidColorBrush(Color.FromRgb(255, 0, 251)));
            colorTable.Add(new SolidColorBrush(Color.FromRgb(255, 0, 106)));
            colorTable.Add(new SolidColorBrush(Color.FromRgb(255, 0, 0)));

            nullColor = new SolidColorBrush(Color.FromRgb(92, 92, 92));
            #endregion
        }


        public void addProcessResultItem(string pid, string at, string bt, string wt, string tt, string ntt)
        {
            ProcessResultTableItem item = new ProcessResultTableItem()
            {
                Height = TABLE_ITEM_HEIGHT,

                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, currentTableItemInterval, 0, 0),
            };
            
            item.setItemContent(pid, at, bt, wt, tt, ntt);

            printProcessList.Add(new KeyValuePair<string, ProcessResultTableItem>(pid, item));
            currentTableItemInterval += TABLE_ITEM_INTERVAL;

            gridProcessTableResult.Children.Add(item);
         
        }

        public void changeProcessResultItem(string pid, string at, string bt, string wt, string tt, string ntt)
        {
            ProcessResultTableItem item;

            foreach (KeyValuePair<string, ProcessResultTableItem> kvp in printProcessList)
            {
                if (kvp.Key == pid)
                {
                    item = kvp.Value;
                    item.setItemContent(pid, at, bt, wt, tt, ntt);
                    item.Background = new SolidColorBrush(Color.FromRgb(255, 157, 0));
                    
                }
                else {
                    item = kvp.Value;
                    item.Background = null;
                }
                
            }
        }
        #region cpu 공간 가시화
        public void addCpuVisualization()
        {
            CpuVisualizationItem item = new CpuVisualizationItem()
            {
                Height = CPU_VIS_ITEM_HEIGHT,

                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, currentCpuItemInterval, 0, 0),
            };

            currentCpuItemInterval += CPU_VIS_ITEM_INTERVAL;

            //item.runPer1S("p1", 20);
            //item.runPer1S("p1", 45);
            //item.runPer1S("p1", 50);

            gridCpuVisualization.Children.Add(item);
            printCpuList.Add(item);
        }
        #endregion  

        public void addCpuGraphVisualization(int cpuId, string id, int time)
        {
            if (id == null)
            {
                printCpuList[cpuId].drawProcess(id, time, nullColor);
            }
            else {
                int processId = Convert.ToInt32(id.Split("p")[1]);
                printCpuList[cpuId].drawProcess(id, time, colorTable[processId - 1]);
            }
            
        }
        #endregion


        // 시뮬레이션 버튼 클릭시#########################################################################
        private void BtnSimulation_Click(object sender, RoutedEventArgs e)
        {
            // TODO : 초기 테이블 데이터 입력받기 or start를 눌리고만 작동하기
            init(allProcessContainer);

            if (timerTick.Enabled)
                return;

            alg = Convert.ChangeType(algObject, algObject.GetType());
            alg.initClock();
            startTimerTick();
        }

        private void startTimerTick()
        {
            if (timerTick.Enabled)
                return;

            // For debugging
            timerTick.Start();

            isSimulationOn = true;
        }

        //// 실제 updata가 일어나는 부분
        private void timerElapsed(object sender, ElapsedEventArgs e)
        {
            
            Console.WriteLine("1초가 되었습니다.");
            
            timerTick.Stop();

             // time 시간과 비교해서 있으면 process 없으면 null
             Process nowInputProcess = allProcessContainer.UseAtNowProcess(time);
             Process nowResultProcess = null;

            // input process의 유무
            if (nowInputProcess != null)
            {
                nowResultProcess = alg.run_per_1s(
                                    new Process(
                                        nowInputProcess.processId,
                                        nowInputProcess.arrTime,
                                        nowInputProcess.burstTime
                                        )
                                    );                
            }
            else
            {
                nowResultProcess = alg.run_per_1s(null);
            }

            this.Dispatcher.BeginInvoke(
                DispatcherPriority.Background, 
                new Action(() =>
            {
                // 알고리즘을 거친후 output의 유무
                if (nowResultProcess != null)
                {
                    // cpu 공간에 그래프를 그리기 위해서
                    addCpuGraphVisualization(
                        0,                                  // CPU ID
                        nowResultProcess.processId,         // Process ID
                        time                                // Clock
                        );

                    // 테이블에 알맞은 값 넣기
                    changeProcessResultItem(
                        nowResultProcess.processId,
                        nowResultProcess.arrTime.ToString(),
                        nowResultProcess.burstTime.ToString(),
                        nowResultProcess.waitingTime.ToString(),
                        nowResultProcess.turnAroundTime.ToString(),
                        nowResultProcess.normalizedTT.ToString()
                        );

                    if (nowResultProcess.burstTime == 0)
                    {
                        checkBt++;
                        
                    }
                }
                else
                {
                    if (processCount != checkBt)
                        addCpuGraphVisualization(0, null, time);
                }
            })); ;

                time++;

            if (processCount != checkBt)
                timerTick.Start();
            else {
                this.Dispatcher.BeginInvoke(
                DispatcherPriority.Background,
                new Action(() =>
                {
                    GridAVGTT.Visibility = Visibility.Visible;
                    Dictionary<string, string> myResultDictionary = alg.getAllResult();

                    Console.WriteLine("=======================");
                    Console.WriteLine("AVERAGE_WAITING_TIME : " + myResultDictionary["AVERAGE_WAITING_TIME"]);
                    Console.WriteLine("CONTEXT_SWITCHING : " + myResultDictionary["CONTEXT_SWITCHING"]);
                    Console.WriteLine("=======================");
                    ChangeAverageResponseTime(myResultDictionary["AVERAGE_WAITING_TIME"]);
                }));
                    

                isSimulationOn = false;

            }

        }


        public void StartBtnClikAlgoritm(ProcessContainer allProcessContainer) {

            // ================================================================= //
            // Variable Definitions
            this.allProcessContainer = null;
            
            this.allProcessContainer = allProcessContainer;

            processCount = allProcessContainer.Count();
            int time = 0;
            int checkBt = 0;
            

            // Algorithm 가져오기
            alg = Convert.ChangeType(algObject, algObject.GetType());
            
            alg.initClock();
            // ================================================================= //

            init(allProcessContainer);

            while (processCount != checkBt)
            {

                // time 시간과 비교해서 있으면 process 없으면 null
                Process nowInputProcess = allProcessContainer.UseAtNowProcess(time);
                Process nowResultProcess = null;


                // input process의 유무
                if (nowInputProcess != null)
                {
                    nowResultProcess = alg.run_per_1s(
                                       new Process(
                                           nowInputProcess.processId,
                                           nowInputProcess.arrTime,
                                           nowInputProcess.burstTime
                                           )
                                       );
                }
                else
                {
                    nowResultProcess = alg.run_per_1s(null);
                }

                // 알고리즘을 거친후 output의 유무
                if (nowResultProcess != null)
                {
                    // cpu 공간에 그래프를 그리기 위해서
                    addCpuGraphVisualization(
                        0,
                        nowResultProcess.processId,
                        time + 1
                        );

                    // 테이블에 알맞은 값 넣기
                    changeProcessResultItem(
                        nowResultProcess.processId,
                        nowResultProcess.arrTime.ToString(),
                        nowResultProcess.burstTime.ToString(),
                        nowResultProcess.waitingTime.ToString(),
                        nowResultProcess.turnAroundTime.ToString(),
                        nowResultProcess.normalizedTT.ToString()
                        );
                    if (nowResultProcess.burstTime == 0)
                    {
                        checkBt++;
                    }
                }
                else
                {
                    addCpuGraphVisualization(0, null, time + 1);
                }

                time++;
            }
            

            Dictionary<string, string> myResultDictionary = alg.getAllResult();
            
            Console.WriteLine("=======================");
            Console.WriteLine("AVERAGE_WAITING_TIME : " +  myResultDictionary["AVERAGE_WAITING_TIME"]);
            Console.WriteLine("CONTEXT_SWITCHING : " + myResultDictionary["CONTEXT_SWITCHING"]);
            Console.WriteLine("=======================");
            ChangeAverageResponseTime(myResultDictionary["AVERAGE_WAITING_TIME"]);

            alg.initClock();
            //TODO 그려주는 ui

        }

        private void init(ProcessContainer allProcessContainer)
        {
            //initClock()

            GridAVGTT.Visibility = Visibility.Hidden;
            time = 0;
            checkBt = 0;
            printProcessList = new List<KeyValuePair<string, ProcessResultTableItem>>();
            currentTableItemInterval = 0;

            // CPU 시각화 하는 부분 초기화
            printCpuList[0].clear();

            // 이미 존재하는 테이블 제거
            gridProcessTableResult.Children.Clear();

            if (allProcessContainer == null)
                return;

            // TODO : 초기 테이블 데이터 입력하기
            for (int i = 0; i < allProcessContainer.Count(); i++)
            {
                // 컨테이너 내부에 있는 정렬된 process리스트 중 i번째 불러옴
                Process intiTableInfo = allProcessContainer.getOriginalprocessList(i);
                addProcessResultItem(
                        intiTableInfo.processId,
                        intiTableInfo.arrTime.ToString(),
                        intiTableInfo.burstTime.ToString(),
                        intiTableInfo.waitingTime.ToString(),
                        intiTableInfo.turnAroundTime.ToString(),
                        intiTableInfo.normalizedTT.ToString()
                     );
            }
        }

        public void ChangeAverageResponseTime(string AverTT) {
            GridAVGTT.Visibility = Visibility.Visible;
            TextBlockAverageResponseTime.Text = AverTT;


        }
    }
}
