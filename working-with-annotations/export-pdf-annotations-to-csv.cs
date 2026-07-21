using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "processed.pdf";
        const string csvPath = "annotations.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (load rule)
        using (Document doc = new Document(inputPdfPath))
        {
            // Prepare CSV file for writing
            using (StreamWriter csvWriter = new StreamWriter(csvPath, false))
            {
                // CSV header
                csvWriter.WriteLine("Page,Type,Rect,Contents,Title");

                // Iterate over all pages (1‑based indexing)
                for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
                {
                    Page page = doc.Pages[pageNumber];

                    // Iterate over all annotations on the current page
                    foreach (Annotation annotation in page.Annotations)
                    {
                        // Basic details
                        string typeName = annotation.GetType().Name;
                        string rect = $"{annotation.Rect.LLX},{annotation.Rect.LLY},{annotation.Rect.URX},{annotation.Rect.URY}";
                        string contents = annotation.Contents?.Replace("\"", "\"\"") ?? string.Empty;

                        // Title is available only on markup annotations
                        string title = string.Empty;
                        if (annotation is MarkupAnnotation markup)
                        {
                            title = markup.Title?.Replace("\"", "\"\"") ?? string.Empty;
                        }

                        // Write a CSV line (values are quoted to handle commas)
                        csvWriter.WriteLine($"{pageNumber},\"{typeName}\",\"{rect}\",\"{contents}\",\"{title}\"");
                    }
                }
            }

            // Save the (unchanged) document (save rule)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotation audit CSV saved to '{csvPath}'.");
        Console.WriteLine($"Processed PDF saved to '{outputPdfPath}'.");
    }
}