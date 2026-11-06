using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

// Custom class to hold extracted annotation details
public class AnnotationInfoDto
{
    // Annotation name (if any). Made nullable because some annotations may not have a name.
    public string? Name { get; set; }

    // Annotation type as string. Marked as required so the compiler knows it will be set via object initializer.
    public required string Type { get; set; }

    // Bounding rectangle (Aspose.Pdf.Rectangle)
    public Aspose.Pdf.Rectangle Rect { get; set; } = null!; // Initialized to non‑null to satisfy the compiler; will be set in the initializer.
}

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

        // List to collect all annotation details
        List<AnnotationInfoDto> extractedAnnotations = new List<AnnotationInfoDto>();

        // Load the PDF document directly (no PdfAnnotationEditor needed)
        Document pdfDoc = new Document(inputPath);

        // Iterate through each page and extract annotations
        foreach (Page page in pdfDoc.Pages)
        {
            // Page.Annotations returns a collection of Annotation objects
            foreach (Annotation annotation in page.Annotations)
            {
                // Populate the DTO with required fields
                AnnotationInfoDto dto = new AnnotationInfoDto
                {
                    Name = annotation.Name,                     // May be null, handled by nullable property
                    Type = annotation.AnnotationType.ToString(), // Enum -> string representation
                    Rect = annotation.Rect
                };

                extractedAnnotations.Add(dto);
            }
        }

        // Example output: list all extracted annotations
        Console.WriteLine("Extracted Annotations:");
        foreach (var ann in extractedAnnotations)
        {
            Console.WriteLine($"Name: {ann.Name ?? "<none>"}, Type: {ann.Type}, Rect: [{ann.Rect.LLX}, {ann.Rect.LLY}, {ann.Rect.URX}, {ann.Rect.URY}]");
        }
    }
}
