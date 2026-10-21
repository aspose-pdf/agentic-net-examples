using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

namespace PdfAnnotationExtractor
{
    // Custom object to hold annotation details
    public class AnnotationDetail
    {
        // Made nullable to satisfy the non‑nullable warnings when the object is created without a constructor
        public string? Name { get; set; }
        public string? Type { get; set; }
        public Aspose.Pdf.Rectangle? Rect { get; set; }
    }

    class Program
    {
        static void Main()
        {
            const string inputPath = "input.pdf";

            if (!System.IO.File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            // Load the PDF document
            Document pdfDoc = new Document(inputPath);

            List<AnnotationDetail> details = new List<AnnotationDetail>();

            // Iterate through each page and collect its annotations
            foreach (Page page in pdfDoc.Pages)
            {
                foreach (Annotation annotation in page.Annotations)
                {
                    // Most annotation types expose a Name property; TextAnnotation also has Title
                    string name = string.Empty;
                    if (!string.IsNullOrEmpty(annotation.Name))
                    {
                        name = annotation.Name;
                    }
                    else if (annotation is TextAnnotation textAnn && !string.IsNullOrEmpty(textAnn.Title))
                    {
                        name = textAnn.Title;
                    }

                    // Annotation type as string
                    string type = annotation.AnnotationType.ToString();

                    // Rectangle defining the annotation bounds (fully‑qualified type)
                    Aspose.Pdf.Rectangle rect = annotation.Rect;

                    details.Add(new AnnotationDetail
                    {
                        Name = name,
                        Type = type,
                        Rect = rect
                    });
                }
            }

            // Example output for analysis
            Console.WriteLine($"Found {details.Count} annotation(s):");
            foreach (var d in details)
            {
                // Guard against possible nulls (should not happen after the above handling)
                string name = d.Name ?? "<none>";
                string type = d.Type ?? "<unknown>";
                // Use a zero‑size rectangle as a safe fallback when Rect is null
                var rect = d.Rect ?? new Aspose.Pdf.Rectangle(0, 0, 0, 0);
                Console.WriteLine($"Name: {name}, Type: {type}, Rect: [{rect.LLX}, {rect.LLY}, {rect.URX}, {rect.URY}]");
            }
        }
    }
}
