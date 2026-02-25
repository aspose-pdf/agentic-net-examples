using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string mhtPath = "input.mht";
        const string outputPdf = "output.pdf";

        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtPath}");
            return;
        }

        // Load the MHT file using MhtLoadOptions
        MhtLoadOptions loadOptions = new MhtLoadOptions();

        // Ensure deterministic disposal of the Document
        using (Document doc = new Document(mhtPath, loadOptions))
        {
            // If the generated PDF is PDF/A, remove the compliance to obtain a regular PDF
            doc.RemovePdfaCompliance();

            // Save the result as a standard PDF file
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Conversion completed. PDF saved to '{outputPdf}'.");
    }
}