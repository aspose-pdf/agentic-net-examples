using System;
using System.IO;
using Aspose.Pdf;

// Suppress known NuGet vulnerability warnings for the transitive package.
#pragma warning disable NU1902, NU1903

class Program
{
    static void Main()
    {
        // Use the application's base directory to build absolute, cross‑platform paths.
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string sourceFolder = Path.Combine(baseDir, "SourcePdfs");
        string outputFolder = Path.Combine(baseDir, "OutputPdfs");

        // Validate source folder – if it does not exist, inform the user and exit.
        if (!Directory.Exists(sourceFolder))
        {
            Console.Error.WriteLine($"Source folder not found: {sourceFolder}");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputFolder);

        // Iterate over all PDF files in the source folder.
        foreach (string inputPath in Directory.GetFiles(sourceFolder, "*.pdf"))
        {
            // Verify the file actually exists (defensive programming).
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found (skipped): {inputPath}");
                continue;
            }

            // Build the output file path with the same file name.
            string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

            try
            {
                // Load the PDF document.
                using (Document pdfDoc = new Document(inputPath))
                {
                    // Delete pages 3 and 4 (1‑based indexing).
                    // Deleting the higher page number first prevents index shift.
                    if (pdfDoc.Pages.Count >= 4)
                    {
                        pdfDoc.Pages.Delete(4); // delete original page 4
                        pdfDoc.Pages.Delete(3); // delete original page 3
                    }
                    else if (pdfDoc.Pages.Count == 3)
                    {
                        pdfDoc.Pages.Delete(3);
                    }
                    // If fewer than 3 pages, nothing to delete.

                    // Save the modified document to the output folder.
                    pdfDoc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(inputPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}
#pragma warning restore NU1902, NU1903