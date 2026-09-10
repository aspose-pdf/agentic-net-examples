using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.AI;

class Program
{
    // Replace with your actual OpenAI API key
    private const string OpenAiApiKey = "YOUR_OPENAI_API_KEY";

    static async Task Main(string[] args)
    {
        const string inputPdfPath = "input.pdf";
        const string imagesFolder = "ExtractedImages";
        const string ocrResultsFolder = "OcrResults";

        // Ensure the input PDF exists – create a minimal placeholder if it does not.
        if (!File.Exists(inputPdfPath))
        {
            using var placeholder = new Document();
            placeholder.Pages.Add();
            placeholder.Save(inputPdfPath);
        }

        // Ensure output directories exist
        Directory.CreateDirectory(imagesFolder);
        Directory.CreateDirectory(ocrResultsFolder);

        // Extract images from the PDF using PdfExtractor (Facades API)
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);
            // Extract all images defined in resources (default mode)
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Save each extracted image to a file (default format is JPEG)
                string imagePath = Path.Combine(imagesFolder, $"image_{imageIndex}.jpg");
                extractor.GetNextImage(imagePath);

                // Perform OCR on the extracted image using OpenAIOcrCopilot
                string ocrText = await PerformOcrOnImageAsync(imagePath);

                // Save OCR result to a text file
                string ocrTextPath = Path.Combine(ocrResultsFolder, $"image_{imageIndex}.txt");
                File.WriteAllText(ocrTextPath, ocrText);

                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction and OCR processing completed.");
    }

    // Helper method that runs OCR on a single image file using Aspose.Pdf.AI OpenAIOcrCopilot
    private static async Task<string> PerformOcrOnImageAsync(string imagePath)
    {
        // Create OpenAI client
        var openAiClient = OpenAIClient
            .CreateWithApiKey(OpenAiApiKey)
            .Build();

        // Configure OCR copilot options and add the image file
        var ocrOptions = OpenAIOcrCopilotOptions
            .Create()
            .WithDocument(imagePath); // adds the image as a document to be processed

        // Create the OCR copilot
        IOcrCopilot ocrCopilot = AICopilotFactory.CreateOcrCopilot(openAiClient, ocrOptions);

        // Retrieve OCR results (await the async call)
        List<TextRecognitionResult> results = await ocrCopilot.GetTextRecognitionResultAsync(CancellationToken.None);

        // Return the extracted text from the first result (if any)
        if (results != null && results.Count > 0 && results[0].OcrDetails.Count > 0)
        {
            return results[0].OcrDetails[0].ExtractedText;
        }

        return string.Empty;
    }
}
