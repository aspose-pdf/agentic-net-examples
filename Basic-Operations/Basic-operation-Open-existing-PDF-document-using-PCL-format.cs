using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the directory containing the input PCL file.
        string dataDir = @"YOUR_DATA_DIRECTORY";

        // Input PCL file and output PDF file paths.
        string pclPath = Path.Combine(dataDir, "input.pcl");
        string pdfPath = Path.Combine(dataDir, "output.pdf");

        // Verify that the PCL file exists before attempting to load it.
        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"Error: PCL file not found at '{pclPath}'.");
            return;
        }

        // Create load options for PCL conversion. Additional options can be set if needed.
        PclLoadOptions pclOptions = new PclLoadOptions();

        // Load the PCL file into an Aspose.Pdf Document using the load options.
        using (Document pdfDocument = new Document(pclPath, pclOptions))
        {
            // Save the loaded document as a PDF file.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PCL file successfully converted and saved to '{pdfPath}'.");
    }
}