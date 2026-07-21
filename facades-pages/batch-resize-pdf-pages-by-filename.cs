using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where the processed PDFs will be saved
        const string outputFolder = "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Mapping of filename keywords to the desired page size.
        // For sizes not provided by the current Aspose.Pdf version, create custom PageSize instances (points).
        var sizeMap = new Dictionary<string, PageSize>(StringComparer.OrdinalIgnoreCase)
        {
            { "A4", PageSize.A4 },
            // Letter: 8.5" x 11" => 612 x 792 points (1 point = 1/72 inch)
            { "Letter", new PageSize(612, 792) },
            // Legal: 8.5" x 14" => 612 x 1008 points
            { "Legal", new PageSize(612, 1008) }
        };

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);

            // Determine the target page size based on the filename.
            // Default to A4 if no keyword matches.
            PageSize targetSize = PageSize.A4;
            foreach (var kvp in sizeMap)
            {
                if (fileNameWithoutExt.Contains(kvp.Key, StringComparison.OrdinalIgnoreCase))
                {
                    targetSize = kvp.Value;
                    break;
                }
            }

            string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

            // Use PdfPageEditor (Aspose.Pdf.Facades) to change the page size.
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Load the source PDF.
                editor.BindPdf(inputPath);

                // Apply the desired page size to all pages.
                editor.PageSize = targetSize;

                // Commit the changes.
                editor.ApplyChanges();

                // Save the modified PDF.
                editor.Save(outputPath);
            }

            Console.WriteLine($"Processed '{inputPath}' → '{outputPath}' with page size {targetSize}");
        }
    }
}
