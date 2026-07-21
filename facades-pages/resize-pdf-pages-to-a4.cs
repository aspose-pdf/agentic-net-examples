using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF into a byte array
        byte[] pdfBytes = File.ReadAllBytes(inputPath);

        // Use a memory stream to load the PDF document
        using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
        {
            // Load the document from the stream
            Document doc = new Document(pdfStream);

            // Change the size of every page to A4 (portrait)
            foreach (Page page in doc.Pages)
            {
                page.PageInfo.Width  = PageSize.A4.Width;   // width in points
                page.PageInfo.Height = PageSize.A4.Height;  // height in points
                page.PageInfo.IsLandscape = false;          // portrait orientation
            }

            // Save the modified document to a new file
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
