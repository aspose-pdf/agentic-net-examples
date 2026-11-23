using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "watermarks.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdfPath))
            {
                // Aspose.Pdf uses 1‑based page indexing
                Aspose.Pdf.Page page = doc.Pages[1];

                // Collect all watermark texts from the first page
                List<string> watermarkTexts = new List<string>();
                foreach (Aspose.Pdf.Artifact artifact in page.Artifacts)
                {
                    if (artifact is Aspose.Pdf.WatermarkArtifact wm)
                    {
                        // WatermarkArtifact.Text holds the watermark string
                        watermarkTexts.Add(wm.Text ?? string.Empty);
                    }
                }

                // Write the collected texts to a CSV file
                using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
                {
                    // CSV header
                    writer.WriteLine("WatermarkText");

                    foreach (string text in watermarkTexts)
                    {
                        // Escape double quotes for CSV compliance
                        string escaped = text.Replace("\"", "\"\"");
                        writer.WriteLine($"\"{escaped}\"");
                    }
                }

                Console.WriteLine($"Extracted {watermarkTexts.Count} watermark(s) to '{outputCsvPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}