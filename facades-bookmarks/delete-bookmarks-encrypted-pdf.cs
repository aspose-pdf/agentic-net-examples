using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";
        const string outputPath = "cleaned.pdf";
        const string password = "user123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document document = new Document(inputPath, password))
            {
                using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor(document))
                {
                    bookmarkEditor.DeleteBookmarks();
                    bookmarkEditor.Save(outputPath);
                }
            }

            Console.WriteLine($"All bookmarks removed. Saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}