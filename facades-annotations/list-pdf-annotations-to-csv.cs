using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class AnnotationLister
{
    static void Main()
    {
        // Input PDF path and output CSV path
        const string pdfPath = "input.pdf";
        const string csvPath = "annotations.csv";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document using Aspose.Pdf.Document (the primary API for accessing pages and annotations)
        Document pdfDocument = new Document(pdfPath);
        int pageCount = pdfDocument.Pages.Count; // Pages are 1‑based

        // Prepare CSV file (header row)
        using (StreamWriter writer = new StreamWriter(csvPath, false, System.Text.Encoding.UTF8))
        {
            writer.WriteLine("PageNumber,AnnotationName");

            // Iterate through all pages
            for (int pageNum = 1; pageNum <= pageCount; pageNum++)
            {
                // Retrieve annotations on the current page
                var page = pdfDocument.Pages[pageNum];
                if (page == null || page.Annotations == null) continue;

                foreach (Annotation annotation in page.Annotations)
                {
                    // Annotation.Name holds the annotation's name (if set). If not, fall back to AnnotationType.
                    string name = !string.IsNullOrEmpty(annotation.Name)
                        ? annotation.Name
                        : annotation.AnnotationType.ToString();

                    // Write CSV line: page number and annotation name (quote the name to protect commas)
                    writer.WriteLine($"{pageNum},\"{name}\"");
                }
            }
        }

        Console.WriteLine($"Annotation list saved to '{csvPath}'.");
    }
}
