using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Base directory of the executable (works for both Windows and Linux)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Resolve input and output folders relative to the base directory
        string inputFolder = Path.Combine(baseDir, "InputPdfs");
        string outputFolder = Path.Combine(baseDir, "OutputPdfs");

        // Validate the input folder – if it does not exist, inform the user and stop.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            Console.Error.WriteLine("Create the folder and place PDF files inside before running the program.");
            return;
        }

        // Ensure the output folder exists.
        Directory.CreateDirectory(outputFolder);

        // Loop through all PDF files in the input folder.
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Determine output file path (same name, different folder).
                string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

                // Use PdfFileInfo (Facades API) to bind the PDF and set custom metadata.
                PdfFileInfo pdfInfo = new PdfFileInfo();
                pdfInfo.BindPdf(inputPath);                     // Load the PDF
                pdfInfo.SetMetaInfo("Department", "Finance"); // Add custom metadata field

                // Save the modified PDF to the output location.
                pdfInfo.Save(outputPath);

                Console.WriteLine($"Processed '{Path.GetFileName(inputPath)}' -> '{outputPath}'");
            }
            catch (Exception ex)
            {
                // Log the error but continue processing the remaining files.
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("All PDFs have been processed.");
    }
}
