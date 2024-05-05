namespace Spp4;

class Program
{
    static void Main(string[] args)
    {
        Logger logger = new Logger("log.txt", 6, TimeSpan.FromSeconds(4));
        
        Console.WriteLine($"Start time: {DateTime.Now}");
        for (int i = 1; i <= 20; i++)
        {
            logger.AddEntity($"Message {i}");
            Thread.Sleep(500);
        }
        logger.Stop();
    }
}