using System.Threading.Tasks;

namespace gen.common.Extensions;

public static class TaskExtensions
{
    public static async Task Forget(this Task task)
    {
         await task.ConfigureAwait(false);
    }
}