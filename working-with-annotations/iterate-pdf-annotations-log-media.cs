using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                AnnotationCollection annotations = page.Annotations;

                // Iterate over all annotations on the current page
                foreach (Annotation annotation in annotations)
                {
                    // Annotation type (enum value)
                    string typeName = annotation.AnnotationType.ToString();

                    // Rectangle coordinates (llx, lly, urx, ury)
                    Aspose.Pdf.Rectangle rect = annotation.Rect;
                    string rectInfo = $"[{rect.LLX}, {rect.LLY}, {rect.URX}, {rect.URY}]";

                    // Retrieve an associated media file name, if applicable
                    string mediaFile = GetMediaFileName(annotation);

                    Console.WriteLine($"Page {pageIndex}: Type={typeName}, Rect={rectInfo}, MediaFile={mediaFile}");
                }
            }
        }
    }

    // Helper method to extract a media file name from known annotation types
    private static string GetMediaFileName(Annotation annotation)
    {
        // ScreenAnnotation (e.g., video/audio)
        if (annotation is ScreenAnnotation screen)
        {
            // The File property may not be directly exposed in some versions; use reflection as a fallback
            var prop = typeof(ScreenAnnotation).GetProperty("File");
            if (prop != null)
            {
                var fileSpec = prop.GetValue(screen) as FileSpecification;
                return fileSpec?.Name ?? "(none)";
            }
            return "(none)";
        }

        // MovieAnnotation (legacy movie annotation)
        if (annotation is MovieAnnotation movie)
        {
            var prop = typeof(MovieAnnotation).GetProperty("File");
            if (prop != null)
            {
                var fileSpec = prop.GetValue(movie) as FileSpecification;
                return fileSpec?.Name ?? "(none)";
            }
            return "(none)";
        }

        // FileAttachmentAnnotation (embedded file)
        if (annotation is FileAttachmentAnnotation fileAttach)
        {
            var fileSpec = fileAttach.File;
            return fileSpec?.Name ?? "(none)";
        }

        // RichMediaAnnotation (rich media)
        if (annotation is RichMediaAnnotation richMedia)
        {
            // Some versions expose a File property; fall back to reflection if needed
            var prop = typeof(RichMediaAnnotation).GetProperty("File");
            if (prop != null)
            {
                var fileSpec = prop.GetValue(richMedia) as FileSpecification;
                return fileSpec?.Name ?? "(none)";
            }
        }

        // No known media file associated
        return "(none)";
    }
}
