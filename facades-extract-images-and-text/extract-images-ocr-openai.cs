using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;          // PdfExtractor
using Aspose.Pdf.AI;               // OpenAI OCR copilot classes

class Program
{
    // Async Main entry point (C# 7.1+)
    static async Task Main(string[] args)
    {
        // Input PDF path
        const string pdfPath = "input.pdf";

        // Ensure a PDF exists – create a minimal one if it does not.
        if (!File.Exists(pdfPath))
        {
            // Create a simple PDF with a single blank page.
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save(pdfPath);
            }
            Console.WriteLine($"Sample PDF created at '{pdfPath}' because the file was missing.");
        }

        // Directory to store extracted images
        const string imagesDir = "extracted_images";
        Directory.CreateDirectory(imagesDir);

        // Collect paths of extracted images
        List<string> extractedImagePaths = new List<string>();

        // ---------- Extract images from PDF ----------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(pdfPath);

            // Extract all images defined in resources (default mode)
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Save each image as PNG (extension can be any supported format)
                string imagePath = Path.Combine(imagesDir, $"image_{imageIndex}.png");
                extractor.GetNextImage(imagePath);
                extractedImagePaths.Add(imagePath);
                imageIndex++;
            }
        }

        // ---------- Perform OCR on each extracted image ----------
        // Replace with your actual OpenAI API key
        const string openAiApiKey = "YOUR_API_KEY";

        // Create the OpenAI client (no disposal required)
        var openAiClient = OpenAIClient
            .CreateWithApiKey(openAiApiKey)
            .Build();

        foreach (string imagePath in extractedImagePaths)
        {
            // Configure OCR options for the current image
            var ocrOptions = OpenAIOcrCopilotOptions
                .Create()
                .WithDocument(imagePath); // Add the image file to the OCR request

            // Create the OCR copilot instance
            IOcrCopilot ocrCopilot = AICopilotFactory.CreateOcrCopilot(openAiClient, ocrOptions);

            // Execute OCR asynchronously – the method returns IReadOnlyList<TextRecognitionResult>
            var ocrResults = await ocrCopilot.GetTextRecognitionResultAsync();

            // Output recognized text (if any)
            if (ocrResults.Count > 0 && ocrResults[0].OcrDetails.Count > 0)
            {
                string extractedText = ocrResults[0].OcrDetails[0].ExtractedText;
                Console.WriteLine($"OCR result for '{Path.GetFileName(imagePath)}':");
                Console.WriteLine(extractedText);
                Console.WriteLine(new string('-', 40));
            }
            else
            {
                Console.WriteLine($"No OCR text found for '{Path.GetFileName(imagePath)}'.");
            }
        }
    }
}
