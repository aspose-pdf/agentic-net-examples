using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class AnnotationInfo
{
    public int PageNumber { get; set; }
    public string? AnnotationName { get; set; }
    public string? AnnotationType { get; set; }
    public string? Title { get; set; }
    public string? Contents { get; set; }
    public string? Color { get; set; }
    public bool Open { get; set; }
}

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string logPath = "annotations_log.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Initialize the facade and bind the PDF document
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPdf);               // Load PDF into the facade
                Document doc = editor.Document;         // Access underlying Document

                var annotations = new List<AnnotationInfo>();

                // Iterate through pages (1‑based indexing)
                for (int pageIdx = 1; pageIdx <= doc.Pages.Count; pageIdx++)
                {
                    Page page = doc.Pages[pageIdx];

                    // Iterate through annotations on the page (1‑based)
                    for (int annIdx = 1; annIdx <= page.Annotations.Count; annIdx++)
                    {
                        Annotation ann = page.Annotations[annIdx];

                        // Determine the Open flag only for annotation types that expose it
                        bool isOpen = false;
                        if (ann is TextAnnotation textAnn)
                            isOpen = textAnn.Open;
                        else if (ann is PopupAnnotation popupAnn)
                            isOpen = popupAnn.Open;

                        AnnotationInfo info = new AnnotationInfo
                        {
                            PageNumber = pageIdx,
                            AnnotationName = ann.Name,
                            AnnotationType = ann.GetType().Name,
                            Title = ann is MarkupAnnotation ma ? ma.Title : null,
                            Contents = ann.Contents,
                            Color = ann.Color?.ToString(),
                            Open = isOpen
                        };

                        annotations.Add(info);
                    }
                }

                // Serialize the collected data to indented JSON
                JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(annotations, jsonOptions);

                // Write JSON to the audit log file
                File.WriteAllText(logPath, json);
            }

            Console.WriteLine($"Annotations exported to JSON log: {logPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
