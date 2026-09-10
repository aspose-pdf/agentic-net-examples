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

        // Load the PDF document (lifecycle: using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        // Create a CSV writer (standard .NET I/O)
        using (StreamWriter writer = new StreamWriter(outputCsv, false))
        {
            // CSV header
            writer.WriteLine("PageNumber,AnnotationIndex,AnnotationName,FullName");

            // Pages are 1‑based (Aspose.Pdf rule)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Annotation collection is also 1‑based
                for (int annIdx = 1; annIdx <= page.Annotations.Count; annIdx++)
                {
                    Annotation ann = page.Annotations[annIdx];

                    // Prefer the Name property; fall back to FullName if Name is null/empty
                    string name = ann.Name ?? string.Empty;
                    string fullName = ann.FullName ?? string.Empty;

                    // Simple CSV escaping for commas
                    name = name.Replace(",", ";");
                    fullName = fullName.Replace(",", ";");

                    writer.WriteLine($"{pageNum},{annIdx},\"{name}\",\"{fullName}\"");
                }
            }
        }

        Console.WriteLine($"Annotation list saved to '{outputCsv}'.");
    }
}