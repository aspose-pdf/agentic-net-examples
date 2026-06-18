using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string psFilePath = "input.ps";
        const string pdfFilePath = "output.pdf";

        // Verify the source PostScript file exists.
        if (!File.Exists(psFilePath))
        {
            Console.Error.WriteLine($"Source file not found: {psFilePath}");
            return;
        }

        // Load the PostScript file with default PsLoadOptions.
        // The Document constructor with a LoadOptions instance performs the conversion.
        using (Document pdfDocument = new Document(psFilePath, new PsLoadOptions()))
        {
            // Save the resulting PDF using default settings.
            pdfDocument.Save(pdfFilePath);
        }

        Console.WriteLine($"PostScript file successfully converted to PDF: {pdfFilePath}");
    }
}