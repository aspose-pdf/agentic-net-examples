using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfFileInfo provides file‑level information and implements IDisposable.
        using (PdfFileInfo fileInfo = new PdfFileInfo(inputPath))
        {
            // Document also implements IDisposable; use a nested using block.
            using (Document doc = new Document(inputPath))
            {
                // Use Document to obtain the page count (PdfFileInfo has no PageCount property).
                Console.WriteLine($"Page count: {doc.Pages.Count}");

                // Add a simple text stamp to the first page.
                TextStamp stamp = new TextStamp("Sample")
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center,
                    Opacity             = 0.5f,
                    Background          = false
                };

                doc.Pages[1].AddStamp(stamp);

                // Save the modified PDF.
                doc.Save(outputPath);
                Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
            } // Document disposed here.
        } // PdfFileInfo disposed here.
    }
}
