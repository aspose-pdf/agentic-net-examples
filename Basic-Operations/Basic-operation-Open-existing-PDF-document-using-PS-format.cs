using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, PsLoadOptions, etc.)

class Program
{
    static void Main()
    {
        // Input PostScript file and desired PDF output file
        const string inputPsPath  = "input.ps";
        const string outputPdfPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPsPath))
        {
            Console.Error.WriteLine($"File not found: {inputPsPath}");
            return;
        }

        // Load the PS file using the appropriate load options and save it as PDF.
        // Both the Document and the load options are created inside a using block
        // to ensure deterministic disposal of resources.
        using (Document doc = new Document(inputPsPath, new PsLoadOptions()))
        {
            // Save the loaded document as PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PostScript file '{inputPsPath}' was converted to PDF '{outputPdfPath}'.");
    }
}