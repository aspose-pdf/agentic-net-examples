using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the PDF file (assumed to already contain the imported XFDF annotations)
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Expected annotation count per page (page numbers are 1‑based)
        var expectedCounts = new Dictionary<int, int>
        {
            { 1, 3 }, // page 1 should have 3 annotations
            { 2, 2 }, // page 2 should have 2 annotations
            // add more entries as needed
        };

        try
        {
            using (Document doc = new Document(pdfPath))
            {
                foreach (var kvp in expectedCounts)
                {
                    int pageNumber = kvp.Key;
                    int expected = kvp.Value;

                    if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                    {
                        Console.WriteLine($"Page {pageNumber} does not exist in the document.");
                        continue;
                    }

                    Page page = doc.Pages[pageNumber];
                    int actual = page.Annotations.Count;

                    if (actual == expected)
                    {
                        Console.WriteLine($"Page {pageNumber}: annotation count matches expected ({actual}).");
                    }
                    else
                    {
                        Console.WriteLine($"Page {pageNumber}: annotation count mismatch. Expected {expected}, found {actual}.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}