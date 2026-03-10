using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xslFoPath = "input.xslfo";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"File not found: {xslFoPath}");
            return;
        }

        // Load the XSL-FO file using default load options
        XslFoLoadOptions loadOptions = new XslFoLoadOptions();

        using (Document doc = new Document(xslFoPath, loadOptions))
        {
            // Remove PDF/A compliance to produce a regular PDF
            doc.RemovePdfaCompliance();

            // Save the document as a standard PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Converted PDF saved to '{outputPdfPath}'.");
    }
}