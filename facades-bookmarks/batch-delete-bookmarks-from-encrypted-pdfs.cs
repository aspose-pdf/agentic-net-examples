using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the encrypted PDFs
        const string inputFolder = @"C:\PdfFiles";
        // Password that unlocks all PDFs in the folder
        const string pdfPassword = "mySecretPassword";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Process each PDF file in the folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Load the encrypted PDF using Document constructor that accepts a password
                Document pdfDoc = new Document(pdfPath, pdfPassword);

                // PdfBookmarkEditor works with a Document instance via BindPdf(Document)
                using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
                {
                    bookmarkEditor.BindPdf(pdfDoc);

                    // Delete all existing bookmarks (no‑argument overload)
                    bookmarkEditor.DeleteBookmarks();

                    // Save the changes back to the same file (overwrites the original)
                    bookmarkEditor.Save(pdfPath);
                }

                Console.WriteLine($"Bookmarks removed: {Path.GetFileName(pdfPath)}");
            }
            catch (InvalidPasswordException)
            {
                Console.Error.WriteLine($"Invalid password for file: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{Path.GetFileName(pdfPath)}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch bookmark removal completed.");
    }
}
