using System.Security.AccessControl;
using System.Security.Principal;
public static class MutexLockManager
{
    private static readonly Dictionary<Guid, Mutex> LocksList = new Dictionary<Guid, Mutex>();

    static MutexLockManager()
    {
        if (OperatingSystem.IsWindows())
        {
            MutexSecurity mutexSecurity = new MutexSecurity();
            mutexSecurity.AddAccessRule(new MutexAccessRule(
                new SecurityIdentifier(WellKnownSidType.WorldSid, null),
                MutexRights.Synchronize | MutexRights.Modify,
                AccessControlType.Allow));
        }
    }
    public static bool Wait(Guid resourceId)
    {
        try
        {
            if (!LocksList.ContainsKey(resourceId))
            {
                var mutex = new Mutex(false, $"MyMutex_{resourceId}");
                LocksList[resourceId] = mutex;
                mutex.WaitOne();
            }
            else
            {
                LocksList[resourceId].WaitOne();
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in Wait: {ex.Message}");
            return false;
        }
    }
    public static bool Release(Guid resourceId)
    {
        try
        {
            if (LocksList.TryGetValue(resourceId, out Mutex mutex))
            {
                mutex.ReleaseMutex();
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in Release: {ex.Message}");
            return false;
        }
    }
}