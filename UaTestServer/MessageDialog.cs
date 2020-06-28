using Opc.Ua.Configuration;
using System;
using System.Threading.Tasks;

namespace UaTestServer
{
    internal class MessageDialog : IApplicationMessageDlg
    {
        private string _text;
        private bool _ask;
        public override void Message(string text, bool ask = false)
        {
            _text = text;
            _ask = ask;
        }

        public override Task<bool> ShowAsync()
        {
            Console.WriteLine("{0}", _text);
            return Task.FromResult(!_ask || Prompt());
        }

        private static bool Prompt()
        {
            ConsoleKey response;
            do
            {
                Console.Write($"[y/n]");
                response = Console.ReadKey(false).Key;
                if (response != ConsoleKey.Enter)
                {
                    Console.WriteLine();
                }
            } while (response != ConsoleKey.Y && response != ConsoleKey.N);

            return (response == ConsoleKey.Y);
        }
    }
}