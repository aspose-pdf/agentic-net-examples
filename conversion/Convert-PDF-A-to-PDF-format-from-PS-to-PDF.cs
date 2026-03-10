using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string psInputPath = "input.ps";
        const string pdfOutputPath = "output.pdf";

        if (!File.Exists(psInputPath))
        {
            Console.Error.WriteLine($"Input file not found: {psInputPath}");
            return;
        }

        try
        {
            // Load the PostScript file using PsLoadOptions (PS is input‑only)
            using (Document doc = new Document(psInputPath, new PsLoadOptions()))
            {
                // If the source PDF/A compliance needs to be removed, call this method.
                // It converts the document to a regular PDF without PDF/A restrictions.
                doc.RemovePdfaCompliance();

                // Save the result as a standard PDF.
                doc.Save(pdfOutputPath);
            }

            Console.WriteLine($"Conversion completed: '{pdfOutputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}