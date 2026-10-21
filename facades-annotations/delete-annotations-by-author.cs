using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string targetAuthor = "John Doe";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (Aspose.Pdf Document API provides full annotation access)
        Document pdfDoc = new Document(inputPath);

        // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
        for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
        {
            Page page = pdfDoc.Pages[pageNum];
            // Iterate backwards so that removal does not affect the remaining indices
            for (int i = page.Annotations.Count - 1; i >= 0; i--)
            {
                Annotation annot = page.Annotations[i];
                // The author information is stored in the Subject property of MarkupAnnotation
                if (annot is MarkupAnnotation markup &&
                    !string.IsNullOrEmpty(markup.Subject) &&
                    markup.Subject.Equals(targetAuthor, StringComparison.OrdinalIgnoreCase))
                {
                    // Delete the matching annotation
                    page.Annotations.Delete(i);
                }
            }
        }

        // Save the modified PDF
        pdfDoc.Save(outputPath);
        Console.WriteLine($"Annotations authored by '{targetAuthor}' have been removed. Saved to '{outputPath}'.");
    }
}
