using System;
using System.IO;
using Aspose.Pdf; // XpsLoadOptions and Document are in this namespace

class Program
{
    static void Main()
    {
        const string inputXpsPath  = "input.xps";
        const string outputPdfPath = "output.pdf";

        // Verify that the source XPS file exists
        if (!File.Exists(inputXpsPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputXpsPath}");
            return;
        }

        // Load the XPS document using XpsLoadOptions and convert it to a regular PDF
        using (Document pdfDoc = new Document(inputXpsPath, new XpsLoadOptions()))
        {
            // Save as PDF (default format). No SaveOptions needed for PDF output.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Conversion completed: '{outputPdfPath}'");
    }
}