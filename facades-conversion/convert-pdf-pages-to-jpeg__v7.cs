using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // JpegDevice lives here

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPattern = "page_{0}.jpg"; // {0} will be replaced by a two‑digit index

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Configure JPEG conversion options via the constructor (quality is passed here)
            var jpegDevice = new JpegDevice(new Resolution(300), 90);

            // Pages are 1‑based in Aspose.Pdf
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                // Build output file name with a sequential suffix starting at 01
                string outputPath = string.Format(outputPattern, pageNumber.ToString("D2"));

                // Convert the current page to JPEG and save it directly
                jpegDevice.Process(doc.Pages[pageNumber], outputPath);

                Console.WriteLine($"Saved page {pageNumber} as {outputPath}");
            }
        }
    }
}
