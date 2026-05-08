using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string psPath = "input.ps";
        const string pdfPath = "output.pdf";

        if (!File.Exists(psPath))
        {
            Console.Error.WriteLine($"File not found: {psPath}");
            return;
        }

        // Load the PostScript file using the appropriate load options.
        PsLoadOptions loadOptions = new PsLoadOptions();

        // Wrap the Document in a using block for deterministic disposal.
        using (Document pdfDoc = new Document(psPath, loadOptions))
        {
            // Save the loaded document as PDF using default settings.
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"Successfully converted '{psPath}' to PDF at '{pdfPath}'.");
    }
}