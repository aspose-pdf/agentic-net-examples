using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.AI;

class Program
{
    static async Task Main(string[] args)
    {
        // Path to the PDF to be processed
        const string inputPdfPath = "input.pdf";

        // OpenAI API key – replace with your actual key or retrieve from a secure source
        const string openAiApiKey = "YOUR_OPENAI_API_KEY";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // --------------------------------------------------------------------
        // 1. Extract text from the last page (the concluding section)
        // --------------------------------------------------------------------
        string concludingText;
        using (Document doc = new Document(inputPdfPath))
        {
            // Pages are 1‑based; the last page holds the concluding content
            int lastPageIndex = doc.Pages.Count;

            // Use TextAbsorber to get the raw text of the page
            TextAbsorber absorber = new TextAbsorber();
            absorber.Visit(doc.Pages[lastPageIndex]);

            concludingText = absorber.Text;
        }

        // --------------------------------------------------------------------
        // 2. Prepare a temporary text file for the summary copilot options
        // --------------------------------------------------------------------
        string tempTextPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".txt");
        File.WriteAllText(tempTextPath, concludingText);

        try
        {
            // ----------------------------------------------------------------
            // 3. Create the OpenAI client
            // ----------------------------------------------------------------
            var openAiClient = OpenAIClient
                .CreateWithApiKey(openAiApiKey)
                .Build();

            // ----------------------------------------------------------------
            // 4. Configure summary copilot options – add the extracted text as a document
            // ----------------------------------------------------------------
            var summaryOptions = OpenAISummaryCopilotOptions
                .Create()
                .WithTemperature(0.5)               // optional: control creativity
                .WithDocument(tempTextPath);        // feed the concluding text

            // ----------------------------------------------------------------
            // 5. Instantiate the summary copilot
            // ----------------------------------------------------------------
            var summaryCopilot = AICopilotFactory.CreateSummaryCopilot(openAiClient, summaryOptions);

            // ----------------------------------------------------------------
            // 6. Obtain the concise summary (as plain text)
            // ----------------------------------------------------------------
            string summary = await summaryCopilot.GetSummaryAsync();

            // ----------------------------------------------------------------
            // 7. Output the summary and optionally save it as a PDF
            // ----------------------------------------------------------------
            Console.WriteLine("=== Concluding Section Summary ===");
            Console.WriteLine(summary);

            // Save the summary as a PDF document
            await summaryCopilot.SaveSummaryAsync("ConcludingSectionSummary.pdf");
        }
        finally
        {
            // Clean up the temporary file
            if (File.Exists(tempTextPath))
            {
                File.Delete(tempTextPath);
            }
        }
    }
}