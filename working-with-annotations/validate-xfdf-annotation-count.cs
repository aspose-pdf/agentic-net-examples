using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the XFDF file containing annotations
        const string pdfPath = "input.pdf";
        const string xfdfPath = "annotations.xfdf";

        // Expected number of annotations on each page (1‑based indexing)
        // Adjust the array to match the expected counts for your document
        int[] expectedCounts = { 2, 0, 3 }; // example: page1=2, page2=0, page3=3

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: wrap in using for deterministic disposal)
            using (Document doc = new Document(pdfPath))
            {
                // Import annotations from the XFDF file into the document
                doc.ImportAnnotationsFromXfdf(xfdfPath);

                // Validate annotation counts page by page
                for (int i = 1; i <= doc.Pages.Count; i++) // Pages are 1‑based
                {
                    Page page = doc.Pages[i];
                    int actualCount = page.Annotations.Count;
                    int expectedCount = i <= expectedCounts.Length ? expectedCounts[i - 1] : 0;

                    if (actualCount == expectedCount)
                    {
                        Console.WriteLine($"Page {i}: OK – {actualCount} annotation(s) as expected.");
                    }
                    else
                    {
                        Console.WriteLine($"Page {i}: MISMATCH – expected {expectedCount}, found {actualCount}.");
                    }
                }

                // Optional: save the PDF with the imported annotations
                doc.Save("output_with_annotations.pdf");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}