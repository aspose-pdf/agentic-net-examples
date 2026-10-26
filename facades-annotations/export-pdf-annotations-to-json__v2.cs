using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Pdf;                     // Core PDF classes
using Aspose.Pdf.Annotations;        // Annotation classes

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string logPath  = "annotations_log.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document using the high‑level Document API (no facade needed for reading)
        Document pdfDoc = new Document(inputPdf);

        // Prepare a list that will be serialized to JSON
        List<object> annotationList = new List<object>();

        int pageNumber = 1;
        foreach (Page page in pdfDoc.Pages)
        {
            foreach (Annotation ann in page.Annotations)
            {
                // The Rectangle type is Aspose.Pdf.Rectangle – no ambiguity here
                Aspose.Pdf.Rectangle rect = ann.Rect;

                // Some annotation types (e.g., TextAnnotation) expose extra fields like Title/Subject.
                // We safely cast to those types; if the cast fails the values stay null.
                string title   = null;
                string subject = null;
                if (ann is TextAnnotation textAnn)
                {
                    title   = textAnn.Title;
                    subject = textAnn.Subject;
                }

                annotationList.Add(new
                {
                    PageNumber = pageNumber,
                    Type       = ann.AnnotationType.ToString(),
                    Rect = new
                    {
                        LLX = rect.LLX,
                        LLY = rect.LLY,
                        URX = rect.URX,
                        URY = rect.URY
                    },
                    Contents = ann.Contents,
                    Title    = title,
                    Subject  = subject
                });
            }
            pageNumber++;
        }

        // Serialize the list to a pretty‑printed JSON string
        string json = JsonSerializer.Serialize(
            annotationList,
            new JsonSerializerOptions { WriteIndented = true }
        );

        // Write the JSON to the specified log file
        File.WriteAllText(logPath, json);

        Console.WriteLine($"Annotation data has been written to '{logPath}'.");
    }
}
