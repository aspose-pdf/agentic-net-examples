using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input folder containing PDFs
        const string inputFolder = "InputPdfs";
        // Output folder for processed PDFs
        const string outputFolder = "OutputPdfs";

        // Resolve paths relative to the executable location (helps when running from different working directories)
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        string inputPathFull = Path.Combine(basePath, inputFolder);
        string outputPathFull = Path.Combine(basePath, outputFolder);

        // Ensure the output directory exists
        Directory.CreateDirectory(outputPathFull);

        // Verify that the input directory exists; if not, inform the user and exit gracefully
        if (!Directory.Exists(inputPathFull))
        {
            Console.WriteLine($"Input folder not found: {inputPathFull}");
            Console.WriteLine("Please create the folder and place PDF files inside before running the program.");
            return;
        }

        // Loop through all PDF files in the input folder
        foreach (string inputFile in Directory.GetFiles(inputPathFull, "*.pdf"))
        {
            try
            {
                // Load the PDF using the Facades PdfFileInfo class
                PdfFileInfo pdfInfo = new PdfFileInfo(inputFile);

                // Set a custom metadata field named "Department"
                pdfInfo.SetMetaInfo("Department", "Finance");

                // Determine the output file path (same name, different folder)
                string outputFile = Path.Combine(outputPathFull, Path.GetFileName(inputFile));

                // Save the updated PDF with the new metadata
                pdfInfo.SaveNewInfo(outputFile);

                // Release resources held by the facade
                pdfInfo.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{inputFile}': {ex.Message}");
            }
        }

        Console.WriteLine("Metadata update completed.");
    }
}
