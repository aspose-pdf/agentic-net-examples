using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace PdfPageDeletion
{
    class Program
    {
        static void Main()
        {
            // Resolve source and output folders relative to the executable location.
            // This makes the code platform‑agnostic and avoids hard‑coded Windows paths.
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string sourceFolder = Path.GetFullPath(Path.Combine(baseDir, "SourcePdfs"));
            string outputFolder = Path.GetFullPath(Path.Combine(baseDir, "OutputPdfs"));

            // Verify that the source folder exists before trying to enumerate files.
            if (!Directory.Exists(sourceFolder))
            {
                Console.Error.WriteLine($"Source folder does not exist: {sourceFolder}");
                return;
            }

            // Ensure the output directory exists.
            Directory.CreateDirectory(outputFolder);

            // Process each PDF file in the source folder.
            foreach (string inputFilePath in Directory.GetFiles(sourceFolder, "*.pdf"))
            {
                string fileName = Path.GetFileName(inputFilePath);
                string outputFilePath = Path.Combine(outputFolder, fileName);

                // PdfFileEditor does not implement IDisposable, so a simple instance is sufficient.
                var editor = new PdfFileEditor();

                // Delete pages 3 and 4 (1‑based indexing) and save the result.
                // The Delete method returns void, so we just invoke it.
                editor.Delete(inputFilePath, new[] { 3, 4 }, outputFilePath);

                // Verify that the output file was created.
                if (!File.Exists(outputFilePath))
                {
                    Console.Error.WriteLine($"Failed to delete pages from '{fileName}'.");
                }
                else
                {
                    Console.WriteLine($"Processed '{fileName}' → '{outputFilePath}'.");
                }
            }
        }
    }
}
