using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class PdfStreamProcessor
{
    // Processes a PDF provided as a stream, extracting all text and images into memory.
    public static void ProcessPdfStream(Stream pdfStream, out string allText, out List<byte[]> images)
    {
        allText = string.Empty;
        images = new List<byte[]>();

        // Ensure the incoming stream is positioned at the beginning.
        if (pdfStream.CanSeek)
            pdfStream.Position = 0;

        // Use PdfExtractor to work directly on the stream.
        using (Aspose.Pdf.Facades.PdfExtractor extractor = new Aspose.Pdf.Facades.PdfExtractor())
        {
            // Bind the PDF document from the stream.
            extractor.BindPdf(pdfStream);

            // ----------- Text Extraction -----------
            // Extract text using Unicode encoding.
            extractor.ExtractText(System.Text.Encoding.Unicode);
            var textBuilder = new StringBuilder();

            // Retrieve text page by page.
            while (extractor.HasNextPageText())
            {
                using (MemoryStream pageTextStream = new MemoryStream())
                {
                    extractor.GetNextPageText(pageTextStream);
                    string pageText = System.Text.Encoding.Unicode.GetString(pageTextStream.ToArray());
                    textBuilder.AppendLine(pageText);
                }
            }
            allText = textBuilder.ToString();

            // ----------- Image Extraction -----------
            // Extract all images defined in the PDF resources.
            extractor.ExtractImage();

            // Retrieve each image into a memory stream.
            while (extractor.HasNextImage())
            {
                using (MemoryStream imageStream = new MemoryStream())
                {
                    // Get the image in the default format (JPEG).
                    if (extractor.GetNextImage(imageStream))
                    {
                        images.Add(imageStream.ToArray());
                    }
                }
            }

            // Optional explicit close; the using statement will dispose anyway.
            extractor.Close();
        }
    }
}

// Example usage: reading a PDF from a file (replace with a network stream in real scenarios).
class Program
{
    static void Main()
    {
        const string samplePdfPath = "sample.pdf";

        if (!File.Exists(samplePdfPath))
        {
            Console.Error.WriteLine($"File not found: {samplePdfPath}");
            return;
        }

        // Simulate receiving a PDF over a network by opening a FileStream.
        using (FileStream pdfFileStream = new FileStream(samplePdfPath, FileMode.Open, FileAccess.Read))
        {
            PdfStreamProcessor.ProcessPdfStream(pdfFileStream, out string extractedText, out List<byte[]> extractedImages);

            Console.WriteLine("=== Extracted Text ===");
            Console.WriteLine(extractedText);

            Console.WriteLine($"=== Extracted Images: {extractedImages.Count} ===");
            // Optionally save the first extracted image to verify the extraction.
            if (extractedImages.Count > 0)
            {
                File.WriteAllBytes("image1.jpg", extractedImages[0]);
                Console.WriteLine("First image saved as image1.jpg");
            }
        }
    }
}