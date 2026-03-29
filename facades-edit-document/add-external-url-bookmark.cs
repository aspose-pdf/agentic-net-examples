using System;
using System.IO;
using System.Drawing; // Added for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string bookmarkTitle = "Example Site";
        const string url = "https://example.org";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
            {
                // Bind the existing PDF document
                editor.BindPdf(inputPath);

                // Create a bookmark that launches an external URL
                Bookmark bm = new Bookmark();
                bm.Title = bookmarkTitle;
                bm.TitleColor = System.Drawing.Color.Blue; // Fixed: use System.Drawing.Color
                bm.BoldFlag = true;
                bm.ItalicFlag = false;
                bm.Action = "URI";               // Action type for external URL
                bm.Destination = url;            // The URL to open

                // Add the bookmark to the document
                editor.CreateBookmarks(bm);

                // Save the modified PDF
                editor.Save(outputPath);
                editor.Close();
            }

            Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
