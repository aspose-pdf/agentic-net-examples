using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputCsv = "annotations.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPdf))
        {
            // Create CSV file for output (lifecycle: create)
            using (StreamWriter writer = new StreamWriter(outputCsv))
            {
                // Write CSV header
                writer.WriteLine("PageNumber,AnnotationType,Rect,Contents,Title,Color");

                // Iterate through pages (1‑based indexing)
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Iterate through annotations on the page (1‑based indexing)
                    for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                    {
                        Annotation ann = page.Annotations[annIndex];
                        string typeName = ann.GetType().Name;

                        // Rectangle coordinates (fully qualified to avoid ambiguity)
                        Aspose.Pdf.Rectangle rect = ann.Rect;
                        string rectStr = $"{rect.LLX},{rect.LLY},{rect.URX},{rect.URY}";

                        // Contents may be null; escape double quotes for CSV
                        string contents = ann.Contents?.Replace("\"", "\"\"") ?? "";

                        // Title is available only on markup annotations
                        string title = "";
                        if (ann is MarkupAnnotation markup)
                        {
                            title = markup.Title?.Replace("\"", "\"\"") ?? "";
                        }

                        // Color may be null; use its string representation
                        string color = ann.Color?.ToString() ?? "";

                        // Write a CSV line, quoting fields that may contain commas
                        writer.WriteLine($"{pageIndex},\"{typeName}\",\"{rectStr}\",\"{contents}\",\"{title}\",\"{color}\"");
                    }
                }
            }
        }

        Console.WriteLine($"Annotations exported to '{outputCsv}'.");
    }
}