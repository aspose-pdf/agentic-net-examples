using System;
using System.IO;
using System.Net.Http;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string imageUrl = "https://example.com/logo.png";

        // Download the image safely – handle non‑successful status codes
        using (HttpClient httpClient = new HttpClient())
        {
            HttpResponseMessage response = httpClient.GetAsync(imageUrl).Result;
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Unable to download image. HTTP {(int)response.StatusCode} {response.ReasonPhrase}");
                // Optionally, you could provide a fallback image here.
                return;
            }

            using (Stream imageStream = response.Content.ReadAsStreamAsync().Result)
            {
                // Load the PDF document
                using (Document doc = new Document(inputPath))
                {
                    // Ensure the document has at least two pages
                    if (doc.Pages.Count < 2)
                    {
                        Console.WriteLine("The PDF does not contain a second page.");
                        return;
                    }

                    // Create an image stamp from the downloaded stream
                    ImageStamp stamp = new ImageStamp(imageStream);
                    // Position the stamp (example coordinates)
                    stamp.XIndent = 100f;
                    stamp.YIndent = 200f;

                    // Add the stamp to page 2 (pages are 1‑based)
                    doc.Pages[2].AddStamp(stamp);

                    // Save the modified PDF
                    doc.Save(outputPath);
                }
            }
        }
    }
}
