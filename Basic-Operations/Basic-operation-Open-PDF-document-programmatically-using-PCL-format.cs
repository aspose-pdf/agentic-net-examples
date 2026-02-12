using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PCL file and output PDF file paths
        const string pclPath = "input.pcl";
        const string pdfPath = "output.pdf";

        // Verify that the PCL file exists
        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"PCL file not found: {pclPath}");
            return;
        }

        try
        {
            // Load the PCL file using PclLoadOptions
            PclLoadOptions loadOptions = new PclLoadOptions();
            // Example: use the new conversion engine if needed
            // loadOptions.ConversionEngine = PclLoadOptions.ConversionEngines.NewEngine;

            // Create a Document from the PCL file
            using (Document pdfDocument = new Document(pclPath, loadOptions))
            {
                // Save the resulting PDF
                pdfDocument.Save(pdfPath);
                Console.WriteLine($"Successfully converted PCL to PDF: {pdfPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}