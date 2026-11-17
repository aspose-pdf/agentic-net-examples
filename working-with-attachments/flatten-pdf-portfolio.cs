using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "portfolio.pdf";
        const string outputPath = "flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF portfolio
            using (Document doc = new Document(inputPath))
            {
                // Remove the interactive collection (portfolio) if it exists
                if (doc.Collection != null)
                {
                    doc.Collection = null;
                }

                // Remove any embedded files that were part of the portfolio
                if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
                {
                    // EmbeddedFileCollection uses 1‑based indexing; delete each file by name
                    for (int i = doc.EmbeddedFiles.Count; i >= 1; i--)
                    {
                        var fileSpec = doc.EmbeddedFiles[i];
                        if (fileSpec != null && !string.IsNullOrEmpty(fileSpec.Name))
                        {
                            doc.EmbeddedFiles.Delete(fileSpec.Name);
                        }
                    }
                }

                // Save as a regular PDF (no portfolio features)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Portfolio flattened to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
