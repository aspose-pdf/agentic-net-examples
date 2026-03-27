using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string csvPath = "bookmarks.csv";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV not found: {csvPath}");
            return;
        }

        // Parse CSV and build hierarchical bookmarks
        Bookmarks rootBookmarks = new Bookmarks();
        Dictionary<int, Bookmark> levelMap = new Dictionary<int, Bookmark>();

        using (StreamReader reader = new StreamReader(csvPath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                string[] parts = line.Split(',');
                if (parts.Length < 3)
                {
                    continue;
                }
                string title = parts[0].Trim();
                int level = int.Parse(parts[1].Trim());
                int pageNumber = int.Parse(parts[2].Trim());

                Bookmark bm = new Bookmark();
                bm.Title = title;
                bm.PageNumber = pageNumber;
                bm.Action = "GoTo";

                if (level == 1)
                {
                    rootBookmarks.Add(bm);
                }
                else
                {
                    if (levelMap.TryGetValue(level - 1, out Bookmark parent))
                    {
                        if (parent.ChildItem == null)
                        {
                            parent.ChildItem = new Bookmarks();
                        }
                        parent.ChildItem.Add(bm);
                    }
                }

                // Update level map for current level
                levelMap[level] = bm;
                // Remove deeper level entries
                List<int> keysToRemove = new List<int>();
                foreach (int key in levelMap.Keys)
                {
                    if (key > level)
                    {
                        keysToRemove.Add(key);
                    }
                }
                foreach (int key in keysToRemove)
                {
                    levelMap.Remove(key);
                }
            }
        }

        // Apply bookmarks to the PDF document
        using (Document doc = new Document(inputPdf))
        {
            PdfBookmarkEditor editor = new PdfBookmarkEditor(doc);
            foreach (Bookmark topBookmark in rootBookmarks)
            {
                editor.CreateBookmarks(topBookmark);
            }
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Bookmarks imported and saved to '{outputPdf}'.");
    }
}
