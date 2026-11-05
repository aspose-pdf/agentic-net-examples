using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "cleaned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Clear standard document information fields (use empty strings / MinValue for non‑nullable types)
            doc.Info.Title = string.Empty;
            doc.Info.Author = string.Empty;
            doc.Info.Subject = string.Empty;
            doc.Info.Keywords = string.Empty;
            doc.Info.Creator = string.Empty;
            doc.Info.Producer = string.Empty;
            doc.Info.CreationDate = DateTime.MinValue;
            doc.Info.ModDate = DateTime.MinValue;

            // Remove hidden XMP metadata entries, if any
            if (doc.Metadata != null)
            {
                // Collect all existing keys first to avoid modifying the collection while iterating
                List<string> keys = doc.Metadata.Keys.ToList();
                foreach (string key in keys)
                {
                    doc.Metadata.Remove(key);
                }
            }

            // Remove any document‑level JavaScript (OpenAction) – there is no collection to clear
            doc.OpenAction = null;

            // Remove any embedded files (attachments) from the PDF using 1‑based indexing and Delete(name)
            if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
            {
                for (int i = doc.EmbeddedFiles.Count; i >= 1; i--)
                {
                    var fileSpec = doc.EmbeddedFiles[i];
                    if (fileSpec != null && !string.IsNullOrEmpty(fileSpec.Name))
                    {
                        doc.EmbeddedFiles.Delete(fileSpec.Name);
                    }
                }
            }

            // Save the cleaned PDF (still PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cleaned PDF saved to '{outputPath}'.");
    }
}
