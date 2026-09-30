using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Use the executable's folder as a reliable base directory.
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string sourceFolder = Path.Combine(baseDir, "SourcePdfs");
        string outputFolder = Path.Combine(baseDir, "OutputPdfs");

        // Validate the source folder – if it does not exist, inform the user and stop.
        if (!Directory.Exists(sourceFolder))
        {
            Console.Error.WriteLine($"Source folder not found: {sourceFolder}");
            Console.Error.WriteLine("Create the folder and place PDF files inside, then rerun the program.");
            return;
        }

        // Ensure the output folder exists.
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the source folder.
        foreach (string inputPath in Directory.GetFiles(sourceFolder, "*.pdf"))
        {
            try
            {
                string fileName = Path.GetFileName(inputPath);
                string outputPath = Path.Combine(outputFolder, fileName);

                // Defensive check – the file should exist because GetFiles returned it, but guard anyway.
                if (!File.Exists(inputPath))
                {
                    Console.Error.WriteLine($"File not found (skipped): {inputPath}");
                    continue;
                }

                // Load, modify, and save the PDF inside a using block to release resources promptly.
                using (Document pdfDoc = new Document(inputPath))
                {
                    // Delete pages 3 and 4 (1‑based indexing). After the first deletion the original
                    // page 4 becomes page 3, so we delete page 3 a second time.
                    if (pdfDoc.Pages.Count >= 4)
                    {
                        pdfDoc.Pages.Delete(3); // removes original page 3
                        pdfDoc.Pages.Delete(3); // original page 4 is now at position 3
                    }
                    else if (pdfDoc.Pages.Count == 3)
                    {
                        pdfDoc.Pages.Delete(3); // only page 3 exists
                    }
                    // If the document has fewer than 3 pages, nothing is deleted.

                    pdfDoc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {fileName}");
            }
            catch (Exception ex)
            {
                // Log the error but continue with the next file.
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Page deletion completed.");
    }
}
