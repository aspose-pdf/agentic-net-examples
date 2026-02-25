using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xpsPath = "input.xps";
        const string pdfPath = "output.pdf";

        if (!File.Exists(xpsPath))
        {
            Console.Error.WriteLine($"XPS file not found: {xpsPath}");
            return;
        }

        try
        {
            // Load the XPS file with XpsLoadOptions
            using (Document doc = new Document(xpsPath, new XpsLoadOptions()))
            {
                // Remove PDF/A compliance if present, producing a regular PDF
                doc.RemovePdfaCompliance();

                // Save the document as PDF
                doc.Save(pdfPath);
            }

            Console.WriteLine($"Conversion completed: {pdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}