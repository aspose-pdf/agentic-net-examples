using System;
using System.IO;
using Aspose.Pdf; // PsLoadOptions and Document are in this namespace

class Program
{
    static void Main()
    {
        // Input PostScript file and output PDF file paths
        const string psPath = "input.ps";
        const string pdfPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(psPath))
        {
            Console.Error.WriteLine($"File not found: {psPath}");
            return;
        }

        // Create load options for PostScript files
        PsLoadOptions loadOptions = new PsLoadOptions();

        // Open the PS file as a PDF document using a using block for deterministic disposal
        using (Document doc = new Document(psPath, loadOptions))
        {
            // Save the resulting PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PostScript file '{psPath}' successfully converted to PDF '{pdfPath}'.");
    }
}