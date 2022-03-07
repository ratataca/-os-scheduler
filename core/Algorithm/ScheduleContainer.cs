using System;
using System.Collections.Generic;
using System.Text;

namespace MsOSProgram.core.Algorithm
{
    class ScheduleContainer
    {
        List<Process> processList = new List<Process>();
        List<double> btList = new List<double>();

        public void init() {
            processList = new List<Process>();
            btList = new List<double>();
        }

        public void add(Process process)
        {   
            processList.Add(process);
            btList.Add(process.burstTime);
        }

        public int search(string pid)
        {
            for (int i = 0; i < processList.Count; i++)
            {
                if (processList[i].processId == pid)
                    return i;
            }

            return -1;
        }

        public void changeWT(int currentClock)
        {
            for (int index = 0; index < processList.Count; index++)
            {
                if (processList[index].burstTime != 0)
                {
                    processList[index].waitingTime = currentClock - processList[index].arrTime;
                }
            }
        }

        public Process changeTT(string pid, int currentClock)
        {
            int index = search(pid);

            if (index == -1)
                return null;

            processList[index].turnAroundTime = currentClock - processList[index].arrTime;


            // Waiting time
            processList[index].waitingTime = processList[index].turnAroundTime - (int)btList[index];

            // NTT
            processList[index].normalizedTT = (int)((processList[index].turnAroundTime / (double)btList[index]) * 10 + 0.5) / 10.0;

            Console.WriteLine(" ** =========================== **");
            Console.WriteLine(">>> Finish process : " + processList[index].processId);
            Console.WriteLine(" ** =========================== **");
            Console.WriteLine(">>> turnAroundTime : " + processList[index].turnAroundTime);
            Console.WriteLine(">>> waitingTime : " + processList[index].waitingTime);
            Console.WriteLine(">>> normalizedTT : " + processList[index].normalizedTT);
            Console.WriteLine();
            Console.WriteLine();

            return processList[index];
        }

        public bool check(string processId, int arrTime, int burstTime, int watingTime, int turnAroundTime, double normalizedTT)
        {
            return processList[search(processId)].check(processId, arrTime, burstTime, watingTime, turnAroundTime, normalizedTT);
        }

        public Process show()
        {
            return null;
        }


        public void init(string processId) {
            int id = Convert.ToInt32(processId.Split("p")[1]);
            
        }
    }
}
