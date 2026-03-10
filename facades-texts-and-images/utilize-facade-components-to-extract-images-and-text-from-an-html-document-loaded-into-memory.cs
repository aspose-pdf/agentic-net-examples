using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class HtmlToPdfExtractor
{
    static void Main()
    {
        // Sample HTML content loaded into memory.
        string htmlContent = @"<html><body><h1>Sample Document</h1><p>This is a paragraph with an <img src='https://example.com/image.png' alt='Sample Image'/>.</p></body></html>";

        // Convert the HTML string to a PDF document.
        Document pdfDoc;
        try
        {
            // HTML to PDF conversion requires GDI+ (Windows only). Wrap in try‑catch.
            using (MemoryStream htmlStream = new MemoryStream(Encoding.UTF8.GetBytes(htmlContent)))
            {
                // HtmlLoadOptions resides in Aspose.Pdf namespace.
                pdfDoc = new Document(htmlStream, new HtmlLoadOptions());
            }
        }
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML to PDF conversion requires Windows (GDI+). Operation aborted.");
            return;
        }

        // Ensure the PDF document is disposed properly.
        using (pdfDoc)
        {
            // Initialize the PdfExtractor facade with the PDF document.
            using (PdfExtractor extractor = new PdfExtractor(pdfDoc))
            {
                // OPTIONAL: set page range (default is all pages).
                extractor.StartPage = 1;
                extractor.EndPage = pdfDoc.Pages.Count;

                // -------------------------
                // Extract text from the PDF.
                // -------------------------
                extractor.ExtractText();

                // Save extracted text to a file.
                string textOutputPath = "extracted_text.txt";
                extractor.GetText(textOutputPath);
                Console.WriteLine($"Text extracted to '{textOutputPath}'.");

                // -------------------------
                // Extract images from the PDF.
                // -------------------------
                extractor.ExtractImage();

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    // GetNextImage(string) saves the next image using the default format (JPEG).
                    string imagePath = $"extracted_image_{imageIndex}.jpg";
                    extractor.GetNextImage(imagePath);
                    Console.WriteLine($"Image {imageIndex} saved to '{imagePath}'.");
                    imageIndex++;
                }

                if (imageIndex == 1)
                {
                    Console.WriteLine("No images were found in the HTML content.");
                }
            }
        }
    }
}