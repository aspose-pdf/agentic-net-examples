using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Base directory of the application
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Directories for input and output PDFs (relative to the base directory)
        string inputDir = Path.Combine(baseDir, "InputPdfs");
        string outputDir = Path.Combine(baseDir, "OutputPdfs");

        // Ensure the input directory exists
        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Custom metadata value to add
        const string department = "Finance";

        // Process each PDF file in the input directory
        foreach (string pdfPath in Directory.GetFiles(inputDir, "*.pdf"))
        {
            try
            {
                string fileName = Path.GetFileName(pdfPath);
                string outputPath = Path.Combine(outputDir, fileName);

                // Load PDF metadata using the Facades API
                PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath);

                // Add or update the custom "Department" metadata field
                pdfInfo.SetMetaInfo("Department", department);

                // Save the PDF with the updated metadata
                pdfInfo.Save(outputPath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to process '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Custom 'Department' metadata added to all PDFs.");
    }
}
