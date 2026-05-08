using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchPageSizeProcessor
{
    // Map file name patterns to desired page sizes.
    // Extend this dictionary as needed for your printing requirements.
    private static readonly Dictionary<string, PageSize> PageSizeMap = new Dictionary<string, PageSize>(StringComparer.OrdinalIgnoreCase)
    {
        { "A4", PageSize.A4 },
        // Letter and Legal are not available in older Aspose.Pdf versions; use explicit dimensions (points).
        // 1 inch = 72 points. Letter = 8.5" x 11" => 612 x 792 points.
        // Legal = 8.5" x 14" => 612 x 1008 points.
        { "Letter", new PageSize(612, 792) },
        { "Legal", new PageSize(612, 1008) }
        // Add more mappings here.
    };

    static void Main()
    {
        // Use paths that are safe on any OS. They are built relative to the executable's folder.
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string inputFolder = Path.Combine(baseDir, "PdfInput");   // Folder containing source PDFs.
        string outputFolder = Path.Combine(baseDir, "PdfOutput"); // Folder where processed PDFs will be saved.

        // Ensure the input and output directories exist.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder.
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Determine the target page size based on the file name.
                PageSize targetSize = DeterminePageSize(Path.GetFileNameWithoutExtension(inputPath));

                // Build the output file path (same name, different folder).
                string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

                // Use PdfPageEditor (a Facade) to change the page size.
                // BindPdf loads the document; PageSize sets the new size for all pages;
                // ApplyChanges writes the modifications; Save writes the result.
                using (PdfPageEditor editor = new PdfPageEditor())
                {
                    editor.BindPdf(inputPath);
                    editor.PageSize = targetSize;
                    editor.ApplyChanges();
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Processed '{inputPath}' → '{outputPath}' with page size {targetSize}.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }

    // Simple heuristic: look for a known keyword in the file name.
    // Returns A4 as default if no specific match is found.
    private static PageSize DeterminePageSize(string fileNameWithoutExtension)
    {
        foreach (var kvp in PageSizeMap)
        {
            if (fileNameWithoutExtension.IndexOf(kvp.Key, StringComparison.OrdinalIgnoreCase) >= 0)
                return kvp.Value;
        }

        // Default page size if no match.
        return PageSize.A4;
    }
}
