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

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                int annotationIndex = 1;

                // Iterate over each annotation on the current page
                foreach (Annotation annotation in page.Annotations)
                {
                    // Annotation type (enum)
                    AnnotationType type = annotation.AnnotationType;

                    // Rectangle defining the annotation's position
                    Aspose.Pdf.Rectangle rect = annotation.Rect;

                    // Retrieve an associated media file name, if applicable
                    string mediaFile = GetMediaFileName(annotation);

                    // Log the details
                    Console.WriteLine(
                        $"Page {page.Number}, Annotation {annotationIndex}: " +
                        $"Type={type}, " +
                        $"Rect=[{rect.LLX}, {rect.LLY}, {rect.URX}, {rect.URY}], " +
                        $"MediaFile={(mediaFile ?? "N/A")}" );

                    annotationIndex++;
                }
            }
        }
    }

    /// <summary>
    /// Attempts to extract the file name associated with an annotation that can carry a file.
    /// Uses reflection to avoid compile‑time dependencies on version‑specific properties such as
    /// ScreenAnnotation.File, MovieAnnotation.File, RichMediaAnnotation.File, etc.
    /// </summary>
    private static string GetMediaFileName(Annotation annotation)
    {
        // Most file‑carrying annotations expose a property named "File" of type FileSpecification.
        PropertyInfo fileProp = annotation.GetType().GetProperty("File", BindingFlags.Public | BindingFlags.Instance);
        if (fileProp != null)
        {
            var fileSpec = fileProp.GetValue(annotation) as FileSpecification;
            return fileSpec?.Name; // Name holds the original file name.
        }
        return null;
    }
}
