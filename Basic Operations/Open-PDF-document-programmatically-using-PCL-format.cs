using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory that contains the source PCL file.
        string dataDir = Path.Combine(Environment.CurrentDirectory, "Data");

        // Full path to the input PCL file.
        string pclPath = Path.Combine(dataDir, "input.pcl");

        // Full path where the resulting PDF will be saved.
        string pdfPath = Path.Combine(dataDir, "output.pdf");

        // Verify that the PCL file exists before attempting to load it.
        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"PCL file not found: {pclPath}");
            return;
        }

        // Initialize load options for PCL conversion.
        PclLoadOptions loadOptions = new PclLoadOptions();

        // Load the PCL file and convert it to PDF.
        using (Document pdfDocument = new Document(pclPath, loadOptions))
        {
            // Save the converted PDF.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PCL file successfully converted to PDF: {pdfPath}");
    }
}