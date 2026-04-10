using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PostScript file and the target PDF file.
        const string psFilePath = "input.ps";
        const string pdfFilePath = "output.pdf";

        // Verify that the source file exists.
        if (!File.Exists(psFilePath))
        {
            Console.Error.WriteLine($"Source file not found: {psFilePath}");
            return;
        }

        // Load the PostScript file using the appropriate load options.
        // PsLoadOptions is the concrete LoadOptions class for PS files.
        using (Document pdfDocument = new Document(psFilePath, new PsLoadOptions()))
        {
            // Save the loaded document as PDF. No SaveOptions are required for PDF output.
            pdfDocument.Save(pdfFilePath);
        }

        Console.WriteLine($"PostScript file converted to PDF: {pdfFilePath}");
    }
}