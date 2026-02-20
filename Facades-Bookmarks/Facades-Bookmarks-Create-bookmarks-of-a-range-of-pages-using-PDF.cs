using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDoc = new Document(inputPath);

            // Define the page range for which bookmarks will be created (e.g., pages 2 to 5)
            int startPage = 2;
            int endPage = 5;

            // Initialize the PdfBookmarkEditor and bind it to the loaded document
            using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
            {
                bookmarkEditor.BindPdf(pdfDoc);

                // Ensure the range is valid within the document
                if (startPage < 1 || endPage > pdfDoc.Pages.Count || startPage > endPage)
                {
                    Console.Error.WriteLine("Error: Invalid page range specified.");
                    return;
                }

                // Prepare arrays of titles and corresponding page numbers
                int rangeSize = endPage - startPage + 1;
                string[] titles = new string[rangeSize];
                int[] pages = new int[rangeSize];
                for (int i = 0; i < rangeSize; i++)
                {
                    int pageNumber = startPage + i;
                    titles[i] = $"Page {pageNumber}";
                    pages[i] = pageNumber;
                }

                // Create bookmarks for the specified range of pages
                bookmarkEditor.CreateBookmarkOfPage(titles, pages);
            }

            // Save the modified PDF document
            pdfDoc.Save(outputPath);
            Console.WriteLine($"Bookmarks for pages {startPage}-{endPage} created successfully. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
