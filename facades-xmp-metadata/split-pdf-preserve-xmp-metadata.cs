using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "SplitPages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the source PDF once to retrieve its XMP metadata (if any) and page count
        int pageCount;
        // Store the XMP values directly; they are of type Aspose.Pdf.XmpValue
        var sourceMetadata = new Dictionary<string, XmpValue>();
        using (Document srcDoc = new Document(inputPath))
        {
            pageCount = srcDoc.Pages.Count; // Aspose.Pdf uses 1‑based indexing

            // Copy all existing XMP metadata entries into a dictionary for later reuse
            foreach (string key in srcDoc.Metadata.Keys)
            {
                sourceMetadata[key] = srcDoc.Metadata[key];
            }
        }

        // PdfFileEditor is a Facades class used for page extraction; it does NOT implement IDisposable
        PdfFileEditor editor = new PdfFileEditor();

        // Extract each page into a separate PDF file
        for (int i = 1; i <= pageCount; i++)
        {
            string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");

            // Correct overload: Extract(sourceFile, startPage, endPage, outputFile)
            editor.Extract(inputPath, i, i, outPath);

            // If the original PDF contained XMP metadata, copy it to the split file
            if (sourceMetadata.Count > 0)
            {
                using (Document splitDoc = new Document(outPath))
                {
                    foreach (var kvp in sourceMetadata)
                    {
                        splitDoc.Metadata[kvp.Key] = kvp.Value;
                    }
                    splitDoc.Save(outPath); // Save with the updated metadata
                }
            }

            Console.WriteLine($"Saved page {i} → {outPath}");
        }

        Console.WriteLine("Splitting completed with XMP metadata preserved.");
    }
}
