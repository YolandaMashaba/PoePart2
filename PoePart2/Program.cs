using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoePart2
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            new VoiceGreeting() { };
            new AsciiArt() { };
            new ResponseSystem() { };
            new CyberChat() { };

            string audioFilePath = "C:\\Users\\ntsal\\source\\repos\\PoePart2\\resources\\greeting.wav";
            string responseFilePath = "C:\\Users\\ntsal\\source\\repos\\PoePart2\\resources\\responses.txt";

            CyberChat chat = new CyberChat(responseFilePath, audioFilePath);

            chat.Run();
        }
    }
}

