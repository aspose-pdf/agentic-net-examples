using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    // Usage: DeletePages.exe <input.pdf> <output.pdf> <pages>
    // <pages> is a comma‑separated list of 1‑based page numbers to delete, e.g. "2,4,5"
    static void Main(string[] args)
    {
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: DeletePages.exe <input.pdf> <output.pdf> <pages>");
            Console.Error.WriteLine("Example: DeletePages.exe source.pdf result.pdf 2,4,5");
            return;
        }

        string inputPath  = args[0];
        string outputPath = args[1];
        string pagesArg   = args[2];

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Parse the pages argument into an array of integers (1‑based indexing)
        int[] pagesToDelete;
        try
        {
            pagesToDelete = pagesArg
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(p => int.Parse(p.Trim()))
                .ToArray();

            // Validate page numbers (must be >= 1)
            if (pagesToDelete.Any(p => p < 1))
                throw new ArgumentException("Page numbers must be greater than zero.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error parsing pages list: {ex.Message}");
            return;
        }

        try
        {
            // Load the PDF document using Aspose.Pdf.Document (PdfFileEditor has no DeletePage method)
            Document pdfDoc = new Document(inputPath);

            // Ensure requested pages exist in the document
            int maxPage = pdfDoc.Pages.Count;
            if (pagesToDelete.Any(p => p > maxPage))
                throw new ArgumentException($"One or more page numbers exceed the document page count ({maxPage}).");

            // Delete pages in descending order to keep indexes stable
            foreach (int pageNumber in pagesToDelete.OrderByDescending(p => p))
            {
                pdfDoc.Pages.Delete(pageNumber);
            }

            // Save the modified document to the output path
            pdfDoc.Save(outputPath);

            Console.WriteLine($"Pages {string.Join(",", pagesToDelete)} deleted. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during page deletion: {ex.Message}");
        }
    }
}
