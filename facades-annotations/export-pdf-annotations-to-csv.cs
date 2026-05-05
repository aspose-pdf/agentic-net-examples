using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputCsv = "annotations.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (load rule)
        using (Document doc = new Document(inputPdf))
        {
            // Create CSV file for output
            using (StreamWriter writer = new StreamWriter(outputCsv))
            {
                // CSV header
                writer.WriteLine("PageNumber,AnnotationName");

                // Iterate pages (1‑based indexing)
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Iterate annotations on the current page (1‑based indexing)
                    for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                    {
                        Annotation annotation = page.Annotations[annIndex];
                        string name = GetAnnotationName(annotation);
                        writer.WriteLine($"{pageIndex},\"{name}\"");
                    }
                }
            }
        }

        Console.WriteLine($"Annotation list saved to '{outputCsv}'.");
    }

    // Retrieves a readable name for an annotation.
    // Tries the 'Name' property, then 'Title' (for markup annotations),
    // and finally falls back to the annotation type name.
    static string GetAnnotationName(Annotation annotation)
    {
        // Attempt to read the 'Name' property via reflection (if it exists)
        var nameProp = annotation.GetType().GetProperty("Name");
        if (nameProp != null)
        {
            var value = nameProp.GetValue(annotation) as string;
            if (!string.IsNullOrEmpty(value))
                return value;
        }

        // For markup annotations, use the Title property
        if (annotation is MarkupAnnotation markup && !string.IsNullOrEmpty(markup.Title))
            return markup.Title;

        // Fallback: use the enum name of the annotation type
        return annotation.AnnotationType.ToString();
    }
}