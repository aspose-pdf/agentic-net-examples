using System;
using System.IO;
using Aspose.Pdf; // Core PDF API (Image class is here)

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF
        const string outputPdf = "watermarked.pdf";   // result PDF
        const string watermarkImg = "watermark.png"; // image to use as watermark

        // Verify files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(watermarkImg))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImg}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Read the watermark image once into a byte array (so we can create a fresh stream per page)
            byte[] imgBytes = File.ReadAllBytes(watermarkImg);

            // Apply the same watermark to every page in the document
            foreach (Page page in doc.Pages)
            {
                // Create a new memory stream for each page (Aspose.Pdf.Image expects a stream positioned at the start)
                using (MemoryStream imgStream = new MemoryStream(imgBytes))
                {
                    // Create an Aspose.Pdf.Image that uses the stream
                    Image pdfImg = new Image
                    {
                        ImageStream = imgStream,
                        // Keep original size but centre it on the page
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment   = VerticalAlignment.Center,
                        // If you want a fixed size, uncomment the lines below and set values
                        // FixWidth = 200,
                        // FixHeight = 200,
                    };

                    // Add the image to the page content
                    page.Paragraphs.Add(pdfImg);
                }
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}
