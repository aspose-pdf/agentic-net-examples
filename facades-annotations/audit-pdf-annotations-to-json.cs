using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string logJson   = "annotations_audit_log.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Collect annotation information in a list of anonymous objects
        var annotationInfos = new List<object>();

        // Use the PdfAnnotationEditor facade to work with annotations
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the PDF document to the editor
            editor.BindPdf(inputPdf);

            // Access the underlying Document for page enumeration (1‑based indexing)
            Document doc = editor.Document;

            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Iterate over all annotations on the current page
                foreach (Annotation ann in page.Annotations)
                {
                    // Gather common properties; cast to specific types when needed
                    var info = new
                    {
                        Page     = pageNum,
                        Type     = ann.GetType().Name,
                        Title    = (ann is MarkupAnnotation markup) ? markup.Title : null,
                        Contents = ann.Contents,
                        Color    = ann.Color?.ToString(),
                        // The Open flag exists only on TextAnnotation and PopupAnnotation
                        Open = ann is TextAnnotation textAnn ? (bool?)textAnn.Open :
                               ann is PopupAnnotation popupAnn ? (bool?)popupAnn.Open :
                               (bool?)null
                    };

                    annotationInfos.Add(info);
                }
            }

            // The using block disposes the editor (calls Close internally)
        }

        // Serialize the collected annotation data to indented JSON
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(annotationInfos, jsonOptions);

        // Write the JSON to the audit log file
        File.WriteAllText(logJson, json);

        Console.WriteLine($"Annotation audit log written to '{logJson}'.");
    }
}
