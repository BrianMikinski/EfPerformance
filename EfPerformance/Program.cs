using CoreBlog;

Console.WriteLine("Hello, World! Welcome to the EF Performance Contest");

Console.WriteLine("EF Core 6 Profiling Service");
var coreProfilingService = new EfCoreProfilingService();

Console.WriteLine("EF6 Profiling Service");
var ef6ProfilingService = new Ef6ProfilingService();
//        }
//    }
//}