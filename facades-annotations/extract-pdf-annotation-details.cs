using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Initialize the annotation editor with the loaded document
            PdfAnnotationEditor editor = new PdfAnnotationEditor(doc);

            // Extract all annotations from all pages. Use Array.Empty<AnnotationType>()
            // to avoid passing a null literal to a non‑nullable parameter.
            IList<Annotation> annotations = editor.ExtractAnnotations(
                1,
                doc.Pages.Count,
                Array.Empty<AnnotationType>());

            // Convert to custom DTO objects
            List<AnnotationInfo> infoList = new List<AnnotationInfo>();
            foreach (Annotation ann in annotations)
            {
                infoList.Add(new AnnotationInfo
                {
                    Name = ann.Name,
                    Type = ann.AnnotationType.ToString(),
                    Rectangle = ann.Rect
                });
            }

            // Output the extracted details (example usage)
            foreach (AnnotationInfo info in infoList)
            {
                Console.WriteLine(
                    $"Name: {info.Name}, Type: {info.Type}, Rect: [{info.Rectangle.LLX}, {info.Rectangle.LLY}, {info.Rectangle.URX}, {info.Rectangle.URY}]");
            }
        }
    }
}

// Custom data transfer object to hold annotation details
public class AnnotationInfo
{
    // Made nullable to satisfy the non‑nullable warnings when the object is
    // instantiated without setting the properties via a constructor.
    public string? Name { get; set; }
    public string? Type { get; set; }
    public Aspose.Pdf.Rectangle? Rectangle { get; set; }
}
