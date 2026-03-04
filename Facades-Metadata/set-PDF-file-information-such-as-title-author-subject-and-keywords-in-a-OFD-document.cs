using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputOfd = "input.ofd";      // OFD source file
        const string outputPdf = "output.pdf";    // Resulting PDF with updated metadata

        if (!File.Exists(inputOfd))
        {
            Console.Error.WriteLine($"File not found: {inputOfd}");
            return;
        }

        // Load the OFD file via the PdfFileInfo facade and set the desired metadata.
        using (PdfFileInfo fileInfo = new PdfFileInfo(inputOfd))
        {
            fileInfo.Title    = "Sample OFD Document";
            fileInfo.Author   = "John Doe";
            fileInfo.Subject  = "Demo OFD to PDF with metadata";
            fileInfo.Keywords = "OFD, Aspose.Pdf, metadata";

            // Save the updated information. OFD cannot be saved directly, so the result is a PDF.
            fileInfo.SaveNewInfo(outputPdf);
        }

        Console.WriteLine($"Metadata applied and saved to '{outputPdf}'.");
    }
}