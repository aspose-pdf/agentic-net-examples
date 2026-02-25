using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // Facade namespace (required by task)

class Program
{
    static void Main()
    {
        const string inputOfdPath   = "input.ofd";   // OFD source file
        const string outputPdfPath  = "output.pdf";  // Desired PDF output

        if (!File.Exists(inputOfdPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputOfdPath}");
            return;
        }

        try
        {
            // Load the OFD file using the concrete load options for OFD.
            // OfdLoadOptions derives from LoadOptions and is not abstract.
            var ofdLoadOptions = new OfdLoadOptions();

            using (Document doc = new Document(inputOfdPath, ofdLoadOptions))
            {
                // The document may be a PDF/A; saving without specifying options
                // produces a regular PDF (PDF 1.7 by default).
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Conversion completed: '{outputPdfPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}