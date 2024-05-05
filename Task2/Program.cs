namespace Task2;

public class Program
{
    static void Main(string[] args)
    {
        TaskQueue taskQueue = new TaskQueue(3);

        int tasksCount = 10;
        int currentTaskNumber = 0;

        Action[] actions = new Action[tasksCount];

        for (int i = 0; i < tasksCount; i++)
        {
            int taskNumber = i + 1;
            actions[i] = () =>
            {
                int numberToSum = taskNumber * 100;
                int sum = 0;
                for (int j = 1; j <= numberToSum; j++)
                {
                    sum += j;
                }

                currentTaskNumber++;
                Console.WriteLine($"Sum from 1 to {numberToSum} = {sum}");
            };
        }

        ParallelWait.ParallelWaitAll(actions, taskQueue);
    }
}