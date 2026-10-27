using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string filePath1 = "doc1.pdf";
        const string filePath2 = "doc2.pdf";
        const string outputPath = "comparison_result.pdf";

        if (!File.Exists(filePath1) || !File.Exists(filePath2))
        {
            Console.Error.WriteLine("One or both input files are missing.");
            return;
        }

        // Load the two PDFs inside using blocks for deterministic disposal.
        using (Document doc1 = new Document(filePath1))
        using (Document doc2 = new Document(filePath2))
        {
            // Assume both documents have the same page size; use the first page as reference.
            Page firstPage = doc1.Pages[1];
            double pageWidth = firstPage.PageInfo.Width;
            double pageHeight = firstPage.PageInfo.Height;

            // Define the footer height (e.g., 50 points). Adjust as needed.
            double footerHeight = 50;

            // Create a rectangle that covers the footer area (bottom of the page).
            // Rectangle is defined as lower‑left X,Y and upper‑right X,Y.
            Rectangle footerRect = new Rectangle(0, 0, pageWidth, footerHeight);

            // Configure comparison options and add the footer rectangle to the ExcludeAreas collections
            // for both documents (the same rectangle works for both if page sizes match).
            var options = new SideBySideComparisonOptions();
            options.ExcludeAreas1 = new[] { footerRect }; // Exclude footer from the first document
            options.ExcludeAreas2 = new[] { footerRect }; // Exclude footer from the second document

            // Perform side‑by‑side visual comparison. The method is static and returns void.
            SideBySidePdfComparer.Compare(doc1, doc2, outputPath, options);
        }

        Console.WriteLine($"Comparison completed. Result saved to '{outputPath}'.");
    }
}
