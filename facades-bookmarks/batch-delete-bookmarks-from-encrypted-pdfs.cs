using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Define the encrypted PDFs, their passwords and the desired output files.
        var files = new[]
        {
            new { Input = "encrypted1.pdf", Output = "clean1.pdf", Password = "userPass1" },
            new { Input = "encrypted2.pdf", Output = "clean2.pdf", Password = "userPass2" }
            // Add more entries as needed.
        };

        foreach (var fileInfo in files)
        {
            if (!File.Exists(fileInfo.Input))
            {
                Console.Error.WriteLine($"Input file not found: {fileInfo.Input}");
                continue;
            }

            // Open the encrypted PDF using the correct password.
            // Document(string path, string password) decrypts the file for further processing.
            using (Document doc = new Document(fileInfo.Input, fileInfo.Password))
            {
                // Initialize the bookmark editor with the opened document.
                using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor(doc))
                {
                    // Delete all bookmarks from the document.
                    bookmarkEditor.DeleteBookmarks();

                    // Save the modified PDF to the specified output path.
                    bookmarkEditor.Save(fileInfo.Output);
                }
            }

            Console.WriteLine($"Bookmarks removed and saved to: {fileInfo.Output}");
        }
    }
}