using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Network share containing PDFs to process
    private const string PdfFolder = @"\\server\share\pdfs";

    static void Main()
    {
        if (!Directory.Exists(PdfFolder))
        {
            Console.Error.WriteLine($"Folder not found: {PdfFolder}");
            return;
        }

        // Process each PDF file in the folder (non‑recursive)
        foreach (string inputPath in Directory.GetFiles(PdfFolder, "*.pdf"))
        {
            string tempPath = Path.Combine(Path.GetDirectoryName(inputPath),
                                           Path.GetFileNameWithoutExtension(inputPath) + "_flattened.pdf");

            try
            {
                // Flatten annotations using PdfAnnotationEditor
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    // Bind the source PDF
                    editor.BindPdf(inputPath);

                    // Optional: remove existing annotations before flattening
                    editor.DeleteAnnotations();

                    // Flatten remaining annotations into the page content
                    editor.FlatteningAnnotations();

                    // Save the flattened PDF to a temporary file
                    editor.Save(tempPath);
                }

                // Replace original file with the flattened version
                File.Delete(inputPath);
                File.Move(tempPath, inputPath);

                Console.WriteLine($"Flattened annotations in: {inputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
                // Clean up temporary file if it exists
                if (File.Exists(tempPath))
                {
                    try { File.Delete(tempPath); } catch { }
                }
            }
        }

        Console.WriteLine("Annotation flattening task completed.");
    }
}