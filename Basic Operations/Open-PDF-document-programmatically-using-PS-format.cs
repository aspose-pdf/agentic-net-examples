using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPs  = "input.ps";
        const string outputPdf = "output.pdf";

        // Verify the source PS file exists
        if (!File.Exists(inputPs))
        {
            Console.Error.WriteLine($"File not found: {inputPs}");
            return;
        }

        // Load the PostScript file using the appropriate load options
        PsLoadOptions psLoadOptions = new PsLoadOptions();

        // Wrap the Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPs, psLoadOptions))
        {
            // Save the loaded document as PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Successfully converted '{inputPs}' to '{outputPdf}'.");
    }
}