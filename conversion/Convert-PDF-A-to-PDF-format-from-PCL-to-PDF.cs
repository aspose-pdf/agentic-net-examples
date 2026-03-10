using System;
using System.IO;
using Aspose.Pdf;   // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Input PCL file (could contain PDF/A content after conversion)
        const string inputPclPath = "input.pcl";
        // Desired output PDF file
        const string outputPdfPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPclPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPclPath}");
            return;
        }

        // Initialize load options for PCL input
        PclLoadOptions pclLoadOptions = new PclLoadOptions();

        // Load the PCL file into a Document instance
        using (Document pdfDocument = new Document(inputPclPath, pclLoadOptions))
        {
            // If the loaded document is PDF/A, remove the compliance to obtain a regular PDF
            pdfDocument.RemovePdfaCompliance();

            // Save the document as a standard PDF
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"Conversion completed. PDF saved to '{outputPdfPath}'.");
    }
}