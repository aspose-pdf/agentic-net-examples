using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the PDF files to process
        const string inputFolder = @"C:\PdfFolder";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Get all PDF files in the folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Build the output file name with "_flattened" suffix
                string directory = Path.GetDirectoryName(pdfPath);
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
                string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_flattened.pdf");

                // Use PdfAnnotationEditor to flatten annotations
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    // Load the source PDF
                    editor.BindPdf(pdfPath);

                    // Flatten all annotations in the document
                    editor.FlatteningAnnotations();

                    // Save the flattened PDF to the new file
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Flattened: '{pdfPath}' → '{outputPath}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}