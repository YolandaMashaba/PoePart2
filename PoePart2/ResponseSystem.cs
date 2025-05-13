using System.Collections.Generic;
using System.IO;

namespace PoePart2
{
    public class ResponseSystem
    {
        private Dictionary<string, string> responses;//Declare dictionary to store predefined responses

        public ResponseSystem()
        {
        }

        //Initialize file path for the chatbot responses
        public ResponseSystem(string filePath)
        {
            responses = new Dictionary<string, string>();
            LoadResponses(filePath);//Load responses from the chatbot response file
        }

        //Method to load the responses from the file
        public void LoadResponses(string filePath)
        {
            //Checks if file path exists
            if (!File.Exists(filePath))
            {
                LoadDefaultResponses();//Loads default responses if file doesn't exist
                return;
            }

            //Read the file and load responses
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                //split each line into a key-value pair
                string[] parts = line.Split(new[] { ':' }, 2);
                if (parts.Length == 2)
                {
                    //Add the key-value pair to the directory
                    responses[parts[0].Trim()] = parts[1].Trim();
                }
            }
        }

        //Method to load default responses if file does not exist
        private void LoadDefaultResponses()
        {
            //Default responses for when the file cannot be found
            responses["how are you"] = "I'm just a bot, but I'm here to assit you, {0}! How can I assist you today?";
            responses["what's your purpose"] = "My purpose is to help you stay safe online, {0}. I can provide tips on password safety, phishing, and safe browsing, {0}.";
            responses["what can i ask you"] = "You can ask me about cybersecurity topics like password safety, phishing, and safe browsing, {0}";
            responses["password safety"] = "Creating a strong password is crucial, {0}. Use a mix of letters, numbers, and special characters. Avoid using common words" +
                "or personal information. Avoid using a single password for every site ";
            responses["phishing"] = "Phishing is a type of online scam where attackers try to trick you into giving them your personal information. Always verify the sender's email" +
                "address and avoid clicking on suspicious links, {0}.";
            responses["safe browsing"] = "To browse safely, {0}, make sure to use HTTPS websites, avoid downloading files from untrustes sources, an keep your browser and " +
                "antivirus software up to date.";
            responses["default"] = "I didn't quite understand that, {0}. Could you please rephrase or ask about cybersecurity related topics like passwords, phishing and safe browsing?";
        }

        //Method to get a response based on the user's input
        public string GetResponse(string userInput, string userName)
        {
            //Declaring a variable to get user input
            string input = userInput.ToLower();//Convert input to lower case for case-sensitive matching

            //Check if the input matches any of the default questions 
            foreach (var key in responses.Keys)
            {
                if (input.Contains(key))
                {
                    return string.Format(responses[key], userName);
                }
            }

            //Defaut responses if the user's input isn't related to cybersecutity 
            return string.Format(responses["default"], userName);
        }
    }
}
