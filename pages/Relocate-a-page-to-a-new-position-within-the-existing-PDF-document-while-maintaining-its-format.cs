using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path
        const string outputPath = "output.pdf";

        // Page numbers are 1‑based in Aspose.Pdf
        // Specify the page to move and the new position
        const int sourcePageNumber = 3; // page to relocate
        const int targetPageNumber = 1; // new position (1 = first page)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Validate page numbers
            if (sourcePageNumber < 1 || sourcePageNumber > doc.Pages.Count ||
                targetPageNumber < 1 || targetPageNumber > doc.Pages.Count + 1)
            {
                Console.Error.WriteLine("Invalid source or target page number.");
                return;
            }

            // Retrieve the page to be moved
            Page pageToMove = doc.Pages[sourcePageNumber];

            // Remove the page from its original location
            doc.Pages.Delete(sourcePageNumber);

            // Adjust target index if the source page was before the target
            int adjustedTarget = targetPageNumber;
            if (sourcePageNumber < targetPageNumber)
                adjustedTarget--; // collection has shifted left after deletion

            // Insert the page at the new position
            doc.Pages.Insert(adjustedTarget, pageToMove);

            // Save the modified PDF (PDF format, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page {sourcePageNumber} moved to position {targetPageNumber} and saved as '{outputPath}'.");
    }
}