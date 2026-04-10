using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where PDFs with the added bookmark will be saved
        const string outputFolder = "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(pdfPath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_toc.pdf");

            try
            {
                // PdfBookmarkEditor is a facade that implements IDisposable
                using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
                {
                    // Load the PDF file into the facade
                    editor.BindPdf(pdfPath);

                    // Add a top‑level bookmark named "Table of Contents" that points to page 1
                    editor.CreateBookmarkOfPage("Table of Contents", 1);

                    // Persist the changes to a new file
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Processed: {pdfPath} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}