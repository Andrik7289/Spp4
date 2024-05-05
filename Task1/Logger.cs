namespace Spp4;

public class Logger
{
    private string filePath;
    private int batchSize;
    private TimeSpan interval;
    private List<string> buffer;
    private object lockObject = new object();
    private bool running;

    public Logger(string filePath, int batchSize, TimeSpan interval)
    {
        this.filePath = filePath;
        this.batchSize = batchSize;
        this.interval = interval;
        buffer = new List<string>();
        running = true;

        Thread thread = new Thread(FlushThread);
        thread.IsBackground = true;
        thread.Start();
    }

    public void AddEntity(string message)
    {
        lock (lockObject)
        {
            buffer.Add(message);
            if (buffer.Count >= batchSize) FlushBuffer(false);
        }
    }

    private void FlushBuffer(bool isTimeToWrite)
    {
        lock (lockObject)
        {
            if (buffer.Count == 0) return;

            List<string> messagesToWrite = new List<string>(buffer);
            buffer.Clear();

            using (StreamWriter writer = File.AppendText(filePath))
            {
                string prefix = "";
                if (isTimeToWrite) prefix = "Time to write, ";

                foreach (string message in messagesToWrite)
                {
                    writer.WriteLine($"{DateTime.Now}: {prefix}{message}");
                }
            }
        }
    }

    private void FlushThread()
    {
        while (running)
        {
            Thread.Sleep(interval);
            FlushBuffer(true);
        }
    }

    public void Stop()
    {
        running = false;
    }
}