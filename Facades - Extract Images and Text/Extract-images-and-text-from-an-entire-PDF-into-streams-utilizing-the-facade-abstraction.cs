using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Collections to hold extracted streams
        List<MemoryStream> textStreams = new List<MemoryStream>();
        List<MemoryStream> imageStreams = new List<MemoryStream>();

        // PdfExtractor is a facade that implements IDisposable
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file (can also bind from a Stream)
            extractor.BindPdf(pdfPath);

            // Process the whole document
            extractor.StartPage = 1;   // first page
            extractor.EndPage = 0;     // 0 means till the last page

            // ----------- Extract Text -----------
            extractor.ExtractText();

            // Retrieve text page by page into MemoryStreams
            while (extractor.HasNextPageText())
            {
                MemoryStream txtStream = new MemoryStream();
                extractor.GetNextPageText(txtStream); // writes page text to the stream
                txtStream.Position = 0;               // reset for reading later
                textStreams.Add(txtStream);
            }

            // ----------- Extract Images -----------
            extractor.ExtractImage();

            // Retrieve each image into a MemoryStream (default format is JPEG)
            while (extractor.HasNextImage())
            {
                MemoryStream imgStream = new MemoryStream();
                extractor.GetNextImage(imgStream); // writes image data to the stream
                imgStream.Position = 0;
                imageStreams.Add(imgStream);
            }

            // Optional explicit close (Dispose will be called by using)
            extractor.Close();
        }

        // Example output: counts of extracted items
        Console.WriteLine($"Extracted {textStreams.Count} text pages.");
        Console.WriteLine($"Extracted {imageStreams.Count} images.");

        // Optional: save first extracted page and image to files for verification
        if (textStreams.Count > 0)
        {
            using (FileStream fs = new FileStream("page1.txt", FileMode.Create, FileAccess.Write))
            {
                textStreams[0].CopyTo(fs);
            }
        }

        if (imageStreams.Count > 0)
        {
            using (FileStream fs = new FileStream("image1.jpg", FileMode.Create, FileAccess.Write))
            {
                imageStreams[0].CopyTo(fs);
            }
        }

        // Clean up all streams after use
        foreach (var ms in textStreams) ms.Dispose();
        foreach (var ms in imageStreams) ms.Dispose();
    }
}