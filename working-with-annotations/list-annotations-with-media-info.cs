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

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                AnnotationCollection annotations = page.Annotations;

                // Iterate over each annotation on the page
                foreach (Annotation annotation in annotations)
                {
                    // Annotation type (enum or class name)
                    string typeName = annotation.AnnotationType.ToString();

                    // Rectangle coordinates
                    Rectangle rect = annotation.Rect;
                    string rectInfo = $"LLX={rect.LLX}, LLY={rect.LLY}, URX={rect.URX}, URY={rect.URY}";

                    // Media file name (if applicable)
                    string mediaFile = "N/A";

                    // Some annotation types embed external media (e.g., ScreenAnnotation, MovieAnnotation, RichMediaAnnotation)
                    // Attempt to retrieve a known property; if not present, keep "N/A"
                    switch (annotation)
                    {
                        case ScreenAnnotation screen:
                            // ScreenAnnotation stores the media file path in the 'File' property (if available)
                            // Use reflection as a safety net in case the property name differs
                            var fileProp = typeof(ScreenAnnotation).GetProperty("File");
                            if (fileProp != null)
                            {
                                var value = fileProp.GetValue(screen) as string;
                                if (!string.IsNullOrEmpty(value))
                                    mediaFile = value;
                            }
                            break;

                        case MovieAnnotation movie:
                            var movieFileProp = typeof(MovieAnnotation).GetProperty("File");
                            if (movieFileProp != null)
                            {
                                var value = movieFileProp.GetValue(movie) as string;
                                if (!string.IsNullOrEmpty(value))
                                    mediaFile = value;
                            }
                            break;

                        case RichMediaAnnotation rich:
                            var richFileProp = typeof(RichMediaAnnotation).GetProperty("File");
                            if (richFileProp != null)
                            {
                                var value = richFileProp.GetValue(rich) as string;
                                if (!string.IsNullOrEmpty(value))
                                    mediaFile = value;
                            }
                            break;

                        // Add other media‑bearing annotation types here if needed
                    }

                    // Log the information
                    Console.WriteLine($"Page {pageIndex}: Type={typeName}, Rect=[{rectInfo}], MediaFile={mediaFile}");
                }
            }
        }
    }
}