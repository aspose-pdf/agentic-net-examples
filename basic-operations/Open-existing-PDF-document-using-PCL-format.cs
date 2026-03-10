using System;
using System.IO;
using Aspose.Pdf; // PclLoadOptions and Document are in this namespace

class Program
{
    static void Main()
    {
        // Input PCL file and desired PDF output paths
        const string pclPath = "input.pcl";
        const string pdfPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"File not found: {pclPath}");
            return;
        }

        // Initialize load options for PCL conversion
        PclLoadOptions pclLoadOptions = new PclLoadOptions();

        // Open the PCL file as a PDF document using the constructor that accepts LoadOptions
        // The Document is wrapped in a using block for deterministic disposal (document-disposal-with-using rule)
        using (Document pdfDoc = new Document(pclPath, pclLoadOptions))
        {
            // Save the resulting PDF (no special SaveOptions needed for PDF output)
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"Converted PCL to PDF: {pdfPath}");
    }
}