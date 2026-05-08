using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "annotation_audit.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            StringBuilder sb = new StringBuilder();
            // CSV header
            sb.AppendLine("AnnotationId,Author");

            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                AnnotationCollection annotations = page.Annotations;

                // Iterate through all annotations on the page (1‑based indexing)
                for (int annIndex = 1; annIndex <= annotations.Count; annIndex++)
                {
                    Annotation ann = annotations[annIndex];

                    // Use the Name property as a surrogate identifier (Id does not exist)
                    string id = ann.Name ?? string.Empty;

                    // Author information is stored in the Subject property of markup annotations
                    string author = string.Empty;
                    if (ann is MarkupAnnotation markup)
                    {
                        author = markup.Subject ?? string.Empty;
                    }

                    // Escape commas in values if needed
                    id = id.Replace(",", "\\,");
                    author = author.Replace(",", "\\,");

                    sb.AppendLine($"{id},{author}");
                }
            }

            // Write CSV content to file
            File.WriteAllText(outputCsvPath, sb.ToString(), Encoding.UTF8);
        }

        Console.WriteLine($"Annotation audit CSV generated at '{outputCsvPath}'.");
    }
}
