using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputCsv = "bookmarks.csv";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF to the PdfBookmarkEditor (Facades API)
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPdf);

            // Extract the collection of bookmarks
            var bookmarks = editor.ExtractBookmarks();

            // Create (or overwrite) the CSV file
            using (StreamWriter writer = new StreamWriter(outputCsv, false, Encoding.UTF8))
            {
                // CSV header
                writer.WriteLine("Title,Destination,Level");

                // Local function to write a bookmark and its children recursively
                void WriteBookmark(Bookmark bm)
                {
                    // Escape commas and quotes for CSV compliance
                    string Escape(string s) => $"\"{s?.Replace("\"", "\"\"")}\"";

                    writer.WriteLine($"{Escape(bm.Title)},{Escape(bm.Destination)},{bm.Level}");

                    // Recursively process child bookmarks, if any
                    foreach (var child in bm.ChildItems)
                    {
                        WriteBookmark(child);
                    }
                }

                // Process all top‑level bookmarks
                foreach (var bm in bookmarks)
                {
                    WriteBookmark(bm);
                }
            }
        }

        Console.WriteLine($"Bookmarks exported to '{outputCsv}'.");
    }
}
