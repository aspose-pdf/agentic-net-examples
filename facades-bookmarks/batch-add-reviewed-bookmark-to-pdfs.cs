using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder  = "InputPdfs";
        // Folder where processed PDFs will be saved
        const string outputFolder = "OutputPdfs";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Ensure input directory exists – create it if missing and inform the user
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
            Console.WriteLine($"Input folder '{inputFolder}' was not found and has been created. Place PDF files there and re‑run the program.");
            return; // nothing to process now
        }

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Build output file name (e.g., "document_reviewed.pdf")
            string fileName   = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_reviewed.pdf");

            // Use PdfBookmarkEditor to add a bookmark
            using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
            {
                // Load the PDF file into the editor
                editor.BindPdf(inputPath);

                // Determine the last page number (Aspose.Pdf uses 1‑based indexing)
                int lastPage = editor.Document.Pages.Count;

                // Add a bookmark titled "Reviewed" that points to the last page
                editor.CreateBookmarkOfPage("Reviewed", lastPage);

                // Save the modified PDF to the output path
                editor.Save(outputPath);
            }

            Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} → {Path.GetFileName(outputPath)}");
        }
    }
}
