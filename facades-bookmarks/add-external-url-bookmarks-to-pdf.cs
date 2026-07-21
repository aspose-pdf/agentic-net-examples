using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "bookmarked_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Define bookmark titles and their target URLs
        string[] titles = { "Aspose Home", "GitHub", "Stack Overflow" };
        string[] urls   = { "https://www.aspose.com", "https://github.com", "https://stackoverflow.com" };

        // Ensure the arrays have the same length
        if (titles.Length != urls.Length)
        {
            Console.Error.WriteLine("Titles and URLs count mismatch.");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the bookmark editor and bind it to the document
            using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
            {
                editor.BindPdf(doc);

                // Create a bookmark for each URL
                for (int i = 0; i < titles.Length; i++)
                {
                    Bookmark bm = new Bookmark
                    {
                        Title      = titles[i],
                        Action     = "URI",          // specifies an external link
                        Destination = urls[i]        // the target web address
                    };

                    // Add the bookmark to the document
                    editor.CreateBookmarks(bm);
                }

                // Save the modified PDF
                editor.Save(outputPdf);
            }
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPdf}'.");
    }
}