using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string logPath    = "annotations_log.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Demonstrate usage of a Facades class as required
        PdfFileEditor facadesHelper = new PdfFileEditor();

        var annotationInfos = new List<object>();

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate over annotations on the current page
                foreach (Annotation ann in page.Annotations)
                {
                    // Base properties available on all annotations
                    string contents = ann.Contents ?? string.Empty;

                    // Title exists only on markup annotations; cast safely
                    string title = string.Empty;
                    if (ann is MarkupAnnotation markup)
                    {
                        title = markup.Title ?? string.Empty;
                    }

                    // Rectangle coordinates (llx, lly, urx, ury)
                    var rect = ann.Rect;
                    var rectInfo = new
                    {
                        LowerLeftX  = rect.LLX,
                        LowerLeftY  = rect.LLY,
                        UpperRightX = rect.URX,
                        UpperRightY = rect.URY
                    };

                    // Build a simple anonymous object to hold the data
                    var info = new
                    {
                        PageNumber = pageIndex,
                        AnnotationType = ann.GetType().Name,
                        Title = title,
                        Contents = contents,
                        Rectangle = rectInfo
                    };

                    annotationInfos.Add(info);
                }
            }
        }

        // Serialize the collected annotation information to indented JSON
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(annotationInfos, jsonOptions);

        // Write JSON to the log file (overwrites if it already exists)
        try
        {
            File.WriteAllText(logPath, json);
            Console.WriteLine($"Annotation data written to '{logPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write log file: {ex.Message}");
        }
    }
}