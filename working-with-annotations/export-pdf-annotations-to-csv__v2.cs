using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputCsvPath = "annotations_audit.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the CSV file is created fresh
        using (StreamWriter writer = new StreamWriter(outputCsvPath, false, System.Text.Encoding.UTF8))
        {
            // Write CSV header
            writer.WriteLine("Page,Index,Type,Rect,Contents,Title,Color");

            // Load the PDF document inside a using block for proper disposal
            using (Document doc = new Document(inputPdfPath))
            {
                // Iterate over all pages (1‑based indexing)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    Page page = doc.Pages[pageNum];
                    // Annotation collection also uses 1‑based indexing
                    for (int annIdx = 1; annIdx <= page.Annotations.Count; annIdx++)
                    {
                        Annotation ann = page.Annotations[annIdx];

                        // Basic details
                        string typeName = ann.GetType().Name;
                        Aspose.Pdf.Rectangle rect = ann.Rect;
                        string rectStr = $"{rect.LLX},{rect.LLY},{rect.URX},{rect.URY}";
                        string contents = ann.Contents?.Replace("\"", "\"\"") ?? string.Empty;

                        // Title is only available on markup annotations
                        string title = string.Empty;
                        if (ann is MarkupAnnotation markup)
                        {
                            title = markup.Title?.Replace("\"", "\"\"") ?? string.Empty;
                        }

                        // Color may be null
                        string color = ann.Color?.ToString() ?? string.Empty;

                        // Escape fields that may contain commas or quotes
                        string Escape(string s) => $"\"{s}\"";

                        writer.WriteLine($"{pageNum},{annIdx},{Escape(typeName)},{Escape(rectStr)},{Escape(contents)},{Escape(title)},{Escape(color)}");
                    }
                }
            }
        }

        Console.WriteLine($"Annotation audit CSV saved to '{outputCsvPath}'.");
    }
}