using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

namespace AnnotationExtractor
{
    // Custom object to hold annotation details
    public class AnnotationInfo
    {
        // Made nullable to satisfy non‑nullable warnings (or could use 'required' in C# 11)
        public string? Name { get; set; }
        public string? Type { get; set; }
        public Aspose.Pdf.Rectangle? Rect { get; set; }
    }

    class Program
    {
        static void Main()
        {
            const string inputPdf = "input.pdf";

            // List to store extracted annotation information
            List<AnnotationInfo> extracted = new List<AnnotationInfo>();

            // Use PdfAnnotationEditor facade to work with annotations
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Load the PDF document into the facade
                editor.BindPdf(inputPdf);

                // Determine page range (Aspose.Pdf uses 1‑based indexing)
                int firstPage = 1;
                int lastPage = editor.Document.Pages.Count;

                // Retrieve all annotation types defined in the enum
                AnnotationType[] allTypes = (AnnotationType[])Enum.GetValues(typeof(AnnotationType));

                // Extract annotations of all types within the page range
                IList<Annotation> annotations = editor.ExtractAnnotations(firstPage, lastPage, allTypes);

                // Populate custom objects with required details
                foreach (Annotation ann in annotations)
                {
                    extracted.Add(new AnnotationInfo
                    {
                        Name = ann.Name,                     // may be null – handled by nullable property
                        Type = ann.AnnotationType.ToString(),
                        Rect = ann.Rect                       // may be null – handled by nullable property
                    });
                }
            }

            // Example output: display extracted information
            foreach (AnnotationInfo info in extracted)
            {
                // Guard against possible nulls when printing
                string name = info.Name ?? "<no name>";
                string type = info.Type ?? "<no type>";
                string rect = info.Rect != null
                    ? $"[{info.Rect.LLX}, {info.Rect.LLY}, {info.Rect.URX}, {info.Rect.URY}]"
                    : "<no rect>";

                Console.WriteLine($"Name: {name}, Type: {type}, Rect: {rect}");
            }
        }
    }
}
