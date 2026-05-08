using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

namespace AnnotationExtractionDemo
{
    // Custom DTO to hold annotation details
    public class AnnotationInfo
    {
        public string Name { get; set; }               // Annotation name (may be null/empty)
        public string Type { get; set; }               // Annotation type as string (e.g., "Text", "Link")
        public Aspose.Pdf.Rectangle Rectangle { get; set; } // Position and size on the page
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

            // Use PdfAnnotationEditor (Facade) to work with annotations
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Load the PDF document
                editor.BindPdf(inputPdf);

                // Access the underlying Document object
                Document doc = editor.Document;

                // Collect annotation details
                List<AnnotationInfo> annotations = new List<AnnotationInfo>();

                // Iterate through all pages (1‑based indexing)
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Iterate through all annotations on the current page (1‑based)
                    for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                    {
                        Annotation ann = page.Annotations[annIndex];

                        // Populate the custom DTO
                        AnnotationInfo info = new AnnotationInfo
                        {
                            Name = ann.Name ?? string.Empty,
                            Type = ann.AnnotationType.ToString(),
                            Rectangle = ann.Rect // Aspose.Pdf.Rectangle
                        };

                        annotations.Add(info);
                    }
                }

                // Example usage: output collected data to console
                foreach (var info in annotations)
                {
                    Console.WriteLine($"Name: {info.Name}, Type: {info.Type}, Rect: [{info.Rectangle.LLX}, {info.Rectangle.LLY}, {info.Rectangle.URX}, {info.Rectangle.URY}]");
                }
            }
        }
    }
}