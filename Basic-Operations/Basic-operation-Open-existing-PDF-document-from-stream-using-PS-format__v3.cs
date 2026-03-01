using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string psFilePath   = "input.ps";      // PostScript file to load
        const string outputPdfPath = "output.pdf";   // Resulting PDF file

        if (!File.Exists(psFilePath))
        {
            Console.Error.WriteLine($"File not found: {psFilePath}");
            return;
        }

        // Open the PostScript file as a stream and load it using PsLoadOptions (PS is input‑only)
        using (FileStream psStream = File.OpenRead(psFilePath))
        {
            // LoadOptions for PostScript input
            PsLoadOptions loadOptions = new PsLoadOptions();

            // Create a Document from the stream with the specified load options
            using (Document doc = new Document(psStream, loadOptions))
            {
                // Save the loaded document as PDF (the default format)
                doc.Save(outputPdfPath);
                Console.WriteLine($"PostScript file converted and saved as PDF: '{outputPdfPath}'");
            }
        }
    }
}