using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the PDFs to process
        const string inputFolder = "InputPdfs";
        // Folder where the updated PDFs will be written
        const string outputFolder = "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_reviewed.pdf");

            // === Create ===
            PdfBookmarkEditor editor = new PdfBookmarkEditor();

            // === Load ===
            editor.BindPdf(inputPath);

            // Determine the last page (Aspose.Pdf uses 1‑based indexing)
            int lastPageNumber = editor.Document.Pages.Count;

            // Add a bookmark titled "Reviewed" that points to the last page
            editor.CreateBookmarkOfPage("Reviewed", lastPageNumber);

            // === Save ===
            editor.Save(outputPath);

            // Release resources held by the facade
            editor.Close();

            Console.WriteLine($"Processed: {inputPath} → {outputPath}");
        }
    }
}