using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF and text file containing page numbers (one per line)
        const string inputPdfPath   = "input.pdf";
        const string pagesListPath  = "pages_to_delete.txt";
        const string outputPdfPath  = "output.pdf";

        // Validate existence of files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(pagesListPath))
        {
            Console.Error.WriteLine($"Page list file not found: {pagesListPath}");
            return;
        }

        // Read page numbers, ignore empty lines and non‑numeric entries
        List<int> pagesToDelete = new List<int>();
        foreach (string line in File.ReadAllLines(pagesListPath))
        {
            if (int.TryParse(line.Trim(), out int pageNum) && pageNum > 0)
                pagesToDelete.Add(pageNum);
        }

        if (pagesToDelete.Count == 0)
        {
            Console.WriteLine("No valid page numbers found to delete.");
            return;
        }

        // Remove duplicates and sort descending to avoid index shift while deleting
        pagesToDelete = pagesToDelete.Distinct()
                                     .OrderByDescending(p => p)
                                     .ToList();

        try
        {
            // Load PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdfPath))
            {
                int totalPages = doc.Pages.Count;

                foreach (int pageNum in pagesToDelete)
                {
                    // Ensure the page number is within the current range
                    if (pageNum >= 1 && pageNum <= totalPages)
                    {
                        doc.Pages.Delete(pageNum);
                        totalPages--; // Adjust count after deletion
                    }
                    else
                    {
                        Console.WriteLine($"Page number {pageNum} is out of range and will be ignored.");
                    }
                }

                // Save the modified document
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Pages deleted. Result saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}