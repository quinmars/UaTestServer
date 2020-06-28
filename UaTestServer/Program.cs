using Opc.Ua;
using Opc.Ua.Configuration;
using System;
using System.Threading.Tasks;

namespace UaTestServer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Console quit logic
            var cancelKey = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

            Console.CancelKeyPress += (o, e)
                =>
            {
                cancelKey.TrySetResult(true);
                e.Cancel = true;
            };

            ApplicationInstance.MessageDlg = new MessageDialog();
            var application = new ApplicationInstance
            {
                ApplicationType = ApplicationType.Server,
                ConfigSectionName = "UaTestServer"
            };

            try
            {

                // load the application configuration.
                await application.LoadApplicationConfiguration(false);

                // check the application certificate.
                bool certOk = await application.CheckApplicationInstanceCertificate(false, 0);
                if (!certOk)
                {
                    throw new Exception("Application instance certificate invalid!");
                }

                // start the server.
                await application.Start(new ReferenceServer());

                await cancelKey.Task;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
            }

            Console.WriteLine("Quit.");
        }
    }
}
