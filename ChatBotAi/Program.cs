using System;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Chatbot AI");
        Console.WriteLine("Write Question Please (Write 'exit' to exit):");

        string apiKey = "API KEY";
        string endpoint = "https://api.openai.com/v1/chat/completions";

        while (true)
        {
            Console.Write("You: ");
            string userInput = Console.ReadLine();

            if (userInput?.ToLower() == "Exit")
                break;

            var client = new RestClient(endpoint);
            var request = new RestRequest
            {
                Method = Method.Post
            };
            request.AddHeader("Authorization", $"Bearer API KEY");
            request.AddHeader("Content-Type", "application/json");

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "system", content = "You are an intelligent assistant." },
                    new { role = "user", content = userInput }
                },
                max_tokens = 150
            };

            request.AddJsonBody(requestBody);
            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                var jsonResponse = JObject.Parse(response.Content);
                var reply = jsonResponse["choices"]?[0]?["message"]?["content"]?.ToString();
                Console.WriteLine($"Chatbot: {reply}");
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.Content}");
            }
        }
    }
}
