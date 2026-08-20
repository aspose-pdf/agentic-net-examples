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
        // Password that unlocks the PDFs (user password)
        const string userPassword = "user123";

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
                Path.GetFileNameWithoutExtension(inputPath) + "_noBookmarks.pdf");

            // Open the encrypted PDF with the password
            using (Document doc = new Document(inputPath, userPassword))
            {
                // Bind the opened document to the bookmark editor
                PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor(doc);

                // Delete all bookmarks in the document
                bookmarkEditor.DeleteBookmarks();

                // Save the modified PDF (the editor saves the underlying document)
                bookmarkEditor.Save(outputPath);
            }

            Console.WriteLine($"Bookmarks removed: {inputPath} → {outputPath}");
        }
    }
}