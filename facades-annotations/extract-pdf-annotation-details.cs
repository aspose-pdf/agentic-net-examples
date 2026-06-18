using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

namespace AnnotationExtraction
{
    // Custom DTO to hold annotation details
    public class AnnotationInfo
    {
        // Initialise with safe defaults to satisfy non‑nullable warnings
        public string Name { get; set; } = string.Empty;               // Annotation name (may be empty)
        public string Type { get; set; } = string.Empty;               // Annotation type as string
        // Provide a default rectangle with explicit coordinates (0,0,0,0)
        public Aspose.Pdf.Rectangle Rect { get; set; } = new Aspose.Pdf.Rectangle(0, 0, 0, 0); // Annotation rectangle (coordinates)
    }

    class Program
    {
        static void Main()
        {
            const string inputPdf = "input.pdf";

            // Ensure the source file exists
            if (!System.IO.File.Exists(inputPdf))
            {
                Console.Error.WriteLine($"File not found: {inputPdf}");
                return;
            }

            // List to store extracted annotation information
            List<AnnotationInfo> extracted = new List<AnnotationInfo>();

            // Use PdfAnnotationEditor facade to work with annotations
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the PDF document to the facade
                editor.BindPdf(inputPdf);

                // Get total number of pages from the underlying Document
                int pageCount = editor.Document.Pages.Count;

                // Extract all annotations from all pages (empty type array = all types)
                IList<Annotation> annotations = editor.ExtractAnnotations(1, pageCount, new AnnotationType[0]);

                // Iterate through each annotation and map required properties
                foreach (Annotation ann in annotations)
                {
                    // Some annotations may not have a name; handle null safely
                    string name = ann.Name ?? string.Empty;

                    // AnnotationType is an enum; convert to its name for readability
                    string type = ann.AnnotationType.ToString();

                    // Rectangle may be null; guard against it using a default rectangle with explicit coordinates
                    Aspose.Pdf.Rectangle rect = ann.Rect ?? new Aspose.Pdf.Rectangle(0, 0, 0, 0);

                    extracted.Add(new AnnotationInfo
                    {
                        Name = name,
                        Type = type,
                        Rect = rect
                    });
                }
            }

            // Output the collected information (example: console)
            foreach (var info in extracted)
            {
                Console.WriteLine($"Name: {info.Name}, Type: {info.Type}, Rect: [{info.Rect.LLX}, {info.Rect.LLY}, {info.Rect.URX}, {info.Rect.URY}]");
            }
        }
    }
}
