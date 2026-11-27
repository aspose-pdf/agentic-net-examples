using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    // Simple DTO for JSON output
    private class StampInfo
    {
        public string Text { get; set; }
    }

    static void Main()
    {
        const string inputPath = "FormF.pdf";
        const string outputPath = "FormF_page3Stamp.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Aspose.Pdf uses 1‑based page indexing
                if (doc.Pages.Count < 3)
                {
                    Console.Error.WriteLine("The document has fewer than 3 pages.");
                    return;
                }

                Page page = doc.Pages[3];
                StampAnnotation stamp = null;

                // Locate the first StampAnnotation on page 3
                foreach (Annotation annot in page.Annotations)
                {
                    if (annot is StampAnnotation sa)
                    {
                        stamp = sa;
                        break;
                    }
                }

                if (stamp == null)
                {
                    Console.Error.WriteLine("No StampAnnotation found on page 3.");
                    return;
                }

                // Extract the textual content of the stamp.
                // The 'Contents' property holds the annotation's text.
                string stampText = stamp.Contents ?? string.Empty;

                // Prepare JSON payload
                StampInfo info = new StampInfo { Text = stampText };
                string json = JsonSerializer.Serialize(info, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to file
                File.WriteAllText(outputPath, json);
                Console.WriteLine($"Stamp text extracted and saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}