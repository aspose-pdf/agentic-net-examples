using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "input_pdfs";
        // Folder where processed PDFs will be written
        const string outputFolder = "output_pdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string sourcePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(sourcePath);
            string destinationPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Create the facade, bind the source PDF, add the bookmark, and save
                using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
                {
                    // Load the PDF into the facade
                    editor.BindPdf(sourcePath);

                    // Add a top‑level bookmark named "Table of Contents" that points to page 1
                    editor.CreateBookmarkOfPage("Table of Contents", 1);

                    // Save the modified PDF to the output location
                    editor.Save(destinationPath);
                }

                Console.WriteLine($"Processed: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {fileName}: {ex.Message}");
            }
        }
    }
}