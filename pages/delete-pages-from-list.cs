using System;
using System.IO;
using System.Collections.Generic;
using System.Linq; // Added for ToArray() extension method
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        const string inputPath = "input.pdf";
        const string pagesFilePath = "pages.txt";
        const string outputPath = "output.pdf";

        // Verify that the required files exist before attempting to open them.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: PDF file '{inputPath}' not found.");
            return;
        }

        if (!File.Exists(pagesFilePath))
        {
            Console.Error.WriteLine($"Error: Pages list file '{pagesFilePath}' not found.");
            return;
        }

        try
        {
            // Load the PDF document.
            using (Document document = new Document(inputPath))
            {
                // Use a SortedSet with a descending comparer so that pages are deleted
                // from highest to lowest index – this prevents index shifting issues.
                var pagesToDelete = new SortedSet<int>(Comparer<int>.Create((a, b) => b.CompareTo(a)));

                // Read page numbers from the text file (one number per line).
                foreach (string line in File.ReadAllLines(pagesFilePath))
                {
                    if (int.TryParse(line.Trim(), out int pageNumber))
                    {
                        // Validate page number range (1‑based indexing).
                        if (pageNumber >= 1 && pageNumber <= document.Pages.Count)
                        {
                            pagesToDelete.Add(pageNumber);
                        }
                    }
                }

                // Delete the specified pages if any were found.
                if (pagesToDelete.Count > 0)
                {
                    // ToArray() is now available via System.Linq.
                    document.Pages.Delete(pagesToDelete.ToArray());
                }

                // Save the modified PDF.
                document.Save(outputPath);
                Console.WriteLine($"Pages removed successfully. Output saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
