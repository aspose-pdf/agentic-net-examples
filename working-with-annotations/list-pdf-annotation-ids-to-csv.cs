using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string csvPath = "annotations.csv";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(pdfPath))
        // Open the CSV file for writing inside a using block.
        using (StreamWriter writer = new StreamWriter(csvPath, false))
        {
            // Write CSV header.
            writer.WriteLine("PageNumber,AnnotationName");

            // Aspose.Pdf uses 1‑based page indexing.
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Iterate over all annotations on the current page.
                foreach (Annotation annotation in page.Annotations)
                {
                    // Use the Name property (or empty string if not set) as the identifier.
                    string name = annotation.Name ?? string.Empty;

                    // Write a CSV line: page number and annotation name.
                    writer.WriteLine($"{pageNum},{name}");
                }
            }
        }

        Console.WriteLine($"Annotation names have been written to '{csvPath}'.");
    }
}
