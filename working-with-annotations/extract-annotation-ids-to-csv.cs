using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string csvReport = "annotation_audit.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("AnnotationId,Author"); // CSV header

            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                AnnotationCollection annotations = page.Annotations;

                // Iterate through annotations (1‑based indexing)
                for (int annIndex = 1; annIndex <= annotations.Count; annIndex++)
                {
                    Annotation ann = annotations[annIndex];

                    // "Name" is the identifier for an annotation.
                    string id = ann.Name ?? string.Empty;

                    // The author (or title) is stored in the Title property of MarkupAnnotation.
                    string author = string.Empty;
                    if (ann is MarkupAnnotation markup && !string.IsNullOrEmpty(markup.Title))
                    {
                        author = markup.Title;
                    }

                    // Escape commas in fields if necessary
                    id = id.Replace(",", "\\,");
                    author = author.Replace(",", "\\,");

                    sb.AppendLine($"{id},{author}");
                }
            }

            // Write CSV report (lifecycle rule: document already saved, now just output file)
            File.WriteAllText(csvReport, sb.ToString(), Encoding.UTF8);
            Console.WriteLine($"Annotation audit report saved to '{csvReport}'.");
        }
    }
}
