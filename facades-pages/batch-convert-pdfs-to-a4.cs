using System;
using System.IO;
using Aspose.Pdf;

class BatchResizeToA4
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = @"C:\PdfInput";
        // Folder where resized PDFs will be saved
        const string outputFolder = @"C:\PdfOutput";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found in the input folder.");
            return;
        }

        foreach (string inputPath in pdfFiles)
        {
            try
            {
                // Determine output file name (optional suffix to avoid overwriting)
                string outputPath = Path.Combine(outputFolder,
                    Path.GetFileNameWithoutExtension(inputPath) + "_A4.pdf");

                // Load the source PDF using Document (not PdfPageEditor)
                Document pdfDocument = new Document(inputPath);

                // Resize each page to A4 size using PageInfo
                foreach (Page page in pdfDocument.Pages)
                {
                    page.PageInfo.Width = PageSize.A4.Width;
                    page.PageInfo.Height = PageSize.A4.Height;
                }

                // Save the resized PDF
                pdfDocument.Save(outputPath);

                Console.WriteLine($"Resized '{Path.GetFileName(inputPath)}' → '{Path.GetFileName(outputPath)}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{Path.GetFileName(inputPath)}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch resizing completed.");
    }
}
