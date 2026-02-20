using System;
using System.IO;
using Aspose.Pdf.Facades;

class DeleteBookmarkExample
{
    static void Main(string[] args)
    {
        // Expected arguments: input PDF path, bookmark title to delete, output PDF path
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: DeleteBookmarkExample <inputPdf> <bookmarkTitle> <outputPdf>");
            return;
        }

        string inputPath = args[0];
        string bookmarkTitle = args[1];
        string outputPath = args[2];

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input PDF file not found: {inputPath}");
            return;
        }

        try
        {
            // Create the PdfBookmarkEditor and bind the PDF document
            using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
            {
                bookmarkEditor.BindPdf(inputPath);

                // Delete the bookmark with the specified title
                bookmarkEditor.DeleteBookmarks(bookmarkTitle);

                // Save the modified PDF to the output path
                bookmarkEditor.Save(outputPath);
            }

            Console.WriteLine($"Bookmark \"{bookmarkTitle}\" deleted successfully. Output saved to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}