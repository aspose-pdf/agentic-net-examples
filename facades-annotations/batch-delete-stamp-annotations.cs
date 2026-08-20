using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing PDF files
        const string inputFolder = @"C:\PdfFolder";
        // Optional: output folder (can be same as input to overwrite)
        const string outputFolder = @"C:\PdfFolder\Processed";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Initialize the annotation editor facade
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    // Load the PDF document
                    editor.BindPdf(pdfPath);

                    // Delete all stamp annotations in the document
                    editor.DeleteAnnotations("Stamp");

                    // Determine output path (overwrite or separate folder)
                    string fileName = Path.GetFileName(pdfPath);
                    string outputPath = Path.Combine(outputFolder, fileName);

                    // Save the modified PDF
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Processed: {pdfPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch stamp deletion completed.");
    }
}