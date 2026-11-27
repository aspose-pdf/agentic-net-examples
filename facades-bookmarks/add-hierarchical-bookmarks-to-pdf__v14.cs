using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
            {
                // Bind the source PDF
                editor.BindPdf(inputPath);

                // Create the parent bookmark (page 3)
                Bookmark parent = new Bookmark
                {
                    Title = "Chapter 8",
                    PageNumber = 3,
                    Action = "GoTo"
                };

                // Create the child bookmark (page 4)
                Bookmark child = new Bookmark
                {
                    Title = "Section 8.1",
                    PageNumber = 4,
                    Action = "GoTo"
                };

                // Attach child to parent
                parent.ChildItems = new Bookmarks();
                parent.ChildItems.Add(child);

                // Add the bookmark hierarchy to the PDF
                editor.CreateBookmarks(parent);

                // Save the modified PDF
                editor.Save(outputPath);
                editor.Close();
            }

            Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
