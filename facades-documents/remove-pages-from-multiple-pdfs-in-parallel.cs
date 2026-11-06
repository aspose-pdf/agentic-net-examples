using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Aspose.Pdf; // Document API for page manipulation

class PdfPageRemover
{
    // Entry point: args[0] = comma‑separated page numbers to delete,
    // args[1..] = paths of PDF files to process.
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: PdfPageRemover <pages> <pdf1> [<pdf2> ...]");
            Console.Error.WriteLine("  <pages> : comma‑separated list of 1‑based page numbers to remove (e.g. 2,5,7)");
            return;
        }

        // Parse page numbers (1‑based) into an int array.
        int[] pagesToRemove = ParsePageNumbers(args[0]);
        if (pagesToRemove.Length == 0)
        {
            Console.Error.WriteLine("No valid page numbers supplied.");
            return;
        }

        // Collect PDF file paths.
        string[] pdfFiles = args.Skip(1).ToArray();

        // Process each PDF in parallel.
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                ProcessPdf(pdfPath, pagesToRemove);
                Console.WriteLine($"Processed: {pdfPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        });
    }

    // Converts a comma‑separated string into an array of distinct, sorted page numbers.
    private static int[] ParsePageNumbers(string pagesCsv)
    {
        var numbers = new HashSet<int>();
        foreach (var part in pagesCsv.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            if (int.TryParse(part.Trim(), out int n) && n > 0)
                numbers.Add(n);
        }
        // Aspose.Pdf expects the page numbers in ascending order for our own logic.
        return numbers.OrderBy(n => n).ToArray();
    }

    // Removes the specified pages from a single PDF file.
    private static void ProcessPdf(string inputPath, int[] pagesToRemove)
    {
        if (!File.Exists(inputPath))
            throw new FileNotFoundException("Input PDF not found.", inputPath);

        // Determine output path (overwrite original safely).
        string tempPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty,
                                       Path.GetFileNameWithoutExtension(inputPath) + "_tmp.pdf");

        // Load the PDF with Document (thread‑local instance).
        using (var pdfDoc = new Document(inputPath))
        {
            // Delete pages in descending order to keep indexes stable.
            foreach (int pageNum in pagesToRemove.Reverse())
            {
                if (pageNum <= pdfDoc.Pages.Count)
                    pdfDoc.Pages.Delete(pageNum);
            }

            // Save the modified document to a temporary file.
            pdfDoc.Save(tempPath);
        }

        // Replace original file with the trimmed version.
        File.Delete(inputPath);
        File.Move(tempPath, inputPath);
    }
}
