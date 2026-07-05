using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input: folder containing encrypted PDFs
        const string inputFolder  = @"C:\InputEncryptedPdfs";
        // Output folder for PDFs with bookmarks removed
        const string outputFolder = @"C:\OutputNoBookmarks";
        // Password required to open the encrypted PDFs
        const string pdfPassword  = "mySecretPassword";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName   = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName + "_nobookmarks.pdf");

            try
            {
                // Load the encrypted PDF using the password
                using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath, pdfPassword))
                {
                    // Initialise the bookmark editor with the loaded document
                    using (Aspose.Pdf.Facades.PdfBookmarkEditor editor = new Aspose.Pdf.Facades.PdfBookmarkEditor(doc))
                    {
                        // Delete all bookmarks
                        editor.DeleteBookmarks();

                        // Save the modified PDF
                        editor.Save(outputPath);
                    }
                }

                Console.WriteLine($"Processed: {inputPath} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}