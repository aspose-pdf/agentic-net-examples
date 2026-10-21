using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Folder containing PDFs to process – either passed as first argument or defaulted.
        string sourceFolder = args.Length > 0 ? args[0] : "PdfFolder";

        if (!Directory.Exists(sourceFolder))
        {
            Console.Error.WriteLine($"Folder not found: {sourceFolder}");
            return;
        }

        // Process each PDF file in the folder.
        foreach (string inputPath in Directory.GetFiles(sourceFolder, "*.pdf"))
        {
            try
            {
                // Build output file name with "_flattened" suffix.
                string directory = Path.GetDirectoryName(inputPath) ?? sourceFolder; // fallback to sourceFolder if null
                string fileName = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(directory, $"{fileName}_flattened.pdf");

                // Use PdfAnnotationEditor to flatten annotations.
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(inputPath);          // bind the source PDF
                    editor.FlatteningAnnotations();    // correct method name
                    editor.Save(outputPath);            // write the flattened PDF
                }

                Console.WriteLine($"Flattened: '{inputPath}' → '{outputPath}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}
