using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Simulate form data as URL‑encoded content
        var formData = new StringContent(
            "field1=value1&field2=value2",
            Encoding.UTF8,
            "application/x-www-form-urlencoded");

        // Create an HttpClient instance (no ASP.NET TestServer needed)
        using var client = new HttpClient();

        try
        {
            // Adjust the URL to point to the actual service endpoint you want to test
            HttpResponseMessage response = await client.PostAsync("http://localhost:5000/submit", formData);

            // Validate that the response status is 200 OK
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("POST succeeded with 200 OK.");
            }
            else
            {
                Console.WriteLine($"POST failed. Status: {(int)response.StatusCode} {response.ReasonPhrase}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during POST request: {ex.Message}");
        }
    }
}
