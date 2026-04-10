using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string jsonLogPath = "annotations_log.json"; // audit log

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document inside a using block)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Initialize the annotation editor facade on the loaded document
            using (PdfAnnotationEditor annotEditor = new PdfAnnotationEditor(pdfDoc))
            {
                // Extract all annotations from the whole document.
                // The overload with (int, int, AnnotationType[]) allows specifying page range.
                // Cast null to AnnotationType[] to avoid ambiguity with the string[] overload.
                IList<Annotation> annotations = annotEditor.ExtractAnnotations(
                    1,
                    pdfDoc.Pages.Count,
                    (AnnotationType[])null);

                // Prepare a simple DTO list to hold the properties we want to audit.
                var auditItems = new List<object>();

                foreach (Annotation ann in annotations)
                {
                    // Cast to MarkupAnnotation when markup‑specific properties are needed.
                    var markup = ann as MarkupAnnotation;

                    // The Open flag is only available on TextAnnotation and PopupAnnotation.
                    bool? openFlag = (ann as TextAnnotation)?.Open ?? (ann as PopupAnnotation)?.Open;

                    var item = new
                    {
                        // Title, Subject and Open exist only on markup annotations.
                        Title = markup?.Title,
                        ann.Contents,
                        Subject = markup?.Subject,
                        ann.Modified,
                        Open = openFlag,
                        Color = ann.Color?.ToString(),
                        // Rectangle coordinates (if available)
                        Rect = ann.Rect == null ? null : new
                        {
                            Llx = ann.Rect.LLX,
                            Lly = ann.Rect.LLY,
                            Urx = ann.Rect.URX,
                            Ury = ann.Rect.URY
                        },
                        ann.Name,
                        // Page number is not directly exposed; infer from the annotation's page reference if needed.
                        // Here we store the index of the page collection where the annotation resides.
                        PageNumber = GetAnnotationPageNumber(pdfDoc, ann)
                    };

                    auditItems.Add(item);
                }

                // Serialize the list to indented JSON.
                JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(auditItems, jsonOptions);

                // Write the JSON to the audit log file.
                File.WriteAllText(jsonLogPath, json);
                Console.WriteLine($"Annotation audit log written to '{jsonLogPath}'.");
            }
        }
    }

    // Helper method to determine on which page an annotation resides.
    // This scans the document pages and returns the first page index (1‑based) that contains the annotation.
    private static int GetAnnotationPageNumber(Document doc, Annotation target)
    {
        for (int i = 1; i <= doc.Pages.Count; i++)
        {
            Page page = doc.Pages[i];
            foreach (Annotation ann in page.Annotations)
            {
                if (ReferenceEquals(ann, target))
                    return i;
            }
        }
        return -1; // not found
    }
}
