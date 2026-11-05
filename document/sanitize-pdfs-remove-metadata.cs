using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Source folder containing PDFs to be sanitized
        const string sourceFolder = @"C:\InputPdfs";
        // Target folder where cleaned PDFs will be saved
        const string targetFolder = @"C:\SanitizedPdfs";

        // Ensure the target folder exists
        Directory.CreateDirectory(targetFolder);

        // Get all PDF files in the source folder
        string[] pdfFiles = Directory.GetFiles(sourceFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found in the source folder.");
            return;
        }

        foreach (string inputPath in pdfFiles)
        {
            try
            {
                // Load the PDF document
                using (Document doc = new Document(inputPath))
                {
                    // ---------- Sanitization steps ----------
                    // 1. Clear document metadata
                    doc.Info.Title = string.Empty;
                    doc.Info.Author = string.Empty;
                    doc.Info.Subject = string.Empty;
                    doc.Info.Keywords = string.Empty;
                    doc.Info.Creator = string.Empty;
                    doc.Info.Producer = string.Empty;
                    // DateTime properties are non‑nullable structs; use MinValue to clear them
                    doc.Info.CreationDate = DateTime.MinValue;
                    doc.Info.ModDate = DateTime.MinValue;

                    // 2. Remove all annotations from each page
                    // Aspose.Pdf uses 1‑based page indexing
                    for (int i = 1; i <= doc.Pages.Count; i++)
                    {
                        Page page = doc.Pages[i];
                        page.Annotations.Clear();
                    }

                    // Additional sanitization (e.g., remove embedded files) can be added here
                    // if (doc.EmbeddedFiles != null) doc.EmbeddedFiles.Clear();

                    // ---------- Save sanitized PDF ----------
                    string fileName = Path.GetFileName(inputPath);
                    string outputPath = Path.Combine(targetFolder, fileName);
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Sanitized: {Path.GetFileName(inputPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Sanitization complete.");
    }
}
