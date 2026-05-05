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
        const string inputPdfPath = "input.pdf";               // source PDF
        const string auditLogPath = "annotations_log.json";   // JSON audit log

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (load rule)
        using (Document pdfDoc = new Document(inputPdfPath))
        // Initialise the annotation editor (facade)
        using (PdfAnnotationEditor annotEditor = new PdfAnnotationEditor())
        {
            // Bind the editor to the loaded document
            annotEditor.BindPdf(pdfDoc);

            // Retrieve all annotation types defined in the enum
            AnnotationType[] allTypes = (AnnotationType[])Enum.GetValues(typeof(AnnotationType));

            // Extract annotations from the whole document (pages are 1‑based)
            IList<Annotation> annotations = annotEditor.ExtractAnnotations(1, pdfDoc.Pages.Count, allTypes);

            // Prepare a serialisable collection of annotation data
            var annotationData = new List<object>();

            foreach (Annotation ann in annotations)
            {
                // Many properties (Title, Subject, Open) belong to derived annotation types.
                var markup = ann as MarkupAnnotation;

                // The Open flag is only available on TextAnnotation and PopupAnnotation.
                bool? isOpen = null;
                if (ann is TextAnnotation textAnn)
                    isOpen = textAnn.Open;
                else if (ann is PopupAnnotation popupAnn)
                    isOpen = popupAnn.Open;

                var data = new
                {
                    // PageNumber is not exposed on the base Annotation in older Aspose.Pdf versions;
                    // if needed, it can be derived from the extraction context. Here we omit it.
                    Type = ann.AnnotationType.ToString(),
                    Modified = ann.Modified,
                    Title = markup?.Title,
                    Contents = ann.Contents,
                    Color = ann.Color?.ToString(),
                    Subject = markup?.Subject,
                    Open = isOpen
                };

                annotationData.Add(data);
            }

            // Serialise to JSON with indentation for readability
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(annotationData, jsonOptions);

            // Write the JSON audit log to file (save rule)
            File.WriteAllText(auditLogPath, json);
        }

        Console.WriteLine($"Annotation audit log written to '{auditLogPath}'.");
    }
}
