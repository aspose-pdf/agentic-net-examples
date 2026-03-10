using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF with annotations
        const string xfdfFile   = "annotations.xfdf";   // temporary XFDF file
        const string outputPdf  = "output.pdf";         // final PDF after import

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF, export its annotations to XFDF, then re‑import them and save
        using (Document doc = new Document(inputPdf))
        {
            // Export all annotations to an XFDF file
            doc.ExportAnnotationsToXfdf(xfdfFile);

            // (Optional) modify the document here if needed

            // Import annotations back from the XFDF file
            doc.ImportAnnotationsFromXfdf(xfdfFile);

            // Save the resulting PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Annotations exported to '{xfdfFile}' and re‑imported. Result saved as '{outputPdf}'.");
    }
}