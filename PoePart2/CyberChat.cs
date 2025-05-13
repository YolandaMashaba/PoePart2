using System.Threading;
using System;

namespace PoePart2
{
    public class CyberChat
    {
        private ResponseSystem responseSystem;//Handles the chatbot responses
        private AsciiArt asciiArt;//Handles the ASCII art 
        private VoiceGreeting voiceGreeting;//Handles the voice greeting

        public CyberChat()
        {
        }

        public CyberChat(string responseFilePath, string audioFilePath)
        {
            //Initialize the components 
            responseSystem = new ResponseSystem(responseFilePath);
            asciiArt = new AsciiArt();
            voiceGreeting = new VoiceGreeting(audioFilePath);
        }

        //Get user's name
        public string GetUserName()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Cyber Chat: " + "I am Cyber Chat. What should I call you? ");
            Console.ResetColor();
            return Console.ReadLine();
        }

        //Print text with a typing effect
        private void PrintWithTypingEffect(string text, int delay)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);//Simulate typing effect
            }
            Console.WriteLine();//Move to the next line after printing
        }

        //Run the chatbot 
        public void Run()
        {
            //Display the ASCII art logo
            Console.ForegroundColor = ConsoleColor.Magenta;
            asciiArt.DisplayLogo();
            Console.ResetColor();

            //Play voice greeting
            voiceGreeting.PlayGreeting();

            //Enhanced welcome message
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintWithTypingEffect("Welcome to Cyber Chat. A cybersecurity awareness bot! Your online safety is our priority. How can I assist you today?.", 50);
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintWithTypingEffect("You can ask me about cybersecurity topics like password safety, phishing, and safe browsing.", 30);
            Console.ResetColor();
            PrintSectionDivider();

            //Get user's name
            string userName = GetUserName();
            Console.ForegroundColor = ConsoleColor.Cyan;
            PrintWithTypingEffect($"Cyber Chat: " + "Hello, " + userName + "! How can I assist you today?", 30);
            Console.ResetColor();

            //while loop to control the interaction
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(userName + ": ");
                Console.ResetColor();

                string userInput = Console.ReadLine();

                if (userInput.ToLower() == "exit")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    PrintWithTypingEffect("Goodbye! Stay safe online.", 30);
                    Console.ResetColor();
                    break;
                }

                //Get and display the bot's response
                string response = responseSystem.GetResponse(userInput, userName);
                Console.ForegroundColor = ConsoleColor.Cyan;
                PrintWithTypingEffect($"Cyber Chat:" + response, 30);
                Console.ResetColor();
            }
        }

        //Print a section divider
        private void PrintSectionDivider()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(new string('=', Console.WindowWidth));
            Console.ResetColor();
        }
    }
}
