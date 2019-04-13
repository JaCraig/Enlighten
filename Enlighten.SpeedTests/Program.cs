using BenchmarkDotNet.Running;

namespace Enlighten.SpeedTests
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            new BenchmarkSwitcher(typeof(Program).Assembly).Run(args);
        }
    }
}