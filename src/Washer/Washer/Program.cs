using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tcp.Client;
using Washer.Bussiness.concrete;
using Washer.Core.IOC;

namespace Washer
{
    static class Program
    {
        public static IServiceProvider ServiceProvider { get; set; }
        static void ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<Washer>();
            services.AddTransient(opt =>
            {
                return new Client("127.0.0.1", 9800);
            });
            services.AddTransient<CommandTypeManager>();
            services.AddTransient<InfoManager>();
            services.AddTransient<ObjectTypeManager>();
            services.AddTransient<ProsessPacketManager>();
            services.AddTransient<ReciverManager>();
            services.AddTransient<RequestManager>();
            services.AddTransient<ResonceManager>();
            services.AddSingleton<SenderManager>();
            services.AddSingleton<StreamManager>();
            services.AddTransient<TransferManager>();

            ServiceProvider = ServiceTool.Create(services);
        }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ConfigureServices();
            Application.Run(ServiceTool.resolve<Washer>());
        }
    }
}
