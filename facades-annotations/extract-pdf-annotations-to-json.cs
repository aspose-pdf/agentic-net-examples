using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string jsonReportPath = "annotations_report.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document.
        Document doc = new Document(inputPdfPath);

        // Prepare a list to hold annotation details.
        var annotationsInfo = new List<object>();

        // Get total number of pages.
        int pageCount = doc.Pages.Count;

        // Iterate through all pages (Aspose.Pdf uses 1‑based indexing).
        for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
        {
            Page page = doc.Pages[pageNumber];
            // Retrieve annotations on the current page.
            var pageAnnotations = page.Annotations;
            if (pageAnnotations == null) continue;

            foreach (Annotation annotation in pageAnnotations)
            {
                // Common rectangle coordinates. Use the correct property name "Rect".
                Aspose.Pdf.Rectangle rect = annotation.Rect;

                // Title is only available on markup annotations.
                string title = null;
                if (annotation is MarkupAnnotation markup)
                {
                    title = markup.Title;
                }

                // Build a simple anonymous object with the needed details.
                var info = new
                {
                    Page = pageNumber,
                    Type = annotation.GetType().Name,
                    AnnotationType = annotation.AnnotationType.ToString(),
                    Title = title,
                    Contents = annotation.Contents,
                    Rectangle = new
                    {
                        LLX = rect.LLX,
                        LLY = rect.LLY,
                        URX = rect.URX,
                        URY = rect.URY
                    }
                };

                annotationsInfo.Add(info);
            }
        }

        // Serialize the list to formatted JSON.
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(annotationsInfo, jsonOptions);

        // Write the JSON report to disk.
        File.WriteAllText(jsonReportPath, json);
        Console.WriteLine($"Annotation report saved to '{jsonReportPath}'.");
    }
}
