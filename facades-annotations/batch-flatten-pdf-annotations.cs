using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing PDF files to process
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
                // Build output file name with "_flattened" suffix before the extension
                string directory = Path.GetDirectoryName(pdfPath);
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
                string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_flattened.pdf");

                // Use PdfAnnotationEditor to flatten annotations
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    // Bind the source PDF
                    editor.BindPdf(pdfPath);

                    // Flatten all annotations in the document
                    editor.FlatteningAnnotations();

                    // Save the flattened PDF
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