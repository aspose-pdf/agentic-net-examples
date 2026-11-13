using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string listFile  = "pages_to_delete.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(listFile))
        {
            Console.Error.WriteLine($"Page list file not found: {listFile}");
            return;
        }

        // Read page numbers (one per line) and collect valid integers
        List<int> pagesToDelete = new List<int>();
        foreach (string line in File.ReadAllLines(listFile))
        {
            if (int.TryParse(line.Trim(), out int pageNum))
                pagesToDelete.Add(pageNum);
        }

        // Delete pages in descending order to avoid index shifting
        pagesToDelete.Sort((a, b) => b.CompareTo(a));

        try
        {
            // Load PDF and ensure deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                foreach (int pageNum in pagesToDelete)
                {
                    // Aspose.Pdf uses 1‑based page indexing
                    if (pageNum >= 1 && pageNum <= doc.Pages.Count)
                        doc.Pages.Delete(pageNum);
                }

                // Save the modified document
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Deleted specified pages. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}