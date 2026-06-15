using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where PDFs with the new bookmark will be saved
        const string outputFolder = "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_reviewed.pdf");

            // Initialize the bookmark editor (facade)
            Aspose.Pdf.Facades.PdfBookmarkEditor editor = new Aspose.Pdf.Facades.PdfBookmarkEditor();

            // Bind the source PDF file to the editor
            editor.BindPdf(inputPath);

            // Determine the last page number (Aspose.Pdf uses 1‑based indexing)
            using (Document doc = new Document(inputPath))
            {
                int lastPage = doc.Pages.Count;
                // Add a bookmark titled "Reviewed" that points to the last page
                editor.CreateBookmarkOfPage("Reviewed", lastPage);
            }

            // Save the modified PDF to the output location
            editor.Save(outputPath);

            // Release resources held by the facade
            editor.Close();
        }

        Console.WriteLine("Batch bookmark addition completed.");
    }
}