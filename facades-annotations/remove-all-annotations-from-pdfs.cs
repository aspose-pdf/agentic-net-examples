using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing the PDFs to process
        const string folderPath = @"C:\PDFs";

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Get all PDF files in the specified folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load the PDF document
                using (Document pdfDoc = new Document(pdfPath))
                {
                    // Remove every annotation from every page
                    foreach (Page page in pdfDoc.Pages)
                    {
                        page.Annotations.Clear();
                    }

                    // Overwrite the original file with the cleaned document
                    pdfDoc.Save(pdfPath);
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
