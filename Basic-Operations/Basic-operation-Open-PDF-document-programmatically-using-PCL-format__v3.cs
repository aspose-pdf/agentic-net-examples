using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the directory containing the PCL file.
        string dataDir = @"YOUR_DATA_DIRECTORY";

        // Input PCL file and output PDF file.
        string pclPath = Path.Combine(dataDir, "input.pcl");
        string pdfPath = Path.Combine(dataDir, "output.pdf");

        // Verify that the PCL file exists.
        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"PCL file not found: {pclPath}");
            return;
        }

        // Initialize load options for PCL conversion.
        PclLoadOptions pclLoadOptions = new PclLoadOptions();

        // Load the PCL file into a PDF Document using the provided constructor.
        using (Document pdfDocument = new Document(pclPath, pclLoadOptions))
        {
            // Save the resulting PDF document.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PCL file successfully converted to PDF: {pdfPath}");
    }
}