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
            Console.Error.WriteLine($"File not found: {psInputPath}");
            return;
        }

        try
        {
            // Load the PostScript file using PsLoadOptions (input‑only format)
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(psInputPath, new Aspose.Pdf.PsLoadOptions()))
            {
                // Save the loaded document as PDF
                doc.Save(pdfOutputPath);
            }

            Console.WriteLine($"Successfully converted PS to PDF: '{pdfOutputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}