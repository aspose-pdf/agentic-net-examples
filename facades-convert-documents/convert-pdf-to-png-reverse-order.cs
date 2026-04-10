using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing.Imaging;               // ImageFormat enum
using Aspose.Pdf;                           // Document
using Aspose.Pdf.Facades;                   // PdfConverter

class PdfToPngReverse
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";               // source PDF
        const string outputDir = "PngPages";                // folder for PNGs

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            int pageCount = doc.Pages.Count; // 1‑based page count

            // Initialize PdfConverter on the loaded document (Facades API)
            using (PdfConverter converter = new PdfConverter(doc))
            {
                // Convert the whole range; order of extraction will be forward
                converter.StartPage = 1;
                converter.EndPage   = pageCount;
                converter.DoConvert();

                // Collect each page image into memory streams
                List<MemoryStream> pageImages = new List<MemoryStream>();
                while (converter.HasNextImage())
                {
                    MemoryStream imgStream = new MemoryStream();
                    // Get next image as PNG (explicit format)
                    converter.GetNextImage(imgStream, ImageFormat.Png);
                    imgStream.Position = 0;               // reset for later reading
                    pageImages.Add(imgStream);
                }

                // Process pages in reverse order
                for (int i = pageImages.Count - 1; i >= 0; i--)
                {
                    int originalPageNumber = i + 1; // because list is 0‑based
                    string outPath = Path.Combine(outputDir, $"page_{originalPageNumber}_rev.png");

                    // Write the memory stream to a file
                    using (FileStream file = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                    {
                        pageImages[i].CopyTo(file);
                    }

                    // Dispose the individual memory stream
                    pageImages[i].Dispose();
                }

                // Close the converter (optional, using will also dispose)
                converter.Close();
            }
        }

        Console.WriteLine("PDF pages have been saved as PNG images in reverse order.");
    }
}