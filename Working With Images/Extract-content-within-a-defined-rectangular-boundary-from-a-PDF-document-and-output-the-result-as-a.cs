using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "extracted.pdf";

        // Define the rectangle (llx, lly, urx, ury) in points.
        // Example: lower‑left (100, 500), upper‑right (300, 700)
        Aspose.Pdf.Rectangle extractRect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load source PDF.
        using (Document srcDoc = new Document(inputPath))
        {
            // Prepare a TextFragmentAbsorber that will collect text inside the rectangle.
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();

            // Restrict the search to the defined rectangle.
            absorber.TextSearchOptions.Rectangle = extractRect;

            // Accept the absorber for the first page (adjust index as needed).
            // Aspose.Pdf uses 1‑based page indexing.
            srcDoc.Pages[1].Accept(absorber);

            // Create a new PDF that will contain the extracted content.
            using (Document resultDoc = new Document())
            {
                // Add a blank page to the result document.
                Page resultPage = resultDoc.Pages.Add();

                // Copy each extracted fragment to the new page.
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    // Clone the fragment to avoid linking it to the source document.
                    TextFragment cloned = (TextFragment)fragment.Clone();

                    // Optionally adjust the fragment position so it fits the new page.
                    // Here we keep the original coordinates.
                    resultPage.Paragraphs.Add(cloned);
                }

                // Save the new PDF.
                resultDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"Extracted content saved to '{outputPath}'.");
    }
}