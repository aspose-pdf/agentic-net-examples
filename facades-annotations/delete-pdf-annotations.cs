using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace DeletePdfAnnotations
{
    class Program
    {
        static void Main(string[] args)
        {
            // ------------------------------------------------------------
            // Argument handling
            // ------------------------------------------------------------
            if (args.Length < 1 || args.Length > 2)
            {
                Console.WriteLine("Usage: DeletePdfAnnotations <inputFolder> [outputFolder]");
                return;
            }

            string inputFolder = args[0];
            string outputFolder = args.Length == 2 ? args[1] : inputFolder;

            // ------------------------------------------------------------
            // Validate folders
            // ------------------------------------------------------------
            if (!Directory.Exists(inputFolder))
            {
                Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
                return;
            }

            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // ------------------------------------------------------------
            // Process each PDF file
            // ------------------------------------------------------------
            foreach (string sourceFile in Directory.EnumerateFiles(inputFolder, "*.pdf"))
            {
                string destFile = Path.Combine(outputFolder, Path.GetFileName(sourceFile));

                try
                {
                    // Use the PdfAnnotationEditor facade to delete annotations
                    using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                    {
                        editor.BindPdf(sourceFile);
                        editor.DeleteAnnotations();
                        editor.Save(destFile);
                    }

                    Console.WriteLine($"Processed: {Path.GetFileName(sourceFile)}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to process '{sourceFile}': {ex.Message}");
                }
            }

            Console.WriteLine($"Annotation removal completed. Processed files are in: {outputFolder}");
        }
    }
}
