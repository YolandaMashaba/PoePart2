using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoePart2
{
    internal class Program
    {
        // Define a delegate for response handling
        public delegate string ResponseHandler(string input, string user);

        static void Main(string[] args)
        {
            new VoiceGreeting() { };
            new AsciiArt() { };
            new ResponseSystem() { };
            new CyberChat() { };

            string audioFilePath = "C:\\Users\\ntsal\\source\\repos\\PoePart2\\resources\\greeting.wav";
            string responseFilePath = "C:\\Users\\ntsal\\source\\repos\\PoePart2\\resources\\responses.txt";

            CyberChat chat = new CyberChat(responseFilePath, audioFilePath);

            // Example of using the delegate
            ResponseHandler handler = (input, user) =>
            {
                if (input.Contains("hello") || input.Contains("hi"))
                    return $"Hello {user}! How can I help with cybersecurity today?";
                return null; // Let the normal response system handle it
            };

            chat.Run();
        }
    }
}

