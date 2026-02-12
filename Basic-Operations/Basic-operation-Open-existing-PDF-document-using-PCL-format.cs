using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Use the current working directory for input and output files
        string dataDir = Directory.GetCurrentDirectory();
        string pclPath = Path.Combine(dataDir, "input.pcl");
        string pdfPath = Path.Combine(dataDir, "output.pdf");

        // Verify that the PCL file exists before attempting conversion
        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"Error: PCL file not found at '{pclPath}'.");
            return;
        }

        try
        {
            // PclLoadOptions resides directly in the Aspose.Pdf namespace; no extra using needed
            var pclOptions = new PclLoadOptions();

            // Load the PCL file with the specified options and save it as PDF
            using (var pdfDocument = new Document(pclPath, pclOptions))
            {
                pdfDocument.Save(pdfPath);
            }

            Console.WriteLine($"Conversion successful. PDF saved to '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}