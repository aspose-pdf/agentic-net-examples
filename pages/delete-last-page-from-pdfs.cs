using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Folder containing PDF files
        const string inputFolder = @"C:\PdfFolder";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Process each *.pdf file in the folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Ensure the document has at least one page
                    if (doc.Pages.Count > 0)
                    {
                        // Pages are 1‑based; delete the last page
                        int lastPageIndex = doc.Pages.Count;
                        doc.Pages.Delete(lastPageIndex);
                    }

                    // Overwrite the original file with the modified document
                    doc.Save(pdfPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}