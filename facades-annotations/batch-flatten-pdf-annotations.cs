using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the source PDFs
        const string inputFolder = "input_pdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Process each PDF file in the folder
        foreach (string sourcePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string directory = Path.GetDirectoryName(sourcePath);
            string baseName   = Path.GetFileNameWithoutExtension(sourcePath);
            string outputPath = Path.Combine(directory, $"{baseName}_flattened.pdf");

            try
            {
                // Create the facade, load the PDF, flatten annotations, and save the result
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(sourcePath);          // load PDF
                    editor.FlatteningAnnotations();     // flatten all annotations
                    editor.Save(outputPath);             // save flattened PDF
                }

                Console.WriteLine($"Flattened: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{sourcePath}': {ex.Message}");
            }
        }
    }
}