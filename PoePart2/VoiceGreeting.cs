using System.Media;

namespace PoePart2
{
    public class VoiceGreeting
    {
            //File path to the audio
            private string audioPath;

            public VoiceGreeting()
            {

            }

            public VoiceGreeting(string filePath)
            {
                audioPath = filePath;//Initialize the audio file path
            }

            //Method to play the audio
            public void PlayGreeting()
            {
                SoundPlayer player = new SoundPlayer(audioPath);
                player.Play();//Play the audio
            }
        }
    }
