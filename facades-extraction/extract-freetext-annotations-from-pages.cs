using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "FreeText_2_4.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // Open the output text file
        using (StreamWriter writer = new StreamWriter(outputTxt, false))
        {
            // Iterate over the required page range (pages are 1‑based)
            for (int pageNumber = 2; pageNumber <= 4; pageNumber++)
            {
                // Ensure the page exists
                if (pageNumber > pdfDocument.Pages.Count)
                    continue;

                Page page = pdfDocument.Pages[pageNumber];
                bool pageHeaderWritten = false;

                // Scan all annotations on the page and pick FreeText ones
                foreach (Annotation annotation in page.Annotations)
                {
                    if (annotation is FreeTextAnnotation freeText)
                    {
                        if (!pageHeaderWritten)
                        {
                            writer.WriteLine($"--- Page {pageNumber} ---");
                            pageHeaderWritten = true;
                        }
                        // The textual content of a FreeText annotation is stored in the "Contents" property
                        writer.WriteLine(freeText.Contents);
                    }
                }

                if (pageHeaderWritten)
                {
                    writer.WriteLine(); // blank line between pages
                }
            }
        }

        Console.WriteLine($"FreeText annotations from pages 2‑4 saved to '{outputTxt}'.");
    }
}
