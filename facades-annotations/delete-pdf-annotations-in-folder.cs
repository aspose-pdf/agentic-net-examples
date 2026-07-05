using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfAnnotationRemover
{
    class Program
    {
        /// <summary>
        /// Deletes all annotations from every PDF file in a source folder.
        /// Usage: AsposePdfAnnotationRemover.exe <sourceFolder> [destinationFolder]
        /// If destinationFolder is omitted, the original files are overwritten.
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: AsposePdfAnnotationRemover.exe <sourceFolder> [destinationFolder]");
                return;
            }

            string sourceFolder = args[0];
            string destinationFolder = args.Length > 1 ? args[1] : sourceFolder;

            if (!Directory.Exists(sourceFolder))
            {
                Console.WriteLine($"Source folder does not exist: {sourceFolder}");
                return;
            }

            // Ensure the destination folder exists (creates it if necessary)
            Directory.CreateDirectory(destinationFolder);

            string[] pdfFiles = Directory.GetFiles(sourceFolder, "*.pdf", SearchOption.TopDirectoryOnly);
            if (pdfFiles.Length == 0)
            {
                Console.WriteLine("No PDF files found in the source folder.");
                return;
            }

            foreach (string inputPath in pdfFiles)
            {
                string fileName = Path.GetFileName(inputPath);
                string outputPath = Path.Combine(destinationFolder, fileName);

                try
                {
                    // Use PdfAnnotationEditor to delete all annotations
                    using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                    {
                        editor.BindPdf(inputPath);
                        editor.DeleteAnnotations();
                        editor.Save(outputPath);
                    }

                    Console.WriteLine($"Processed: {fileName} -> {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing {fileName}: {ex.Message}");
                }
            }
        }
    }
}
