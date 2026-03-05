using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pclPath = "input.pcl";
        const string pdfPath = "output.pdf";

        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"File not found: {pclPath}");
            return;
        }

        // Load the PCL file using PclLoadOptions
        PclLoadOptions loadOptions = new PclLoadOptions();

        // Convert to PDF by loading and then saving
        using (Document pdfDocument = new Document(pclPath, loadOptions))
        {
            // Save as PDF (default format)
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"Conversion completed: '{pdfPath}'");
    }
}