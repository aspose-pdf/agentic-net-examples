using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Define input PCL file and output PDF file paths
        string dataDir = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        string pclPath = Path.Combine(dataDir, "sample.pcl");
        string pdfPath = Path.Combine(dataDir, "output.pdf");

        // Verify that the source PCL file exists
        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"Error: PCL file not found at '{pclPath}'.");
            return;
        }

        // Initialize PCL load options (optional: select conversion engine)
        PclLoadOptions loadOptions = new PclLoadOptions();
        loadOptions.ConversionEngine = PclLoadOptions.ConversionEngines.NewEngine;

        // Load the PCL file into a PDF document using the specified options
        using (Document pdfDocument = new Document(pclPath, loadOptions))
        {
            // Save the resulting PDF document
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"Conversion successful. PDF saved to '{pdfPath}'.");
    }
}