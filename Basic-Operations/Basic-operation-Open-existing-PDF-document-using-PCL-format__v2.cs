using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pclPath = "input.pcl";
        const string pdfPath = "output.pdf";

        // Verify the source PCL file exists
        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"File not found: {pclPath}");
            return;
        }

        // Configure loading options for PCL format
        PclLoadOptions loadOptions = new PclLoadOptions();
        // Example: choose the newer conversion engine if needed
        // loadOptions.ConversionEngine = PclLoadOptions.ConversionEngines.NewEngine;

        // Load the PCL file into a Document instance
        using (Document doc = new Document(pclPath, loadOptions))
        {
            // Save the resulting PDF; Document.Save(string) writes PDF by default
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PCL file successfully converted to PDF: '{pdfPath}'");
    }
}