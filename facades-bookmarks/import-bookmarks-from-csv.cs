using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";      // source PDF
        const string csvPath   = "bookmarks.csv";  // CSV with Title,Level,Page
        const string outPath   = "output.pdf";     // PDF with imported bookmarks

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV not found: {csvPath}");
            return;
        }

        // Initialize the bookmark editor and bind the source PDF
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(pdfPath);

            // Parse CSV and build a hierarchical bookmark structure
            List<Bookmark> topLevelBookmarks = new List<Bookmark>();
            Stack<Bookmark> hierarchyStack = new Stack<Bookmark>();

            using (StreamReader sr = new StreamReader(csvPath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        continue; // skip empty lines

                    // Expected CSV format: Title,Level,PageNumber
                    string[] parts = line.Split(',');
                    if (parts.Length < 3)
                        continue; // malformed line – ignore

                    string title = parts[0].Trim();
                    if (!int.TryParse(parts[1].Trim(), out int level) || level < 1)
                        continue; // invalid level – ignore
                    if (!int.TryParse(parts[2].Trim(), out int pageNumber) || pageNumber < 1)
                        continue; // invalid page – ignore

                    // Create a new bookmark for this entry
                    Bookmark bm = new Bookmark
                    {
                        Title      = title,
                        PageNumber = pageNumber
                    };

                    // Adjust the stack to match the current level
                    while (hierarchyStack.Count >= level)
                        hierarchyStack.Pop();

                    if (hierarchyStack.Count == 0)
                    {
                        // This is a top‑level bookmark
                        topLevelBookmarks.Add(bm);
                    }
                    else
                    {
                        // Attach as a child of the current parent
                        Bookmark parent = hierarchyStack.Peek();
                        if (parent.ChildItem == null)
                            parent.ChildItem = new Bookmarks();

                        parent.ChildItem.Add(bm);
                    }

                    // Push the current bookmark onto the stack
                    hierarchyStack.Push(bm);
                }
            }

            // Add the constructed bookmarks to the PDF
            foreach (Bookmark topBm in topLevelBookmarks)
            {
                editor.CreateBookmarks(topBm);
            }

            // Save the resulting PDF
            editor.Save(outPath);
            // Optional: close the editor (handled by using)
        }

        Console.WriteLine($"Bookmarks imported and saved to '{outPath}'.");
    }
}