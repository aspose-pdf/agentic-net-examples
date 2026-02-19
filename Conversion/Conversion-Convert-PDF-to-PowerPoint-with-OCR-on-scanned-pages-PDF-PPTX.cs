using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.AI;

class PdfToPptxWithOcr
{
    // Entry point – async to allow awaiting OCR copilot calls.
    static async Task Main(string[] args)
    {
        // Input PDF path (first argument) and output PPTX path (second argument).
        string pdfPath = args.Length > 0 ? args[0] : "input.pdf";
        string pptxPath = args.Length > 1 ? args[1] : "output.pptx";

        // Verify that the source PDF exists.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document.
            Document pdfDocument = new Document(pdfPath);

            // --------------------------------------------------------------------
            // OCR processing – using Aspose.Pdf.AI OCR copilot (optional).
            // --------------------------------------------------------------------
            // NOTE: Replace "YOUR_API_KEY" with a valid OpenAI API key.
            var openAiClient = OpenAIClient
                .CreateWithApiKey("YOUR_API_KEY")   // Create OpenAI client.
                .Build();

            var ocrOptions = OpenAIOcrCopilotOptions
                .Create()
                .WithDocument(pdfPath);

            IOcrCopilot ocrCopilot = AICopilotFactory.CreateOcrCopilot(openAiClient, ocrOptions);

            // Retrieve OCR results asynchronously (results are not used further in this example).
            List<TextRecognitionResult> ocrResults = await ocrCopilot.GetTextRecognitionResultAsync(CancellationToken.None);

            // --------------------------------------------------------------------
            // PDF to PPTX conversion – using Aspose.Pdf (cross‑platform).
            // --------------------------------------------------------------------
            pdfDocument.Save(pptxPath, SaveFormat.Pptx);

            Console.WriteLine($"Conversion completed. PPTX saved to: {pptxPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}