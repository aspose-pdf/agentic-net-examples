using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputCsv = "annotations_report.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Prepare CSV header
                using (StreamWriter writer = new StreamWriter(outputCsv, false))
                {
                    writer.WriteLine("PageNumber,AnnotationId,Author");

                    // Aspose.Pdf uses 1‑based page indexing
                    for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                    {
                        Page page = doc.Pages[pageNum];

                        // Each page has an Annotations collection
                        foreach (Annotation annot in page.Annotations)
                        {
                            // "Id" does not exist – use the optional Name property as an identifier
                            string id = annot.Name ?? string.Empty;

                            // Author information is stored in the Subject property of MarkupAnnotation
                            string author = string.Empty;
                            if (annot is MarkupAnnotation markup)
                            {
                                author = markup.Subject ?? string.Empty;
                            }

                            // Escape commas in fields if necessary
                            id = id.Contains(",") ? $"\"{id}\"" : id;
                            author = author.Contains(",") ? $"\"{author}\"" : author;

                            writer.WriteLine($"{pageNum},{id},{author}");
                        }
                    }
                }
            }

            Console.WriteLine($"Annotation report generated: {outputCsv}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
