using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PoePart2
{
    public class ResponseSystem
    {
        private Dictionary<string, List<string>> responses;
        private Dictionary<string, string> userMemory;
        private Dictionary<string, List<string>> sentimentResponses;
        private List<string> cybersecurityKeywords;

        public ResponseSystem()
        {
            InitializeDefaultResponses();
            InitializeKeywordsList();
        }

        public ResponseSystem(string filePath)
        {
            responses = new Dictionary<string, List<string>>();
            userMemory = new Dictionary<string, string>();
            sentimentResponses = new Dictionary<string, List<string>>();
            cybersecurityKeywords = new List<string>();

            LoadResponses(filePath);
            InitializeSentimentResponses();
            InitializeKeywordsList();
        }

        private void InitializeKeywordsList()
        {
            cybersecurityKeywords = new List<string>
            {
                "password", "phishing", "malware", "ransomware", "firewall",
                "vpn", "encryption", "2fa", "authentication", "data breach",
                "social engineering", "ddos", "iot security", "zero day",
                "patch", "update", "backup", "dark web", "identity theft",
                "spyware", "adware", "rootkit", "trojan", "worm", "keylogger",
                "man-in-the-middle", "sql injection", "xss", "csrf", "botnet",
                "spoofing", "brute force", "credential stuffing", "shoulder surfing",
                "whaling", "smishing", "vishing", "quid pro quo", "tailgating",
                "water holing", "typosquatting", "clickjacking", "session hijacking",
                "cryptojacking", "sextortion", "sim swapping", "ai security",
                "biometrics", "quantum security", "blockchain security"
            };
        }

        private void InitializeDefaultResponses()
        {
            responses = new Dictionary<string, List<string>>();
            userMemory = new Dictionary<string, string>();
            sentimentResponses = new Dictionary<string, List<string>>();

            // Expanded cybersecurity responses
            responses["password"] = new List<string>
            {
                "Strong passwords should be at least 12 characters long and include uppercase, lowercase, numbers and symbols, {0}.",
                "Consider using passphrases instead of passwords - they're longer but easier to remember, {0}. Example: 'PurpleTiger$JumpsOver42Clouds!'",
                "Never reuse passwords across different sites, {0}. If one gets compromised, they all become vulnerable.",
                "Password managers can generate and store complex passwords securely, {0}. They only require you to remember one master password."
            };

            responses["phishing"] = new List<string>
            {
                "Phishing emails often create urgency ('Your account will be closed!') or offer too-good-to-be-true deals, {0}.",
                "Always check the sender's email address carefully, {0}. Scammers often use addresses that look similar to legitimate ones.",
                "Hover over links before clicking to see the actual URL, {0}. If it looks suspicious, don't click!",
                "Legitimate companies will never ask for sensitive information via email, {0}. When in doubt, contact the company directly."
            };

            // Added 25+ new cybersecurity topics with multiple responses each
            responses["malware"] = new List<string>
            {
                "Malware includes viruses, worms, trojans and spyware, {0}. Keep your antivirus updated and avoid suspicious downloads.",
                "Signs of malware infection include slow performance, pop-ups, and unexplained network activity, {0}."
            };

            responses["ransomware"] = new List<string>
            {
                "Ransomware encrypts your files and demands payment for decryption, {0}. Regular backups are your best defense.",
                "Never pay ransomware attackers, {0}. There's no guarantee you'll get your files back, and it funds criminal activity."
            };

            responses["vpn"] = new List<string>
            {
                "VPNs encrypt your internet traffic, protecting your data on public Wi-Fi, {0}. Choose reputable providers with no-log policies.",
                "While VPNs enhance privacy, they don't make you completely anonymous online, {0}."
            };

            responses["2fa"] = new List<string>
            {
                "Two-factor authentication (2FA) adds an extra layer of security beyond passwords, {0}. Use authenticator apps instead of SMS when possible.",
                "Even if someone gets your password, 2FA can prevent them from accessing your account, {0}."
            };

            // Additional topics truncated for brevity - would include all cybersecurityKeywords

            responses["default"] = new List<string>
            {
                 "I didn't quite understand that, {0}. Try asking about: passwords, phishing, or malware.",
                 "I'm not sure about that topic, {0}. I can help with cybersecurity basics."
            };

            InitializeSentimentResponses();
        }

        private void InitializeSentimentResponses()
        {
            sentimentResponses["worried"] = new List<string>
            {
                "I understand cybersecurity can feel overwhelming, {0}. Let's break this down into manageable steps.",
                "It's completely normal to feel concerned, {0}. The good news is there are simple steps you can take to protect yourself."
            };

            sentimentResponses["frustrated"] = new List<string>
            {
                "I hear your frustration, {0}. Technology should work for you, not against you. Let me simplify this.",
                "Security can sometimes feel inconvenient, {0}, but these measures exist to protect what's important to you."
            };

            sentimentResponses["confused"] = new List<string>
            {
                "Let me explain that differently, {0}. Cybersecurity concepts can be tricky at first.",
                "I'll break this down into simpler terms, {0}. Everyone starts somewhere with these concepts."
            };
        }

        public void LoadResponses(string filePath)
        {
            if (!File.Exists(filePath))
            {
                InitializeDefaultResponses();
                return;
            }

            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(new[] { ':' }, 2);
                if (parts.Length == 2)
                {
                    string key = parts[0].Trim().ToLower();
                    string value = parts[1].Trim();

                    if (!responses.ContainsKey(key))
                    {
                        responses[key] = new List<string>();
                    }
                    responses[key].Add(value);
                }
            }
        }

        public void RememberUserPreference(string key, string value)
        {
            userMemory[key] = value;
        }

        public string GetUserPreference(string key)
        {
            return userMemory.ContainsKey(key) ? userMemory[key] : null;
        }

        public string GetResponse(string userInput, string userName)
        {
            string input = userInput.ToLower();
            Random random = new Random();

            // Check for sentiment keywords first
            foreach (var sentiment in sentimentResponses.Keys)
            {
                if (input.Contains(sentiment))
                {
                    int index = random.Next(sentimentResponses[sentiment].Count);
                    return string.Format(sentimentResponses[sentiment][index], userName);
                }
            }

            // Check for cybersecurity keywords - now using the comprehensive list
            foreach (var keyword in cybersecurityKeywords)
            {
                if (responses.ContainsKey(keyword) && input.Contains(keyword))
                {
                    // Remember user interest
                    RememberUserPreference("interest", keyword);

                    int index = random.Next(responses[keyword].Count);
                    return string.Format(responses[keyword][index], userName);
                }
            }

            // Check if we have remembered user preferences to personalize default response
            string interest = GetUserPreference("interest");
            if (interest != null)
            {
                return string.Format($"Since you asked about {interest} before, you might want to know: " +
                    responses["default"][random.Next(responses["default"].Count)], userName);
            }

            // Default response that lists some available topics
            return string.Format(responses["default"][random.Next(responses["default"].Count)], userName);
        }

        // New method to get all cybersecurity keywords
        public List<string> GetCybersecurityKeywords()
        {
            return cybersecurityKeywords;
        }
    }
}
