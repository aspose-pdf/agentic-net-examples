using System;
using System.IO;
using System.Drawing.Imaging; // ImageFormat for PdfExtractor
using Aspose.Pdf.Facades;      // PdfExtractor (Facades API)
using Aspose.Pdf.AI;           // OpenAI OCR copilot

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";               // source PDF
        const string outputFolder = "OcrResults";         // folder for OCR text files
        const string openAiApiKey = "YOUR_OPENAI_API_KEY"; // replace with your key

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Prepare the OpenAI OCR copilot once – it can be reused for every image
        var openAiClient = Aspose.Pdf.AI.OpenAIClient.CreateWithApiKey(openAiApiKey).Build();

        // Initialize PDF extractor (Facades)
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);
            extractor.ExtractImage(); // extract all images defined in resources

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Retrieve image into a memory stream (PNG format)
                using (MemoryStream imgStream = new MemoryStream())
                {
                    extractor.GetNextImage(imgStream, ImageFormat.Png);
                    imgStream.Position = 0; // reset for reading

                    // Save image temporarily – required for OCR copilot
                    string tempImagePath = Path.Combine(outputFolder, $"page_image_{imageIndex}.png");
                    using (FileStream file = new FileStream(tempImagePath, FileMode.Create, FileAccess.Write))
                    {
                        imgStream.CopyTo(file);
                    }

                    // Prepare OCR options for the current image
                    var ocrOptions = Aspose.Pdf.AI.OpenAIOcrCopilotOptions.Create()
                                         .WithDocument(tempImagePath);

                    // Create the OCR copilot instance
                    IOcrCopilot ocrCopilot = Aspose.Pdf.AI.AICopilotFactory.CreateOcrCopilot(openAiClient, ocrOptions);

                    // Perform OCR (synchronously for simplicity)
                    var results = ocrCopilot.GetTextRecognitionResultAsync().GetAwaiter().GetResult();

                    // Extract the recognised text (if any)
                    string ocrText = string.Empty;
                    if (results != null && results.Count > 0 && results[0].OcrDetails != null && results[0].OcrDetails.Count > 0)
                    {
                        ocrText = results[0].OcrDetails[0].ExtractedText ?? string.Empty;
                    }

                    // Write OCR text to a file
                    string textOutputPath = Path.Combine(outputFolder, $"image_{imageIndex}_ocr.txt");
                    File.WriteAllText(textOutputPath, ocrText);

                    // Clean up temporary image file
                    File.Delete(tempImagePath);
                }

                imageIndex++;
            }
        }

        Console.WriteLine("OCR extraction completed.");
    }
}
