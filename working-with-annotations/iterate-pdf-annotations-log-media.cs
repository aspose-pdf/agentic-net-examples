using System;
using System.IO;
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

        // Load the PDF document (no special load options required)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate over each annotation on the current page
                foreach (Annotation annotation in page.Annotations)
                {
                    // Basic information: type and rectangle
                    string typeName = annotation.AnnotationType.ToString();
                    Aspose.Pdf.Rectangle rect = annotation.Rect;

                    // Try to obtain an associated media file name, if any
                    string mediaFile = GetMediaFileName(annotation);

                    // Log the details
                    Console.WriteLine(
                        $"Page {pageIndex}: Type={typeName}, Rect=[{rect.LLX},{rect.LLY},{rect.URX},{rect.URY}], MediaFile={mediaFile}");
                }
            }
        }
    }

    /// <summary>
    /// Returns the media file name associated with an annotation, if applicable.
    /// Supports FileAttachment, Movie annotations. Returns an empty string when no media file is associated.
    /// </summary>
    private static string GetMediaFileName(Annotation annotation)
    {
        switch (annotation)
        {
            case FileAttachmentAnnotation fileAtt:
                // FileAttachmentAnnotation stores the attached file as a FileSpecification.
                // The file name is available via the Name property.
                return fileAtt.File?.Name ?? string.Empty;

            case MovieAnnotation movie:
                // MovieAnnotation stores the media file as a FileSpecification.
                return movie.File?.Name ?? string.Empty;

            // ScreenAnnotation and RichMediaAnnotation do not expose a direct file reference in the
            // core Aspose.Pdf API version used here. If needed, additional processing (e.g., extracting
            // assets from RichMediaContent) would require a newer API version. For now we return an empty string.
            case ScreenAnnotation _:
            case RichMediaAnnotation _:
                return string.Empty;

            default:
                // No media file associated with this annotation type
                return string.Empty;
        }
    }
}
