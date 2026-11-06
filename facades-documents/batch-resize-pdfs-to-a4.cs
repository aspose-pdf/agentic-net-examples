using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Use the application's base directory to build relative paths – this works on any platform
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Input and output folders (relative to the base directory)
        string sourceFolder = Path.Combine(baseDir, "SourcePdfs");
        string targetFolder = Path.Combine(baseDir, "ResizedPdfs");

        // Validate that the source folder exists before proceeding
        if (!Directory.Exists(sourceFolder))
        {
            Console.Error.WriteLine($"Source folder not found: {sourceFolder}");
            return;
        }

        // Ensure the target directory exists
        Directory.CreateDirectory(targetFolder);

        // Retrieve all PDF files in the source folder
        string[] pdfFiles = Directory.GetFiles(sourceFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found in the source folder.");
            return;
        }

        // A4 size in points (1 point = 1/72 inch)
        const double a4Width = 595; // 210 mm
        const double a4Height = 842; // 297 mm

        // Process each PDF file
        foreach (string inputPath in pdfFiles)
        {
            try
            {
                string fileName = Path.GetFileName(inputPath);
                string outputPath = Path.Combine(targetFolder, fileName);

                // Load the PDF document inside a using block to guarantee disposal
                using (var pdfDoc = new Document(inputPath))
                {
                    // Resize every page to A4 (portrait)
                    foreach (Page page in pdfDoc.Pages)
                    {
                        page.PageInfo.Width = a4Width;
                        page.PageInfo.Height = a4Height;
                        page.PageInfo.IsLandscape = false;
                    }

                    // Save the resized PDF
                    pdfDoc.Save(outputPath);
                }

                Console.WriteLine($"Resized: {fileName} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch resizing completed.");
    }
}
