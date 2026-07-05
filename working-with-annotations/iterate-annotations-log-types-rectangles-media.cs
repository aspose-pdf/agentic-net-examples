using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

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

                // Iterate over all annotations on the page (1‑based indexing)
                for (int annIndex = 1; annIndex <= annotations.Count; annIndex++)
                {
                    Annotation ann = annotations[annIndex];

                    // Annotation type (enum value and CLR type name)
                    string typeInfo = $"{ann.AnnotationType} ({ann.GetType().Name})";

                    // Rectangle (coordinates)
                    Aspose.Pdf.Rectangle rect = ann.Rect;
                    string rectInfo = $"LLX={rect.LLX}, LLY={rect.LLY}, URX={rect.URX}, URY={rect.URY}";

                    // Attempt to retrieve a media file name (if any) via reflection
                    string mediaFile = GetMediaFileName(ann);

                    Console.WriteLine($"Page {pageIndex}, Annotation {annIndex}:");
                    Console.WriteLine($"  Type      : {typeInfo}");
                    Console.WriteLine($"  Rectangle : {rectInfo}");
                    Console.WriteLine($"  MediaFile : {(string.IsNullOrEmpty(mediaFile) ? "N/A" : mediaFile)}");
                }
            }

            // Save the document (unchanged) to satisfy lifecycle rule
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Document saved as '{outputPath}'.");
    }

    // Helper: extracts a media file name from known annotation types using reflection
    private static string GetMediaFileName(Annotation ann)
    {
        // Known property names that may hold a media file path
        string[] possibleNames = { "File", "MediaFile", "Source", "Path" };

        foreach (string propName in possibleNames)
        {
            PropertyInfo prop = ann.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.PropertyType == typeof(string))
            {
                var value = prop.GetValue(ann) as string;
                if (!string.IsNullOrEmpty(value))
                    return value;
            }
        }

        // No media file property found
        return null;
    }
}