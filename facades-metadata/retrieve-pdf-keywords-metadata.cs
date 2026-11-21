using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // Verify the file exists before attempting to read it
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfFileInfo gives access to document metadata without loading the full PDF.
        // It implements IDisposable, so wrap it in a using block.
        using (PdfFileInfo info = new PdfFileInfo(pdfPath))
        {
            // Retrieve the Keywords metadata field
            string keywords = info.Keywords;

            // Display the value (empty string if not set)
            Console.WriteLine($"Keywords: {keywords}");
        }
    }
}