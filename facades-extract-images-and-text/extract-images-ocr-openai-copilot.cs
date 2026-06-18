using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.AI;   // OpenAI OCR copilot classes

class Program
{
    // Async Main entry point (C# 7.1+)
    static async Task Main()
    {
        const string inputPdfPath   = "input.pdf";          // Source PDF
        const string imagesFolder   = "extracted_images";  // Temp folder for images
        const string openAiApiKey   = "YOUR_OPENAI_API_KEY"; // Replace with your key

        // Validate input PDF
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the temporary folder exists
        Directory.CreateDirectory(imagesFolder);

        // -----------------------------------------------------------------
        // 1. Extract all images from the PDF using PdfExtractor (Facade API)
        // -----------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(inputPdfPath);

            // Process the whole document (1‑based page indexing)
            extractor.StartPage = 1;
            extractor.EndPage   = extractor.Document.Pages.Count;

            // Instruct the extractor to extract images
            extractor.ExtractImage();

            int imageCounter = 1;
            while (extractor.HasNextImage())
            {
                // Save each image as PNG (default format is JPEG; specifying .png forces PNG)
                string imagePath = Path.Combine(imagesFolder, $"image_{imageCounter}.png");
                extractor.GetNextImage(imagePath);
                imageCounter++;
            }
        }

        // ---------------------------------------------------------------
        // 2. Prepare the OpenAI OCR copilot with the extracted image files
        // ---------------------------------------------------------------
        // Create the OpenAI client (API key only; adjust if you need organization, etc.)
        var openAiClient = OpenAIClient
            .CreateWithApiKey(openAiApiKey)
            .Build();

        // Build OCR options and add each extracted image as a document source
        var ocrOptions = OpenAIOcrCopilotOptions.Create();
        foreach (string imgPath in Directory.GetFiles(imagesFolder, "*.png"))
        {
            // WithDocument adds a path to a PDF or an image that will be processed
            ocrOptions = ocrOptions.WithDocument(imgPath);
        }

        // Create the OCR copilot instance
        IOcrCopilot ocrCopilot = AICopilotFactory.CreateOcrCopilot(openAiClient, ocrOptions);

        // ---------------------------------------------------------------
        // 3. Run OCR asynchronously and output the recognized text
        // ---------------------------------------------------------------
        List<TextRecognitionResult> ocrResults = await ocrCopilot.GetTextRecognitionResultAsync();

        foreach (TextRecognitionResult result in ocrResults)
        {
            // Each result may contain multiple OCR details (e.g., per page or per image)
            foreach (var detail in result.OcrDetails)
            {
                Console.WriteLine("----- OCR Extracted Text -----");
                Console.WriteLine(detail.ExtractedText);
                Console.WriteLine("------------------------------");
            }
        }

        // Optional: clean up extracted images if no longer needed
        // Directory.Delete(imagesFolder, recursive: true);
    }
}