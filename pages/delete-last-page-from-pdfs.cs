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
        foreach (string filePath in Directory.GetFiles(folderPath, "*.pdf"))
        {
            try
            {
                // Load the PDF document
                using (Document doc = new Document(filePath))
                {
                    // Ensure there is at least one page to delete
                    if (doc.Pages.Count > 0)
                    {
                        // Pages are 1‑based; delete the last page
                        int lastPageIndex = doc.Pages.Count;
                        doc.Pages.Delete(lastPageIndex);
                    }

                    // Save the modified document back to the same file
                    doc.Save(filePath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{filePath}': {ex.Message}");
            }
        }
    }
}