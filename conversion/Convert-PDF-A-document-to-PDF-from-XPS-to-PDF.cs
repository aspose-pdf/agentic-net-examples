using System;
using System.IO;
using Aspose.Pdf; // Document, XpsLoadOptions, etc.

class Program
{
    static void Main()
    {
        // Input XPS file (assumed to be PDF/A compliant or any XPS)
        const string xpsPath = "input.xps";
        // Desired output PDF file (standard PDF, not PDF/A)
        const string pdfPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(xpsPath))
        {
            Console.Error.WriteLine($"Source file not found: {xpsPath}");
            return;
        }

        // Initialize load options for XPS format
        XpsLoadOptions loadOptions = new XpsLoadOptions();

        // Load the XPS document and convert it to PDF
        using (Document pdfDocument = new Document(xpsPath, loadOptions))
        {
            // Save as PDF. No SaveOptions needed because the default format is PDF.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"Conversion completed: '{xpsPath}' → '{pdfPath}'");
    }
}