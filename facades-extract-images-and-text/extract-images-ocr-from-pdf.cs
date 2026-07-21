using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;
using Aspose.Pdf.AI; // OCR‑related classes are directly under Aspose.Pdf.AI

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";                 // Source PDF
        const string outputTextPath = "extracted_text.txt"; // Where OCR results will be saved
        const string openAiApiKey = "YOUR_API_KEY";         // Replace with your OpenAI API key

        // Ensure the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // StringBuilder to accumulate OCR text from all images
        StringBuilder ocrTextBuilder = new StringBuilder();

        // Initialize the PDF extractor facade
        PdfExtractor extractor = new PdfExtractor();
        try
        {
            // Bind the PDF file and extract images from it
            extractor.BindPdf(pdfPath);
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Save each extracted image to a temporary file (JPEG by default)
                string tempImagePath = $"image_{imageIndex}.jpg";
                extractor.GetNextImage(tempImagePath);

                // ---- OCR processing for the extracted image ----
                // Create an OpenAI client (classes are in Aspose.Pdf.AI namespace)
                var openAiClient = OpenAIClient
                    .CreateWithApiKey(openAiApiKey)
                    .Build();

                // Configure OCR copilot options to include the image file
                var ocrOptions = OpenAIOcrCopilotOptions
                    .Create()
                    .WithDocument(tempImagePath);

                // Instantiate the OCR copilot
                IOcrCopilot ocrCopilot = AICopilotFactory.CreateOcrCopilot(openAiClient, ocrOptions);

                // Perform OCR (synchronously for simplicity)
                var ocrResults = ocrCopilot
                    .GetTextRecognitionResultAsync()
                    .GetAwaiter()
                    .GetResult();

                // Extract recognized text if available
                if (ocrResults.Count > 0 && ocrResults[0].OcrDetails.Count > 0)
                {
                    string recognized = ocrResults[0].OcrDetails[0].ExtractedText;
                    ocrTextBuilder.AppendLine(recognized);
                }

                // Clean up the temporary image file
                try { File.Delete(tempImagePath); } catch { /* ignore cleanup errors */ }

                imageIndex++;
            }
        }
        finally
        {
            // Release resources held by the extractor
            extractor.Close();
        }

        // Write all OCR'd text to the output file
        File.WriteAllText(outputTextPath, ocrTextBuilder.ToString());

        Console.WriteLine($"OCR extraction completed. Results saved to '{outputTextPath}'.");
    }
}
