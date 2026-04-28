using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PCL file
        const string pclPath = "input.pcl";
        // Path for the resulting PDF file
        const string pdfPath = "output.pdf";

        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"File not found: {pclPath}");
            return;
        }

        // Create load options for PCL conversion
        PclLoadOptions loadOptions = new PclLoadOptions();

        // Enable HP‑GL/2 vector loading.
        // The NewEngine conversion engine supports HP‑GL/2 graphics.
        loadOptions.ConversionEngine = PclLoadOptions.ConversionEngines.NewEngine;

        // Load the PCL file into a Document using the specified options
        using (Document pdfDocument = new Document(pclPath, loadOptions))
        {
            // Save the document as PDF
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"Successfully converted '{pclPath}' to PDF '{pdfPath}'.");
    }
}