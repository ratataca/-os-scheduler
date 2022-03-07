using System;
using System.Collections.Generic;
using System.Text;

namespace MsOSProgram.core.Algorithm
{
    public class ProcessContainer
    {
        List<Process> processList;
        List<Process> processListSorted;
        public int allTime = -10000;
        

        public ProcessContainer()
        {
            processList = new List<Process>();
        }

        public void add(Process process)
        {
            processList.Add(process);
        }
        
        public bool check(int i, string processId, int arrTime, int burstTime, int watingTime, int turnAroundTime, double normalizedTT)
        {
            return processList[i].check(processId, arrTime, burstTime, watingTime, turnAroundTime, normalizedTT);
        }
        

        public int Count()
        {
            return processList.Count;
        }

        public List<Process> ProcessList()
        {
            return processList;
        }
        // time과 AT와 비교하여 해당 return : process or null
        public Process UseAtNowProcess(int time) {
            for (int i = 0; i < processListSorted.Count; i++) {
                if (processListSorted[i] != null) {
                    if (processListSorted[i].arrTime == time)
                    {
                        return processListSorted[i];
                    }
                }
            }
            return null;
        }


        public void sorted()
        {
            // Alltime 최댓값 찾기
            int max = -10000;
            foreach (Process process in processList)
            {
                int processArriveTime = process.getArriveTime();
                if (max < processArriveTime)
                    max = processArriveTime;
            }

            allTime = max + 1;

            // processList를 이용해서 processListSorted를 만들기
            processListSorted = new List<Process>();
            for (int i = 0; i < allTime; i++)
            {
                bool isCheckProcessIn = false;
                foreach (Process process in processList)
                {
                    if (process.getArriveTime() == i)
                    {
                        processListSorted.Add(process);

                        isCheckProcessIn = true;
                        break;
                    }
                }

                if (!isCheckProcessIn)
                    processListSorted.Add(null);
            }

            // For debugging
            // Console.WriteLine();
        }

        public Process getCurrentProcess(int index)
        {
            return processListSorted[index];
        }

        public int getCurrentProcessCount()
        {
            return processListSorted.Count;
        }


        public Process getOriginalprocessList(int index)
        {
            return processList[index];
        }

        public int getOriginalprocessListCount() {
            return processList.Count;
        }


    }
}
