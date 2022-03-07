using System;
using System.Collections.Generic;
using System.Text;

namespace MsOSProgram.core.Algorithm
{
    public class Process
    {
        public string processId = "";
        public int arrTime = -1;
        public int burstTime = -1;
        public int waitingTime = -1;
        public int turnAroundTime = -1;
        public double normalizedTT = -1;


        public string printProcessId = "";
        public string printArrTime = "";
        public string printBurstTime = "";
        public string printWaitingTime = "";
        public string printTurnAroundTime = "";
        public string printNormalizedTT = "";




        public Process(string processId, int arrTime, int burstTime)
        {
            this.processId = processId;
            this.arrTime = arrTime;
            this.burstTime = burstTime;
        }

        public Process(string processId, string arrTime, string burstTime, string waitingTime, string turnAroundTime, string normalizedTT)
        {
            this.printProcessId = processId;
            this.printArrTime = arrTime;
            this.printBurstTime = burstTime;
            this.printWaitingTime = waitingTime;
            this.printTurnAroundTime = turnAroundTime;
            this.printNormalizedTT = normalizedTT;
        
        }



        public bool check(string processId, int arrTime, int burstTime, int waitingTime, int turnAroundTime, double normalizedTT)
        {
            if (this.processId != processId || !this.processId.Equals(processId))
            {
                return false;
            }


            if (this.arrTime != arrTime)
            {
                return false;
            }


            //if (this.burstTime != burstTime)
            //    return false;

            if (this.waitingTime != waitingTime)
            {
                Console.WriteLine("\tWaiting Time Error\t Current : " + this.waitingTime.ToString() + " Correct : " + waitingTime.ToString());
                return false;
            }


            if (this.turnAroundTime != turnAroundTime)
            {
                Console.WriteLine("\tturnAroundTime Error\t Current : " + this.turnAroundTime.ToString() + " Correct : " + turnAroundTime.ToString());
                return false;
            }


            if (this.normalizedTT != normalizedTT)
            {
                Console.WriteLine("\tnormalizedTTr\t Current : " + this.normalizedTT.ToString() + " Correct : " + normalizedTT.ToString());
                return false;
            }

            return true;
        }

        public int getArriveTime()
        {
            return arrTime;
        }
    }
}
