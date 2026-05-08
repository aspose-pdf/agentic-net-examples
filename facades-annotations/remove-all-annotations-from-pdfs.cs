using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfAnnotationRemover
{
    class Program
    {
        static void Main(string[] args)
        {
            // ---------------------------------------------------------------
            // Validate arguments
            // ---------------------------------------------------------------
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: AsposePdfAnnotationRemover <FolderPath> [OutputFolder]");
                return;
            }

            string folderPath = args[0];
            string outputFolder = args.Length > 1 ? args[1] : folderPath;

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Input folder does not exist: {folderPath}");
                return;
            }

            // ---------------------------------------------------------------
            // Ensure the output folder exists (creates it if necessary)
            // ---------------------------------------------------------------
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            // ---------------------------------------------------------------
            // Process each PDF file in the folder
            // ---------------------------------------------------------------
            foreach (string pdfFile in Directory.GetFiles(folderPath, "*.pdf"))
            {
                string outputFile = Path.Combine(outputFolder, Path.GetFileName(pdfFile));

                // PdfAnnotationEditor implements IDisposable, so we use a using block.
                using (var editor = new PdfAnnotationEditor())
                {
                    // Bind the source PDF.
                    editor.BindPdf(pdfFile);

                    // Delete **all** annotations in the document.
                    editor.DeleteAnnotations();

                    // Save the cleaned PDF. The Save method overwrites the file if it already exists.
                    editor.Save(outputFile);
                }

                Console.WriteLine($"Processed: {pdfFile} -> {outputFile}");
            }

            Console.WriteLine("Annotation removal completed.");
        }
    }
}
