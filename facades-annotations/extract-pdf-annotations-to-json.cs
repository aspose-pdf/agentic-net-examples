using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    // DTO to hold annotation data that will be serialized to JSON
    private class AnnotationInfo
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Title { get; set; }
        public string? Contents { get; set; }
        public DateTime? Modified { get; set; }          // Correct type – Annotation.Modified is DateTime
        public string? Color { get; set; }               // stored as a string representation
        public string? Subject { get; set; }             // only available on markup annotations
        public bool? Open { get; set; }                  // only available on popup annotations
    }

    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string jsonLogPath  = "annotations_log.json"; // audit log file

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using for deterministic disposal)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Initialize the annotation editor facade on the loaded document
                using (PdfAnnotationEditor annotEditor = new PdfAnnotationEditor(pdfDoc))
                {
                    // Extract all annotations from the whole document.
                    AnnotationType[] allTypes = new AnnotationType[]
                    {
                        AnnotationType.Text,
                        AnnotationType.Link,
                        AnnotationType.Highlight,
                        AnnotationType.Square,
                        AnnotationType.Circle,
                        AnnotationType.Ink,
                        AnnotationType.FreeText,
                        AnnotationType.Line,
                        AnnotationType.Polygon,
                        AnnotationType.PolyLine,
                        AnnotationType.Popup,
                        AnnotationType.FileAttachment,
                        AnnotationType.Sound,
                        AnnotationType.Movie,
                        AnnotationType.Screen,
                        AnnotationType.PrinterMark,
                        AnnotationType.TrapNet,
                        AnnotationType.Watermark,
                        AnnotationType.Widget
                    };

                    // ExtractAnnotations returns a list of Annotation objects.
                    IList<Annotation> annotations = annotEditor.ExtractAnnotations(1, pdfDoc.Pages.Count, allTypes);

                    // Transform each Annotation into a serializable DTO.
                    List<AnnotationInfo> infoList = new List<AnnotationInfo>();
                    foreach (Annotation ann in annotations)
                    {
                        // Convert Aspose.Pdf.Color to a readable string (e.g., "RGB(255,0,0)")
                        string? colorString = ann.Color != null ? ann.Color.ToString() : null;

                        // Title is only available on markup annotations; cast safely.
                        string? title = null;
                        string? subject = null;
                        bool? open = null;

                        if (ann is MarkupAnnotation markupAnn)
                        {
                            title = markupAnn.Title;
                            subject = markupAnn.Subject; // MarkupAnnotation provides Subject (author)
                        }

                        if (ann is PopupAnnotation popupAnn)
                        {
                            open = popupAnn.Open; // PopupAnnotation provides Open flag
                        }

                        infoList.Add(new AnnotationInfo
                        {
                            Name     = ann.Name,
                            Type     = ann.AnnotationType.ToString(),
                            Title    = title,
                            Contents = ann.Contents,
                            Modified = ann.Modified, // DateTime?
                            Color    = colorString,
                            Subject  = subject,
                            Open     = open
                        });
                    }

                    // Serialize the list to pretty‑printed JSON.
                    JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                    string json = JsonSerializer.Serialize(infoList, jsonOptions);

                    // Write the JSON audit log to the specified file.
                    File.WriteAllText(jsonLogPath, json);
                }
            }

            Console.WriteLine($"Annotation audit log written to '{jsonLogPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
