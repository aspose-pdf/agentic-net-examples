using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document and PsLoadOptions

class Program
{
    static void Main()
    {
        const string psInputPath  = "input.ps";   // PostScript file to open
        const string pdfOutputPath = "output.pdf"; // Destination PDF file

        // Verify the source file exists
        if (!File.Exists(psInputPath))
        {
            Console.Error.WriteLine($"File not found: {psInputPath}");
            return;
        }

        // Load the PS file using PsLoadOptions (PS is input‑only format)
        PsLoadOptions loadOptions = new PsLoadOptions();

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(psInputPath, loadOptions))
        {
            // Save the loaded content as a PDF
            doc.Save(pdfOutputPath);
        }

        Console.WriteLine($"PS file '{psInputPath}' successfully opened and saved as PDF to '{pdfOutputPath}'.");
    }
}