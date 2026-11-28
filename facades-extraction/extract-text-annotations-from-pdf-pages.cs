using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputPath = "TextNotes.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(pdfPath);

        // Ensure the requested page range is within the document bounds
        int startPage = 3;
        int endPage   = 7;
        endPage = Math.Min(endPage, pdfDocument.Pages.Count);

        using (StreamWriter writer = new StreamWriter(outputPath, false))
        {
            // Iterate through the required pages (Aspose.Pdf pages are 1‑based)
            for (int pageNumber = startPage; pageNumber <= endPage; pageNumber++)
            {
                Page page = pdfDocument.Pages[pageNumber];
                if (page == null) continue;

                // Examine each annotation on the page
                foreach (Annotation annotation in page.Annotations)
                {
                    // We are interested only in text annotations (sticky notes, etc.)
                    if (annotation is TextAnnotation textAnnotation)
                    {
                        // Write the annotation's contents to the output file
                        writer.WriteLine(textAnnotation.Contents);
                    }
                }
            }
        }

        Console.WriteLine($"Text annotations from pages {startPage}-{endPage} saved to '{outputPath}'.");
    }
}
