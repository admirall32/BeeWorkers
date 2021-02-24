using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeWorkers
{
    class Queen
    {
        private Worker[] workers;
        private int shiftNumber;

        public Queen(Worker[] newWorkers)
        {
            workers = newWorkers;
            shiftNumber = 0;
        }
        public bool AssignWork(string newJob, int shiftsToWork)
        {
            bool findFreeBee = false;
            for (int i = 0; i < workers.Length; i++)
            {
                if (workers[i].DoThisJob(newJob, shiftsToWork))
                {
                    findFreeBee = true;
                    break;
                }                    
            }

            return findFreeBee;
        }

        public string WorkTheNextShift()
        {
            shiftNumber++;
            string resultString = "Report for shift #" + shiftNumber + "\r\n";
            for (int i = 0; i < workers.Length; i++)
            {
                if (workers[i].DidYouFinished())
                {
                    resultString += "Worker #" + (i + 1) +". I finished the job! \r\n";
                }
                else
                {
                    resultString += "Worker #" + (i + 1) + ". I`m doing" +
                        workers[i].CurrentJob + " for " + 
                        workers[i].shiftsLeft + " more shifts. \r\n";
                }
                
            }

            return resultString + "------------------------------------ \r\n";
        }
    }

    class Worker
    {
        public Worker(string[] jobsICanDo) { this.jobsICanDo = jobsICanDo; }
        private string currentJob = ""; 
        public string CurrentJob
        {
            get
            {
                if (shiftsLeft <= 0)
                {
                    currentJob = "";
                }
                return currentJob;
            }
        }

        public int shiftsLeft { 
            get
            {
                return shiftsToWork - shiftsWorked;
            } 
        }

        private string[] jobsICanDo;
        private int shiftsToWork;
        private int shiftsWorked;

        public bool DoThisJob(string newJob, int shiftsToWork)
        {
            bool canDoThisJob = false;
            for (int i = 0; i < jobsICanDo.Length; i++)
            {
                if (newJob == jobsICanDo[i])
                {
                    canDoThisJob = true;
 //Console.WriteLine("#" + i + " I Can`t(((((((((");
                    break;
                }
            }

            if (String.IsNullOrEmpty(CurrentJob) && canDoThisJob)
            {
                currentJob = newJob;
                this.shiftsToWork = shiftsToWork;
                shiftsWorked = 0;
Console.WriteLine("I CAN!");
                return true;
            }
            else
            {
Console.WriteLine(currentJob);
                return false;

            }

        }

        public bool DidYouFinished()
        {
            if (shiftsLeft > 0)
                shiftsWorked++;

            if (shiftsLeft > 0)
                return false;
            else
                return true;
        }
    }
}
