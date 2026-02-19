using System;
using System.IO;
using Aspose.Pdf; // Core PDF classes

class Program
{
    static void Main()
    {
        // Input PCL file and desired PDF output paths (generic names)
        const string pclPath = "input.pcl";
        const string pdfPath = "output.pdf";

        // Verify that the source PCL file exists
        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"Error: PCL file not found at '{pclPath}'.");
            return;
        }

        try
        {
            // Create load options for PCL conversion
            var loadOptions = new PclLoadOptions();

            // Load the PCL file and convert it to a PDF Document
            var pdfDocument = new Document(pclPath, loadOptions);

            // Save the resulting PDF document
            pdfDocument.Save(pdfPath);

            Console.WriteLine($"PCL file successfully converted and saved as PDF: {pdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}