using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the source PDF files
        const string inputFolder = @"C:\InputPdfs";
        // Root folder where each booklet will be placed in its own subdirectory
        const string outputRoot = @"C:\BookletOutputs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure the root output directory exists
        Directory.CreateDirectory(outputRoot);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        // Process each PDF file
        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Create a dedicated output directory for this booklet
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
                string bookletDir = Path.Combine(outputRoot, fileNameWithoutExt);
                Directory.CreateDirectory(bookletDir);

                // Destination booklet file
                string bookletPath = Path.Combine(bookletDir, "booklet.pdf");

                // Use PdfFileEditor (Facades API) to create the booklet.
                // PdfFileEditor does NOT implement IDisposable, so no using block.
                PdfFileEditor editor = new PdfFileEditor();

                // Simple booklet creation – default settings.
                // Use the MakeBooklet overload (no PageSize argument).
                bool success = editor.MakeBooklet(pdfPath, bookletPath);
                if (!success)
                {
                    Console.Error.WriteLine($"Failed to create booklet for '{pdfPath}'. MakeBooklet returned false.");
                }
                else
                {
                    Console.WriteLine($"Booklet created: {bookletPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create booklet for '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("All booklets processed.");
    }
}
