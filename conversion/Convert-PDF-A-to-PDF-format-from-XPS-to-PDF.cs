using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for source XPS file and destination PDF file
        const string xpsPath = "input.xps";
        const string pdfPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(xpsPath))
        {
            Console.Error.WriteLine($"Source file not found: {xpsPath}");
            return;
        }

        // Load the XPS document using XpsLoadOptions
        XpsLoadOptions loadOptions = new XpsLoadOptions();

        // Wrap the Document in a using block for proper disposal
        using (Document doc = new Document(xpsPath, loadOptions))
        {
            // If the loaded document is PDF/A, remove the compliance to obtain a regular PDF
            doc.RemovePdfaCompliance();

            // Save the document as a standard PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"Conversion completed. PDF saved to '{pdfPath}'.");
    }
}