using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, MhtLoadOptions)

class Program
{
    static void Main()
    {
        const string inputMhtml = "input.mht";
        const string outputPdf  = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputMhtml))
        {
            Console.Error.WriteLine($"Input file not found: {inputMhtml}");
            return;
        }

        try
        {
            // Load the MHTML file using MhtLoadOptions
            MhtLoadOptions loadOptions = new MhtLoadOptions();

            // Wrap the Document in a using block for deterministic disposal
            using (Document doc = new Document(inputMhtml, loadOptions))
            {
                // Save the document as PDF (default format)
                doc.Save(outputPdf);
            }

            Console.WriteLine($"MHTML successfully converted to PDF: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}