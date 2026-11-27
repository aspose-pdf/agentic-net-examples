using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bookmarked_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF to obtain page count (Document implements IDisposable)
        using (Document doc = new Document(inputPath))
        {
            // Instantiate the bookmark editor and bind it to the loaded document
            using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
            {
                editor.BindPdf(doc);

                // Create a blue, bold bookmark for each page.
                // For page 6 we also add a child bookmark titled "Details".
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    // Top‑level bookmark for the current page
                    Bookmark pageBookmark = new Bookmark
                    {
                        Title = $"Page {i}",
                        PageNumber = i,
                        TitleColor = System.Drawing.Color.Blue, // fully qualified to avoid ambiguity
                        BoldFlag = true,
                        ItalicFlag = false
                    };

                    // If this is page 6, add a child bookmark under it
                    if (i == 6)
                    {
                        Bookmark child = new Bookmark
                        {
                            Title = "Details",
                            PageNumber = 6,
                            TitleColor = System.Drawing.Color.Blue,
                            BoldFlag = true,
                            ItalicFlag = false
                        };
                        pageBookmark.ChildItems.Add(child);
                    }

                    // Add the constructed bookmark (with any children) to the document
                    editor.CreateBookmarks(pageBookmark);
                }

                // Save the PDF with the new bookmark structure
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Bookmarked PDF saved to '{outputPath}'.");
    }
}
