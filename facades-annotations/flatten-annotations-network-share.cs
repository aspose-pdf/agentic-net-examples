using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // UNC path to the network share containing PDFs
        const string sourceFolder = @"\\server\share\pdfs";

        // Folder where flattened PDFs will be written (can be the same as source)
        const string destinationRoot = @"\\server\share\pdfs\flattened";

        if (!Directory.Exists(sourceFolder))
        {
            Console.Error.WriteLine($"Source folder not found: {sourceFolder}");
            return;
        }

        // Ensure the destination root exists
        Directory.CreateDirectory(destinationRoot);

        // Retrieve all PDF files recursively
        string[] pdfFiles = Directory.GetFiles(sourceFolder, "*.pdf", SearchOption.AllDirectories);

        foreach (string inputPath in pdfFiles)
        {
            try
            {
                // Preserve relative folder structure in the destination
                string relativePath = Path.GetRelativePath(sourceFolder, inputPath);
                string outputPath = Path.Combine(destinationRoot, relativePath);
                Directory.CreateDirectory(Path.GetDirectoryName(outputPath));

                // Flatten annotations using PdfAnnotationEditor
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    // Bind the source PDF
                    editor.BindPdf(inputPath);

                    // Flatten remaining annotations (method name is FlatteningAnnotations)
                    editor.FlatteningAnnotations();

                    // Save the flattened PDF
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Flattened: {inputPath} -> {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}