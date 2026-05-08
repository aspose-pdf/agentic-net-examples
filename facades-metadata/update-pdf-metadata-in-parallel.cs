using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Resolve input and output directories relative to the executable location.
        // This makes the code work on any OS (Windows, Linux, macOS).
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string inputDirectory = Path.Combine(baseDir, "InputPdfs");
        string outputDirectory = Path.Combine(baseDir, "OutputPdfs");

        // Ensure the directories exist.
        Directory.CreateDirectory(inputDirectory);
        Directory.CreateDirectory(outputDirectory);

        // Get all PDF files from the input folder.
        string[] inputFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);
        if (inputFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputDirectory}'." );
            return;
        }

        // Process each PDF file in parallel.
        Parallel.ForEach(inputFiles, inputPath =>
        {
            try
            {
                // Build the output file path (same file name, different folder).
                string outputPath = Path.Combine(outputDirectory, Path.GetFileName(inputPath));

                // Use PdfFileInfo facade to modify metadata.
                using (PdfFileInfo info = new PdfFileInfo())
                {
                    info.BindPdf(inputPath);
                    info.Title = "Updated Title";
                    info.Author = "Updated Author";
                    info.Subject = "Updated Subject";
                    info.Keywords = "Updated, Keywords";
                    info.SaveNewInfo(outputPath);
                }

                Console.WriteLine($"Metadata updated: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        });
    }
}
