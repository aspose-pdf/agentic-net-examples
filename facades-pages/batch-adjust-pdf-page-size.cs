using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = @"C:\PdfInput";
        // Folder where processed PDFs will be saved
        const string outputFolder = @"C:\PdfOutput";

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(pdfPath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_Adjusted.pdf");

            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Create a PdfPageEditor facade to work with page properties
                using (PdfPageEditor editor = new PdfPageEditor())
                {
                    // Bind the loaded document to the editor
                    editor.BindPdf(doc);

                    // Define the target page size (example: A4)
                    // PageSize.PageLetter, PageSize.A4, etc. are available static properties
                    PageSize targetSize = PageSize.A4;

                    // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
                    for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                    {
                        // Set the size of each page to the target dimensions
                        // Page.SetPageSize expects width and height as double values
                        doc.Pages[pageIndex].SetPageSize(targetSize.Width, targetSize.Height);
                    }

                    // Apply any pending changes made through the editor
                    editor.ApplyChanges();
                }

                // Save the modified document to the output folder
                doc.Save(outputPath);
                Console.WriteLine($"Processed and saved: {outputPath}");
            }
        }
    }
}