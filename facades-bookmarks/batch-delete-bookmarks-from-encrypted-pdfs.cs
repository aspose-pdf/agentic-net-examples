using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        // 1. Input folder containing encrypted PDFs
        // 2. Password to open the PDFs (user or owner password)
        // 3. Output folder where processed PDFs will be saved
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <inputFolder> <password> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string password = args[1];
        string outputFolder = args[2];

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        foreach (string inputPath in pdfFiles)
        {
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Open the encrypted PDF with the supplied password.
                using (Document doc = new Document(inputPath, password))
                {
                    // Initialize the bookmark editor on the opened document.
                    using (PdfBookmarkEditor editor = new PdfBookmarkEditor(doc))
                    {
                        // Delete all bookmarks.
                        editor.DeleteBookmarks();

                        // Save the modified PDF (overwrites or creates a new file).
                        editor.Save(outputPath);
                    }
                }

                Console.WriteLine($"Processed: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to process '{fileName}': {ex.Message}");
            }
        }

        Console.WriteLine("Bookmark removal completed.");
    }
}