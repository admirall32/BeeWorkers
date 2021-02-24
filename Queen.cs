using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeWorkers
{
    class Bee
    {
        public const double HoneyUnitsConsumedPerMg = .25;

        public double WeightMg { get; private set; }
        public Bee(double Weight)
        {
            WeightMg = Weight;
        }

        virtual public double HoneyConsumptionRate() 
        { return WeightMg * HoneyUnitsConsumedPerMg; }
    }

    class Queen : Bee
    {
        private Worker[] workers;
        private int shiftNumber;

        public Queen(Worker[] newWorkers, double Weight):base(Weight)
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
            double honeyCount = 0;
            shiftNumber++;
            string resultString = "------------------------------------ \r\n" +
                "Report for shift #" + shiftNumber + "\r\n";

            for (int i = 0; i < workers.Length; i++)
            {
                if (workers[i].DidYouFinished())
                {
                    resultString += "Worker #" + (i + 1) +". I finished the job and eat " + workers[i].HoneyConsumptionRate() + " \r\n";
                    honeyCount += workers[i].HoneyConsumptionRate();
                }
                else
                {
                    resultString += "Worker #" + (i + 1) + ". I`m doing" +
                        workers[i].CurrentJob + " for " + 
                        workers[i].shiftsLeft + " more shifts and eat " + workers[i].HoneyConsumptionRate() + " \r\n";
                    honeyCount += workers[i].HoneyConsumptionRate();
                }                
            }

            return resultString + "Total honey consumed for the shift: " + honeyCount + " units /r/n" +
                "------------------------------------ \r\n";
        }
    }

    class Worker : Bee
    {
        public Worker(string[] jobsICanDo, double Weight) : base(Weight) 
        { this.jobsICanDo = jobsICanDo; }

        private double honeyUnitsPerShiftWorked = .65;
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
                    break;
                }
            }

            if (String.IsNullOrEmpty(CurrentJob) && canDoThisJob)
            {
                currentJob = newJob;
                this.shiftsToWork = shiftsToWork;
                shiftsWorked = 0;
                return true;
            }
            else return false;
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

        public override double HoneyConsumptionRate()
        {   
            if(shiftsLeft > 0)
                return base.HoneyConsumptionRate() + honeyUnitsPerShiftWorked;
            else
                return base.HoneyConsumptionRate();

        }
    }
}
