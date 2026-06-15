using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // List of encrypted PDF files to process
        string[] inputFiles = { "encrypted1.pdf", "encrypted2.pdf" };
        // Password that unlocks the PDFs (same for all files in this example)
        const string password = "userPassword";

        foreach (string inputPath in inputFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Output file name – original name with a suffix
            string outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_nobookmarks.pdf");

            // Open the encrypted PDF using the password
            using (Document doc = new Document(inputPath, password))
            {
                // Initialize the bookmark editor with the opened document
                using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor(doc))
                {
                    // Delete all bookmarks
                    bookmarkEditor.DeleteBookmarks();

                    // Save the result to a new file
                    bookmarkEditor.Save(outputPath);
                }
            }

            Console.WriteLine($"Bookmarks removed: {inputPath} → {outputPath}");
        }
    }
}