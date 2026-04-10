using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing encrypted PDFs
        const string inputFolder = "EncryptedPdfs";
        // Folder where PDFs without bookmarks will be saved
        const string outputFolder = "BookmarksRemoved";

        // Password that unlocks the PDFs (owner or user password)
        const string password = "ownerPass";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string filePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(filePath);
            string outPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the encrypted PDF using the supplied password
                using (Document doc = new Document(filePath, password))
                {
                    // Bind the loaded document to the bookmark editor
                    using (PdfBookmarkEditor editor = new PdfBookmarkEditor(doc))
                    {
                        // Remove all bookmarks from the document
                        editor.DeleteBookmarks();

                        // Save the result to the output path
                        editor.Save(outPath);
                    }
                }

                Console.WriteLine($"Bookmarks removed: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {fileName}: {ex.Message}");
            }
        }
    }
}