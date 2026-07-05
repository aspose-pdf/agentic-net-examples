using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Aspose.Pdf;

class BatchPageSizeProcessor
{
    // Mapping of file name (or pattern) to desired page size.
    // Adjust this dictionary to match your printing requirements.
    private static readonly Dictionary<string, PageSize> PageSizeMap = new Dictionary<string, PageSize>(StringComparer.OrdinalIgnoreCase)
    {
        // Example entries:
        // {"ReportA.pdf", PageSize.A4},
        // {"Invoice_*.pdf", PageSize.Letter},
        // {"Brochure.pdf", PageSize.A5}
    };

    // Determines the target page size for a given file.
    // Returns null if no specific size is defined (no change will be applied).
    private static PageSize GetTargetPageSize(string fileName)
    {
        // Direct match
        if (PageSizeMap.TryGetValue(fileName, out PageSize size))
            return size;

        // Simple wildcard support (e.g., "Invoice_*.pdf")
        foreach (var kvp in PageSizeMap)
        {
            string pattern = kvp.Key.Replace("*", ".*").Replace("?", ".");
            if (Regex.IsMatch(fileName, "^" + pattern + "$", RegexOptions.IgnoreCase))
                return kvp.Value;
        }

        return null; // No specific size defined
    }

    static void Main()
    {
        const string inputFolder = @"C:\PdfInput";
        const string outputFolder = @"C:\PdfOutput";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(inputPath);
            PageSize targetSize = GetTargetPageSize(fileName);

            // If no size mapping is found, copy the file unchanged.
            if (targetSize == null)
            {
                string copyPath = Path.Combine(outputFolder, fileName);
                File.Copy(inputPath, copyPath, overwrite: true);
                Console.WriteLine($"Copied without changes: {fileName}");
                continue;
            }

            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF document.
                using (Document doc = new Document(inputPath))
                {
                    // Apply the desired page size to every page.
                    foreach (Page page in doc.Pages)
                    {
                        page.PageInfo.Width = targetSize.Width;
                        page.PageInfo.Height = targetSize.Height;
                        page.PageInfo.IsLandscape = targetSize.IsLandscape;
                    }

                    // Save the modified document.
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed '{fileName}' with page size {targetSize}.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}
