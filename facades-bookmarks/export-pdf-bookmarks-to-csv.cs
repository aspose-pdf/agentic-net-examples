using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;   // Facade API for bookmark handling
using Aspose.Pdf;           // Core API (required for Destination type)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputCsv = "bookmarks.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Bind the PDF to the bookmark editor (no IDisposable required)
            PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();
            bookmarkEditor.BindPdf(inputPdf);

            // Extract all bookmarks; each Bookmark contains Title, Destination and Level
            IList<Bookmark> bookmarks = bookmarkEditor.ExtractBookmarks();

            // Write CSV header and rows
            using (StreamWriter writer = new StreamWriter(outputCsv))
            {
                writer.WriteLine("Title,Destination,Level"); // CSV header

                foreach (Bookmark bm in bookmarks)
                {
                    // Destination: use the raw destination string (it may be a page number, named destination, etc.)
                    string destination = bm.Destination ?? string.Empty;

                    // Escape title for CSV (double quotes and commas)
                    string title = bm.Title ?? string.Empty;
                    title = title.Replace("\"", "\"\""); // double up quotes
                    if (title.Contains(",") || title.Contains("\""))
                    {
                        title = $"\"{title}\"";
                    }

                    writer.WriteLine($"{title},{destination},{bm.Level}");
                }
            }

            Console.WriteLine($"Bookmarks extracted to '{outputCsv}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
