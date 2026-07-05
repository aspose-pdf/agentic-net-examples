using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;
using Aspose.Pdf.AI;

class Program
{
    // Replace with your actual OpenAI API key.
    private const string OpenAiApiKey = "YOUR_OPENAI_API_KEY";

    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string imagesOutputDir = "ExtractedImages";
        const string ocrResultsPath = "OcrResults.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        Directory.CreateDirectory(imagesOutputDir);

        // -----------------------------------------------------------------
        // 1. Extract images from the PDF using PdfExtractor (Facades API)
        // -----------------------------------------------------------------
        List<string> extractedImageFiles = new List<string>();
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);
            // Use a higher resolution if needed (default 150 DPI)
            extractor.Resolution = 300;
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string imageFile = Path.Combine(imagesOutputDir, $"image-{imageIndex}.png");
                extractor.GetNextImage(imageFile); // saves image to file
                extractedImageFiles.Add(imageFile);
                Console.WriteLine($"Extracted image: {imageFile}");
                imageIndex++;
            }

            // extractor.Close(); // optional, Dispose will be called by using
        }

        if (extractedImageFiles.Count == 0)
        {
            Console.WriteLine("No images were found in the PDF.");
            return;
        }

        // -----------------------------------------------------------------
        // 2. Perform OCR on each extracted image using OpenAIOcrCopilot
        // -----------------------------------------------------------------
        // Initialize OpenAI client (synchronous for simplicity)
        var openAiClient = OpenAIClient.CreateWithApiKey(OpenAiApiKey).Build();

        using (StreamWriter resultWriter = new StreamWriter(ocrResultsPath, false))
        {
            foreach (string imagePath in extractedImageFiles)
            {
                // Configure OCR options for the current image
                var ocrOptions = OpenAIOcrCopilotOptions
                                    .Create()
                                    .WithDocument(imagePath);

                // Create OCR copilot instance
                IOcrCopilot ocrCopilot = AICopilotFactory.CreateOcrCopilot(openAiClient, ocrOptions);

                // Execute OCR (synchronously using .Result for brevity)
                List<TextRecognitionResult> ocrResults = ocrCopilot
                                                            .GetTextRecognitionResultAsync()
                                                            .Result;

                // Extract recognized text (handle possible empty results)
                string recognizedText = string.Empty;
                if (ocrResults != null && ocrResults.Count > 0 &&
                    ocrResults[0].OcrDetails != null && ocrResults[0].OcrDetails.Count > 0)
                {
                    recognizedText = ocrResults[0].OcrDetails[0].ExtractedText;
                }

                // Write OCR output
                resultWriter.WriteLine($"--- OCR for {Path.GetFileName(imagePath)} ---");
                resultWriter.WriteLine(recognizedText);
                resultWriter.WriteLine();

                Console.WriteLine($"OCR completed for {imagePath}");
            }
        }

        Console.WriteLine($"All OCR results saved to: {ocrResultsPath}");
    }
}
