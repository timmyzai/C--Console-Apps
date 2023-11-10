namespace MutexConcurrency
{
    public class MutextConcurrencyTest
    {
        private decimal walletBalance1 = 1000;
        private decimal walletBalance2 = 1000;

        public void Main(decimal amountToDeduct)
        {
            var walletGuid1 = Guid.NewGuid();
            var walletGuid2 = Guid.NewGuid();

            var task1 = Task.Run(() =>
            {
                Parallel.For(0, 5, i =>
                {
                    Console.WriteLine($"Mutex_test => Thread {i} starts @ {DateTime.Now}");
                    DeductFromBalance(amountToDeduct, walletGuid1, ref walletBalance1);
                });
            });

            var task2 = Task.Run(() =>
            {
                Parallel.For(0, 5, i =>
                {
                    Console.WriteLine($"Mutex_test => Thread - 2nd {i} starts @ {DateTime.Now}");
                    DeductFromBalance(amountToDeduct, walletGuid2, ref walletBalance2);
                });
            });

            Task.WaitAll(task1, task2);

            Console.WriteLine("Mutex_test => Concurrency test completed.");
        }

        private void DeductFromBalance(decimal amountToDeduct, Guid walletGuid, ref decimal walletBalance)
        {
            MutexLockManager.Wait(walletGuid);
            try
            {
                if (walletBalance >= amountToDeduct)
                {
                    walletBalance -= amountToDeduct;
                    Thread.Sleep(2000);
                    Console.WriteLine($"Mutex_test => Wallet {walletGuid}'s balance deducted: {amountToDeduct}. New balance: {walletBalance} @ {DateTime.Now}");
                }
                else
                {
                    Console.WriteLine("Mutex_test => Insufficient balance. No deduction made.");
                }
            }
            finally
            {
                MutexLockManager.Release(walletGuid);
            }
        }
    }

}
