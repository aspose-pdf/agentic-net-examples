using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing PDF files
        const string folderPath = @"C:\PdfFolder";

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Process each PDF file in the folder
        foreach (string pdfFile in Directory.GetFiles(folderPath, "*.pdf"))
        {
            try
            {
                // Load the document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfFile))
                {
                    // Ensure the document has at least one page
                    if (doc.Pages.Count > 0)
                    {
                        // Pages are 1‑based; delete the last page
                        int lastPageIndex = doc.Pages.Count;
                        doc.Pages.Delete(lastPageIndex);
                    }

                    // Save the modified document back to the same file (overwrites original)
                    doc.Save(pdfFile);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(pdfFile)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfFile}': {ex.Message}");
            }
        }
    }
}