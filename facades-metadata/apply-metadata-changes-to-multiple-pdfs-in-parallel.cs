using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Determine a reliable base directory (the folder where the executable resides)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Resolve input folder – if the expected folder does not exist, fall back to the base directory
        string inputDir = Path.Combine(baseDir, "InputPdfs");
        if (!Directory.Exists(inputDir))
        {
            Console.WriteLine($"Input folder '{inputDir}' not found. Falling back to base directory.");
            inputDir = baseDir;
        }

        // Get PDF files – if none are found, inform the user and exit gracefully
        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputDir}'. Nothing to process.");
            return;
        }

        // Resolve (and create) output folder
        string outputDir = Path.Combine(baseDir, "OutputPdfs");
        Directory.CreateDirectory(outputDir);

        // Metadata values to apply
        const string newTitle    = "Processed Document";
        const string newAuthor   = "Automation";
        const string newSubject  = "Batch Metadata Update";
        const string newKeywords = "Aspose,PDF,Metadata";

        // Process each file in parallel
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                // Verify the file still exists (it could have been moved/deleted between enumeration and processing)
                if (!File.Exists(pdfPath))
                {
                    Console.Error.WriteLine($"File not found: {pdfPath}");
                    return;
                }

                // Load file info using Facades API
                PdfFileInfo info = new PdfFileInfo(pdfPath);

                // Apply metadata changes
                info.Title    = newTitle;
                info.Author   = newAuthor;
                info.Subject  = newSubject;
                info.Keywords = newKeywords;

                // Save the updated PDF to the output folder
                string outPath = Path.Combine(outputDir, Path.GetFileName(pdfPath));
                info.Save(outPath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        });

        Console.WriteLine("Metadata update completed.");
    }
}
