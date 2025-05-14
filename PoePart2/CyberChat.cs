using System.Threading;
using System;

namespace PoePart2
{
    public class CyberChat
    {
            private ResponseSystem responseSystem;
            private AsciiArt asciiArt;
            private VoiceGreeting voiceGreeting;
            private string currentTopic;
            private string userName;

            public CyberChat() { }

            public CyberChat(string responseFilePath, string audioFilePath)
            {
                responseSystem = new ResponseSystem(responseFilePath);
                asciiArt = new AsciiArt();
                voiceGreeting = new VoiceGreeting(audioFilePath);
            }

            private void PrintWithTypingEffect(string text, int delay)
            {
                foreach (char c in text)
                {
                    Console.Write(c);
                    Thread.Sleep(delay);
                }
                Console.WriteLine();
            }

            private void PrintSectionDivider()
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(new string('=', Console.WindowWidth));
                Console.ResetColor();
            }

            public string GetUserName()
            {
                Console.ForegroundColor = ConsoleColor.Green;
                PrintWithTypingEffect("Cyber Chat: I am Cyber Chat. What should I call you? ", 30);
                Console.ResetColor();
                userName = Console.ReadLine();
                return userName;
            }

            public void Run()
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                asciiArt.DisplayLogo();
                Console.ResetColor();

                voiceGreeting.PlayGreeting();

                Console.ForegroundColor = ConsoleColor.Yellow;
                PrintWithTypingEffect("Welcome to Cyber Chat. A cybersecurity awareness bot! Your online safety is our priority.", 50);
                PrintWithTypingEffect("You can ask me about: password safety, scams, privacy, or phishing.", 30);
                Console.ResetColor();
                PrintSectionDivider();

                userName = GetUserName();
                Console.ForegroundColor = ConsoleColor.Cyan;
                PrintWithTypingEffect($"Cyber Chat: Hello, {userName}! How can I assist you today?", 30);
                Console.ResetColor();

                string lastResponse = "";
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"{userName}: ");
                    Console.ResetColor();

                    string userInput = Console.ReadLine();

                    if (userInput.ToLower() == "exit")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        PrintWithTypingEffect("Cyber Chat: Goodbye! Stay safe online.", 30);
                        Console.ResetColor();
                        break;
                    }

                    // Check for follow-up questions
                    if (userInput.ToLower().Contains("more") ||
                        userInput.ToLower().Contains("explain") ||
                        userInput.ToLower().Contains("details"))
                    {
                        if (!string.IsNullOrEmpty(currentTopic))
                        {
                            userInput = currentTopic; // Continue the current topic
                        }
                    }

                    string response = responseSystem.GetResponse(userInput, userName);

                    // Update current topic if we're discussing a cybersecurity topic
                    foreach (var topic in new[] { "password", "scam", "privacy", "phishing" })
                    {
                        if (response.ToLower().Contains(topic))
                        {
                            currentTopic = topic;
                            break;
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    PrintWithTypingEffect($"Cyber Chat: {response}", 30);
                    Console.ResetColor();

                    lastResponse = response;
                }
            }
        }
    }

        

