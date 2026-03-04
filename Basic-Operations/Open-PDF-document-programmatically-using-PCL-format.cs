using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPclPath = "input.pcl";
        const string outputPdfPath = "output.pdf";

        // Verify that the source PCL file exists.
        if (!File.Exists(inputPclPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPclPath}");
            return;
        }

        // Create load options specific to PCL format.
        PclLoadOptions pclLoadOptions = new PclLoadOptions();

        // Load the PCL file; the constructor performs the conversion to PDF.
        using (Document pdfDocument = new Document(inputPclPath, pclLoadOptions))
        {
            // Save the resulting PDF document.
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"PCL file '{inputPclPath}' successfully converted to PDF '{outputPdfPath}'.");
    }
}