using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace OpenAI
{
    public class ChatGPTDragon : MonoBehaviour
    {
        [SerializeField] private InputField inputField;
        [SerializeField] private Button button;
        [SerializeField] private Text textArea;

        private OpenAIApi openai = new OpenAIApi();

        private string userInput;
        //private string Instruction = "Act as a random stranger in a chat room and reply to the questions.\nQ: ";
        private string Instruction = "Act as a cute dragon named Vici and  " +
                                     " lives inside your telephone and " +
                                     " talk on any interesting theme " +
                                      "reply to the questions in a kind and joyfull manner.\nQ: ";

        public UnityEvent OnReplyReceived;

        private void Start()
        {
            button.onClick.AddListener(SendReply);
        }

        private async void SendReply()
        {
            userInput = inputField.text;
            Instruction += $"{userInput}\nA: ";
            
            textArea.text = "...";
            inputField.text = "";

            button.enabled = false;
            inputField.enabled = false;

            // Complete the instruction
            var completionResponse = await openai.CreateCompletion(new CreateCompletionRequest()
            {
                Prompt = Instruction,
                Model = "text-davinci-003",
                MaxTokens = 128
            });

            OnReplyReceived.Invoke();

            textArea.text = completionResponse.Choices[0].Text;
            Instruction += $"{completionResponse.Choices[0].Text}\nQ: ";

            button.enabled = true;
            inputField.enabled = true;
        }
    }
}