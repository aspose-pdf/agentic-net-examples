using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths to the PDF files to compare
        const string pdfPath1 = "document1.pdf";
        const string pdfPath2 = "document2.pdf";

        // Verify that both files exist before proceeding
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both PDF files were not found.");
            return;
        }

        // Load the PDF documents using the core Document API (no Facades)
        Document doc1 = new Document(pdfPath1);
        Document doc2 = new Document(pdfPath2);

        // Perform a case‑insensitive text comparison page by page.
        // Aspose.Pdf.Comparison.ComparisonOptions does not expose an IgnoreCase flag,
        // so we extract the text ourselves and compare using StringComparison.OrdinalIgnoreCase.
        bool areIdenticalIgnoringCase = true;

        // If the documents have a different number of pages, they are considered different.
        if (doc1.Pages.Count != doc2.Pages.Count)
        {
            areIdenticalIgnoringCase = false;
        }
        else
        {
            for (int pageNumber = 1; pageNumber <= doc1.Pages.Count && areIdenticalIgnoringCase; pageNumber++)
            {
                // Extract text from the current page of each document.
                var absorber1 = new TextAbsorber();
                doc1.Pages[pageNumber].Accept(absorber1);
                string text1 = absorber1.Text ?? string.Empty;

                var absorber2 = new TextAbsorber();
                doc2.Pages[pageNumber].Accept(absorber2);
                string text2 = absorber2.Text ?? string.Empty;

                // Compare the texts ignoring case.
                if (!string.Equals(text1, text2, StringComparison.OrdinalIgnoreCase))
                {
                    areIdenticalIgnoringCase = false;
                }
            }
        }

        // Output the result
        Console.WriteLine($"Comparison result (case ignored): {(areIdenticalIgnoringCase ? "Identical" : "Different")}");
    }
}
