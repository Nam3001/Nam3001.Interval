namespace Nam3001.Interval
{
    public class Interval
    {
        static Dictionary<Task, CancellationTokenSource> TaskCancellationTokenList = new Dictionary<Task, CancellationTokenSource>();


        public static Task Set(Action cb, int timeout)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Task task = new Task(() =>
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    Thread.Sleep(timeout);
                    cb();
                }
                Console.WriteLine("interval cleared");
            }, cts.Token);
            TaskCancellationTokenList.Add(task, cts);
            task.Start();
            return task;
        }
        public static void Clean()
        {
            foreach (KeyValuePair<Task, CancellationTokenSource> item in TaskCancellationTokenList)
            {
                var cts = item.Value;
                cts.Cancel();
            }
        }
        public static void Clear(Task t)
        {
            TaskCancellationTokenList[t].Cancel();
        }
    }
}