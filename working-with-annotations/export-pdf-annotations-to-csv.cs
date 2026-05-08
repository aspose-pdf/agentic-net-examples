using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string csvPath   = "annotations.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF and open a writer for the CSV
            using (Document doc = new Document(inputPath))
            using (StreamWriter writer = new StreamWriter(csvPath, false))
            {
                // CSV header
                writer.WriteLine("PageNumber,AnnotationType,Rect,Title,Contents");

                // Pages are 1‑based
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Iterate all annotations on the page
                    foreach (Annotation ann in page.Annotations)
                    {
                        string typeName = ann.GetType().Name;

                        // Rectangle coordinates (lower‑left x/y, upper‑right x/y)
                        string rect = $"{ann.Rect.LLX},{ann.Rect.LLY},{ann.Rect.URX},{ann.Rect.URY}";

                        // Title is only available on markup annotations
                        string title = "";
                        if (ann is MarkupAnnotation markup)
                        {
                            title = markup.Title?.Replace("\"", "\"\"") ?? "";
                        }

                        // Contents may be null
                        string contents = ann.Contents?.Replace("\"", "\"\"") ?? "";

                        // Write a CSV line, quoting fields that may contain commas
                        writer.WriteLine($"{i},\"{typeName}\",\"{rect}\",\"{title}\",\"{contents}\"");
                    }
                }
            }

            Console.WriteLine($"Annotations exported to '{csvPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}