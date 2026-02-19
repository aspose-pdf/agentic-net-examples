using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Define input PCL file and output PDF file paths.
        string dataDir = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        string pclPath = Path.Combine(dataDir, "sample.pcl");
        string pdfPath = Path.Combine(dataDir, "output.pdf");

        // Verify that the source PCL file exists.
        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"Error: PCL file not found at '{pclPath}'.");
            return;
        }

        // Set up load options for the PCL file.
        PclLoadOptions loadOptions = new PclLoadOptions();

        // Choose the conversion engine (LegacyEngine is default; NewEngine can be used if needed).
        loadOptions.ConversionEngine = PclLoadOptions.ConversionEngines.NewEngine;

        // Load the PCL file into a PDF document using the specified options.
        using (Document pdfDocument = new Document(pclPath, loadOptions))
        {
            // Save the document as a regular PDF file.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"Conversion completed successfully. PDF saved to '{pdfPath}'.");
    }
}