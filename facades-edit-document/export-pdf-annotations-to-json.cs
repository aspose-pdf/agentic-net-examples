using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

public class AnnotationInfo
{
    public int PageNumber { get; set; }
    public string? Subtype { get; set; }
    public double LLX { get; set; }
    public double LLY { get; set; }
    public double URX { get; set; }
    public double URY { get; set; }
    public string? Contents { get; set; }
}

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "annotations.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document using Aspose.Pdf.Document (preferred over PdfAnnotationEditor)
        Document doc = new Document(inputPdf);

        List<AnnotationInfo> allAnnotations = new List<AnnotationInfo>();

        // Pages are 1‑based in Aspose.Pdf
        for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
        {
            Page page = doc.Pages[pageNum];
            foreach (Annotation annot in page.Annotations)
            {
                var info = new AnnotationInfo
                {
                    PageNumber = pageNum,
                    // Use AnnotationType (enum) instead of the non‑existent Subtype property
                    Subtype    = annot.AnnotationType.ToString(),
                    LLX        = annot.Rect?.LLX ?? 0,
                    LLY        = annot.Rect?.LLY ?? 0,
                    URX        = annot.Rect?.URX ?? 0,
                    URY        = annot.Rect?.URY ?? 0,
                    // Contents is defined on the base Annotation class and may be null
                    Contents   = annot.Contents
                };
                allAnnotations.Add(info);
            }
        }

        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(allAnnotations, jsonOptions);
        File.WriteAllText(outputJson, json);
        Console.WriteLine($"Exported {allAnnotations.Count} annotations to '{outputJson}'.");
    }
}
