using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfAnnotationRemover
{
    class Program
    {
        static void Main(string[] args)
        {
            // Expected arguments: <SourceFolder> [DestinationFolder]
            if (args.Length < 1 || args.Length > 2)
            {
                Console.WriteLine("Usage: AsposePdfAnnotationRemover <SourceFolder> [DestinationFolder]");
                return;
            }

            string sourceFolder = args[0];
            string destinationFolder = args.Length == 2 ? args[1] : sourceFolder;

            // Verify source folder exists
            if (!Directory.Exists(sourceFolder))
            {
                Console.Error.WriteLine($"Source folder '{sourceFolder}' does not exist.");
                return;
            }

            // Ensure destination folder exists
            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }

            // Process each PDF file in the source folder
            foreach (string pdfPath in Directory.GetFiles(sourceFolder, "*.pdf"))
            {
                string fileName = Path.GetFileNameWithoutExtension(pdfPath);
                string outputPath = Path.Combine(destinationFolder, $"{fileName}_noann.pdf");

                // Use PdfAnnotationEditor to delete all annotations
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(pdfPath);
                    editor.DeleteAnnotations();
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)} -> {outputPath}");
            }
        }
    }
}
