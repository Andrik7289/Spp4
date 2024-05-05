namespace Task2;

public class ParallelWait
{
    public static void ParallelWaitAll(Action[] actions, TaskQueue taskQueue)
    {
        int completedTasks = 0;
        object lockObject = new object();

        foreach (Action action in actions)
        {
            Action wrappedAction = () =>
            {
                try
                {
                    action();
                }
                finally
                {
                    lock (lockObject)
                    {
                        completedTasks++;
                        if (completedTasks == actions.Length)
                        {
                            taskQueue.Stop();
                        }
                    }
                }
            };

            taskQueue.AddTask(wrappedAction);
        }
    }
}