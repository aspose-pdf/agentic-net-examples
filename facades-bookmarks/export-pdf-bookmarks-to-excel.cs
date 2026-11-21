using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string csvPath = "bookmarks.csv"; // Excel can open CSV files

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // PdfBookmarkEditor does not implement IDisposable, so we instantiate it directly.
            var editor = new PdfBookmarkEditor();
            editor.BindPdf(pdfPath);

            // Extract the bookmarks from the PDF.
            Bookmarks bookmarks = editor.ExtractBookmarks();

            // Write bookmarks to a CSV file (compatible with Excel).
            using (var writer = new StreamWriter(csvPath))
            {
                // Header row
                writer.WriteLine("Title,Level,Destination");

                foreach (Bookmark bm in bookmarks)
                {
                    // Escape commas and quotes in the title.
                    string title = bm.Title ?? string.Empty;
                    title = title.Replace("\"", "\"\"");
                    if (title.Contains(",") || title.Contains("\""))
                    {
                        title = $"\"{title}\"";
                    }

                    writer.WriteLine($"{title},{bm.Level},{bm.PageNumber}");
                }
            }

            // Clean up the editor.
            editor.Close();

            Console.WriteLine($"Bookmarks successfully exported to '{csvPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}
