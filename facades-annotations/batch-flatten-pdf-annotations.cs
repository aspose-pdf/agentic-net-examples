using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder = @"C:\PdfFolder";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Process each PDF file in the folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Build output file name with "_flattened" suffix
                string directory = Path.GetDirectoryName(pdfPath);
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
                string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_flattened.pdf");

                // Use PdfAnnotationEditor to flatten annotations
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(pdfPath);               // Load the PDF
                    editor.FlatteningAnnotations();        // Flatten all annotations
                    editor.Save(outputPath);                // Save the flattened PDF
                }

                Console.WriteLine($"Flattened: {Path.GetFileName(pdfPath)} → {Path.GetFileName(outputPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}