using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.AI;
using System.Drawing.Imaging;

class Program
{
    static async Task Main(string[] args)
    {
        const string inputPdf = "input.pdf";
        const string apiKey = "YOUR_API_KEY";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPdf);
            return;
        }

        // Step 1: Extract all images from the PDF.
        List<string> imagePaths = new List<string>();
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.ExtractImage();
            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string imageFile = "image" + imageIndex.ToString() + ".png";
                extractor.GetNextImage(imageFile, ImageFormat.Png);
                imagePaths.Add(imageFile);
                imageIndex++;
            }
        }

        if (imagePaths.Count == 0)
        {
            Console.WriteLine("No images found in the PDF.");
            return;
        }

        // Step 2: Create the OpenAI client for OCR.
        OpenAIClient openAiClient = OpenAIClient.CreateWithApiKey(apiKey).Build();

        // Step 3: Build OCR copilot options, adding each extracted image.
        OpenAIOcrCopilotOptions ocrOptions = OpenAIOcrCopilotOptions.Create();
        foreach (string imgPath in imagePaths)
        {
            ocrOptions = ocrOptions.WithDocument(imgPath);
        }

        // Step 4: Create the OCR copilot and run OCR.
        IOcrCopilot ocrCopilot = AICopilotFactory.CreateOcrCopilot(openAiClient, ocrOptions);
        List<TextRecognitionResult> ocrResults = await ocrCopilot.GetTextRecognitionResultAsync();

        // Step 5: Output the recognized text for each image.
        int resultIndex = 0;
        foreach (TextRecognitionResult result in ocrResults)
        {
            Console.WriteLine("Result for image " + (resultIndex + 1).ToString() + ":");
            foreach (OcrDetail detail in result.OcrDetails)
            {
                Console.WriteLine(detail.ExtractedText);
            }
            resultIndex++;
        }
    }
}