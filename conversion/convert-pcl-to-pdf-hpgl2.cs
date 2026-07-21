using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PCL file and the destination PDF file.
        const string pclPath = "input.pcl";
        const string pdfPath = "output.pdf";

        // Verify that the source file exists.
        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"File not found: {pclPath}");
            return;
        }

        // Create load options for PCL conversion.
        // HP‑GL/2 vectors are loaded automatically; no explicit property is required.
        PclLoadOptions loadOptions = new PclLoadOptions();

        // Load the PCL file with the specified options and save it as PDF.
        using (Document pdfDocument = new Document(pclPath, loadOptions))
        {
            pdfDocument.Save(pdfPath); // Save as PDF (default format).
        }

        Console.WriteLine($"PCL file '{pclPath}' successfully converted to PDF at '{pdfPath}'.");
    }
}