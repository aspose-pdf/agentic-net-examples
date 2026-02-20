using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: input PDF path, output PDF path, bookmark title, page number
        if (args.Length < 4)
        {
            Console.WriteLine("Usage: <inputPdf> <outputPdf> <bookmarkTitle> <pageNumber>");
            return;
        }

        string inputPdf = args[0];
        string outputPdf = args[1];
        string bookmarkTitle = args[2];
        if (!int.TryParse(args[3], out int pageNumber) || pageNumber < 1)
        {
            Console.WriteLine("Invalid page number. It must be a positive integer.");
            return;
        }

        // Verify that the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.WriteLine($"Error: Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Initialize the bookmark editor facade
            using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
            {
                // Bind the existing PDF document
                bookmarkEditor.BindPdf(inputPdf);

                // Create a bookmark for the specified page
                // The method creates a bookmark with the given title pointing to the page number
                bookmarkEditor.CreateBookmarkOfPage(bookmarkTitle, pageNumber);

                // Save the modified PDF to the output path
                bookmarkEditor.Save(outputPdf);
            }

            Console.WriteLine($"Bookmark \"{bookmarkTitle}\" (page {pageNumber}) added successfully to {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while processing the PDF: {ex.Message}");
        }
    }
}