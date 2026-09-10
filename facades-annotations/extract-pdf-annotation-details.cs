using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

namespace AnnotationExtractionDemo
{
    // Simple DTO to hold extracted annotation details
    public class AnnotationInfo
    {
        public string Name { get; set; }               // Annotation name (may be empty)
        public string Type { get; set; }               // Annotation type as string
        public Aspose.Pdf.Rectangle Rect { get; set; } // Annotation rectangle (coordinates)
    }

    class Program
    {
        static void Main()
        {
            const string inputPdf = "input.pdf";

            // Ensure the source PDF exists
            if (!System.IO.File.Exists(inputPdf))
            {
                Console.Error.WriteLine($"File not found: {inputPdf}");
                return;
            }

            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Initialize the PdfAnnotationEditor facade on the loaded document
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor(doc))
                {
                    // Prepare a list to collect all annotation details
                    List<AnnotationInfo> extracted = new List<AnnotationInfo>();

                    // Determine the page range (1‑based indexing)
                    int firstPage = 1;
                    int lastPage  = doc.Pages.Count;

                    // Retrieve all possible annotation types
                    AnnotationType[] allTypes = (AnnotationType[])Enum.GetValues(typeof(AnnotationType));

                    // Extract annotations from the specified page range
                    IList<Annotation> annotations = editor.ExtractAnnotations(firstPage, lastPage, allTypes);

                    // Transform each Aspose.Pdf.Annotation into our DTO
                    foreach (Annotation ann in annotations)
                    {
                        extracted.Add(new AnnotationInfo
                        {
                            Name = ann.Name,
                            Type = ann.AnnotationType.ToString(),
                            Rect = ann.Rect // rectangle is already an Aspose.Pdf.Rectangle
                        });
                    }

                    // Example usage: print extracted information to console
                    foreach (AnnotationInfo info in extracted)
                    {
                        Console.WriteLine($"Name: {info.Name ?? "(none)"} | Type: {info.Type} | Rect: [{info.Rect.LLX}, {info.Rect.LLY}, {info.Rect.URX}, {info.Rect.URY}]");
                    }
                }
            }
        }
    }
}