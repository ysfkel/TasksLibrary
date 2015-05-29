using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TasksLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
       
          //  CreateTasksUsingNew();
           // CreateTasksUsingStartNew();
          //  ParallelForEach();
          //  ParallelFor();
            CreateTasksUsingStartNewCancellationToken();

            //for (int i = 0; i < 10; i++)
            //{
            //    Thread.Sleep(250);
            //    Console.WriteLine("do some other work {0}",i);
            //}

            Console.WriteLine("Press any key to quit");
            Console.ReadKey();
        }

        //takes list
        static void ParallelForEach()
        {

            var list = new List<int>() { 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20};

            Parallel.ForEach(list, (i) => Console.WriteLine(i));

            Console.WriteLine("Operation complete");
        }

        //Takes range
        static void ParallelFor()
        {
            Parallel.For(1, 20, (i) => Console.WriteLine(i));
        }

        static void CreateTasksUsingStartNew()
        {
           Task t1= Task.Factory.StartNew(() => DoSomeIOTask(4, 2500)).ContinueWith((prevTask)=>DoSomeOtherImportantTask(7,6000));
           Task t2= Task.Factory.StartNew(() => DoSomeIOTask(5, 3500)).ContinueWith((prevTask)=>DoSomeOtherImportantTask(8,7000));
           Task t3= Task.Factory.StartNew(() => DoSomeIOTask(6, 4500)).ContinueWith((prevTask)=>DoSomeOtherImportantTask(9,8500));

           Task.WaitAll(t1, t2, t3);

           Console.WriteLine("all tasks complete");
        }

        //cancellation token example
        static void CreateTasksUsingStartNewCancellationToken()
        {
             try
            {
                var tokenSource = new CancellationTokenSource();
                CancellationToken token = tokenSource.Token;
                Task t1 = Task.Factory.StartNew(() => DoSomeIOTask(4, 2500)).ContinueWith((prevTask) => DoSomeOtherImportantTask(7, 6000, token)).ContinueWith((prevTask) => DoSomeOtherVeryImportantTask(10, 2000, token));
                tokenSource.Cancel();
            }
            catch (Exception e)
            {

                Console.WriteLine("Type {0}, Message: {1}",e.GetType(),e.Message);
            }

        }
        //creating tasks using new key word
        static void CreateTasksUsingNew()
        {
            Task t1 = new Task(() => DoSomeIOTask(1, 2500));
            Task t2 = new Task(() => DoSomeIOTask(2, 3000));
            Task t3 = new Task(() => DoSomeIOTask(3, 1000));


            t1.Start();
            t2.Start();
            t3.Start();
            Task.WaitAll(t1, t2, t3);

            Console.WriteLine("all tasks complete");
        }


        //TASKS TO EXECUTE/////////////////////////////////////


        static void DoSomeIOTask(int id,int sleepTime)
        {
            Console.WriteLine("Task {0} is beginning",id);
            Thread.Sleep(sleepTime);
            Console.WriteLine("Task {0} has completed",id);
        }

        static void DoSomeOtherImportantTask(int id, int sleepTime)
        {
            Console.WriteLine("Task {0} is beginning more work", id);
            Thread.Sleep(sleepTime);
            Console.WriteLine("Task {0} has completed more work", id);
        }
        static void DoSomeOtherImportantTask(int id, int sleepTime,CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                Console.WriteLine("Token cancellation request detected");
                token.ThrowIfCancellationRequested();
            }
            Console.WriteLine("Task {0} is beginning more work", id);
            Thread.Sleep(sleepTime);
            Console.WriteLine("Task {0} has completed more work", id);
        }

        static void DoSomeOtherVeryImportantTask(int id, int sleepTime, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                Console.WriteLine("Token cancellation request detected");
                token.ThrowIfCancellationRequested();
            }
            Console.WriteLine("Task {0} is beginning more work", id);
            Thread.Sleep(sleepTime);
            Console.WriteLine("Task {0} has completed more work", id);
        }
    }
}
