using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "annotations.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdfPath))
        // Open a StreamWriter for the CSV output
        using (StreamWriter csvWriter = new StreamWriter(outputCsvPath))
        {
            // Write CSV header
            csvWriter.WriteLine("Page,Index,Type,Name,Title,Contents,Rect");

            // Pages are 1‑based in Aspose.Pdf
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Annotations collection is also 1‑based
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation ann = page.Annotations[annIndex];

                    // Basic details
                    string typeName = ann.GetType().Name;
                    string name = ann.Name ?? string.Empty;
                    string contents = ann.Contents ?? string.Empty;

                    // Title is available on markup annotations
                    string title = string.Empty;
                    if (ann is MarkupAnnotation markup)
                    {
                        title = markup.Title ?? string.Empty;
                    }

                    // Rectangle coordinates (fully qualified to avoid ambiguity)
                    Aspose.Pdf.Rectangle rect = ann.Rect;
                    string rectString = $"{rect.LLX},{rect.LLY},{rect.URX},{rect.URY}";

                    // Write a CSV line, escaping fields that may contain commas or quotes
                    csvWriter.WriteLine($"{pageIndex},{annIndex},{typeName},{EscapeCsv(name)},{EscapeCsv(title)},{EscapeCsv(contents)},{rectString}");
                }
            }
        }

        Console.WriteLine($"Annotation audit CSV saved to '{outputCsvPath}'.");
    }

    // Helper to escape CSV fields according to RFC 4180
    static string EscapeCsv(string value)
    {
        if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
        {
            value = value.Replace("\"", "\"\"");
            return $"\"{value}\"";
        }
        return value;
    }
}