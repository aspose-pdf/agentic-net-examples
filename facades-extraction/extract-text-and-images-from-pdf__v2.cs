using System;
using System.IO;
using System.Text;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "ExtractionResult";
        // These cannot be const because they are computed at runtime
        string textOutput = Path.Combine(outputFolder, "text.txt");
        string summaryFile = Path.Combine(outputFolder, "summary.txt");

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Load the PDF document
                extractor.BindPdf(inputPdf);

                // ----------- Extract Text -----------
                extractor.ExtractText(); // populate internal text buffer
                string extractedText;
                using (MemoryStream textStream = new MemoryStream())
                {
                    // GetText writes the extracted text into the provided stream
                    extractor.GetText(textStream);
                    textStream.Position = 0;
                    using (StreamReader reader = new StreamReader(textStream, Encoding.UTF8))
                    {
                        extractedText = reader.ReadToEnd();
                    }
                }
                File.WriteAllText(textOutput, extractedText);

                // ----------- Extract Images -----------
                // The ExtractImageMode property is not available in the referenced Aspose.Pdf version.
                // Image extraction is performed by calling ExtractImage() which extracts all images by default.
                extractor.ExtractImage();
                int imageCount = 0;
                while (extractor.HasNextImage())
                {
                    imageCount++;
                    string imagePath = Path.Combine(outputFolder, $"image_{imageCount}.png");
                    // Save the next image to the specified path using PNG format
                    extractor.GetNextImage(imagePath, ImageFormat.Png);
                }

                // ----------- Write Summary -----------
                using (StreamWriter sw = new StreamWriter(summaryFile))
                {
                    sw.WriteLine($"Extracted from: {inputPdf}");
                    sw.WriteLine($"Text length: {extractedText.Length}");
                    sw.WriteLine($"Images extracted: {imageCount}");
                    sw.WriteLine($"Text file: {textOutput}");
                    sw.WriteLine("Image files:");
                    for (int i = 1; i <= imageCount; i++)
                    {
                        sw.WriteLine($"  image_{i}.png");
                    }
                }
            }

            Console.WriteLine($"Extraction completed. Results are in '{outputFolder}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
