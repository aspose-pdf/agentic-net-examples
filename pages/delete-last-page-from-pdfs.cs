using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing the PDFs – modify as needed
        string folderPath = @"C:\PDFs";

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Get all PDF files in the folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load each PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Ensure the document has at least one page
                    if (doc.Pages.Count > 0)
                    {
                        // Pages are 1‑based; delete the last page
                        doc.Pages.Delete(doc.Pages.Count);

                        // Overwrite the original file with the modified document
                        doc.Save(pdfPath);

                        Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)}");
                    }
                    else
                    {
                        Console.WriteLine($"Skipped (no pages): {Path.GetFileName(pdfPath)}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}