using System;
using System.IO;
using Aspose.Pdf;

class BatchResizePdf
{
    static void Main()
    {
        // Use the application's base directory to build input/output paths.
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string sourceFolder = Path.Combine(baseDir, "SourcePdfs");
        string targetFolder = Path.Combine(baseDir, "ResizedPdfs");

        // Validate source folder – if it does not exist, create it and inform the user.
        if (!Directory.Exists(sourceFolder))
        {
            Console.WriteLine($"Source folder not found: '{sourceFolder}'. Creating an empty folder.");
            Directory.CreateDirectory(sourceFolder);
            Console.WriteLine("Place PDF files into the source folder and re‑run the program.");
            return;
        }

        // Ensure the target directory exists.
        Directory.CreateDirectory(targetFolder);

        // Get all PDF files in the source folder.
        string[] pdfFiles = Directory.GetFiles(sourceFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{sourceFolder}'." );
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Verify the file still exists before loading.
                if (!File.Exists(pdfPath))
                {
                    Console.Error.WriteLine($"File not found: {pdfPath}");
                    continue;
                }

                // Load the PDF document inside a using block to ensure proper disposal.
                using (Document pdfDoc = new Document(pdfPath))
                {
                    // Resize each page to A4 size.
                    foreach (Page page in pdfDoc.Pages)
                    {
                        page.SetPageSize(PageSize.A4.Width, PageSize.A4.Height);
                    }

                    // Build the output file path.
                    string outputPath = Path.Combine(targetFolder, Path.GetFileName(pdfPath));

                    // Save the resized PDF.
                    pdfDoc.Save(outputPath);
                }

                Console.WriteLine($"Resized: {Path.GetFileName(pdfPath)} → {targetFolder}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch resizing completed.");
    }
}
