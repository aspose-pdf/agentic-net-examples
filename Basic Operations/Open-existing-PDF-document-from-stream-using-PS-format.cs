using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document, PsLoadOptions, etc.

class Program
{
    static void Main()
    {
        // Path to the source PostScript file (PS format)
        const string psFilePath = "input.ps";
        // Desired output PDF file path
        const string pdfOutputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(psFilePath))
        {
            Console.Error.WriteLine($"Source file not found: {psFilePath}");
            return;
        }

        // Open the PS file as a read‑only stream
        using (FileStream psStream = File.OpenRead(psFilePath))
        {
            // Configure load options for PS format
            PsLoadOptions loadOptions = new PsLoadOptions();

            // Load the PS stream into a Document instance
            using (Document pdfDoc = new Document(psStream, loadOptions))
            {
                // Save the loaded document as a PDF file
                pdfDoc.Save(pdfOutputPath);
            }
        }

        Console.WriteLine($"Successfully converted '{psFilePath}' to PDF at '{pdfOutputPath}'.");
    }
}