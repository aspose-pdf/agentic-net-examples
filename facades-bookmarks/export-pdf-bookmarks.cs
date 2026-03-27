using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;

public class BookmarkInfo
{
    public string Title { get; set; }
    public int Level { get; set; }
    public int PageNumber { get; set; }
    public List<BookmarkInfo> Children { get; set; }
}

public class Program
{
    public static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bookmarks.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPath);
            return;
        }

        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPath);
            Bookmarks rootBookmarks = editor.ExtractBookmarks();

            List<BookmarkInfo> jsonRoot = new List<BookmarkInfo>();
            foreach (Bookmark bm in rootBookmarks)
            {
                BookmarkInfo info = ConvertBookmark(bm, 1);
                jsonRoot.Add(info);
            }

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            string json = JsonSerializer.Serialize(jsonRoot, options);
            File.WriteAllText(outputPath, json);
        }

        Console.WriteLine("Bookmarks exported to " + outputPath);
    }

    private static BookmarkInfo ConvertBookmark(Bookmark bm, int level)
    {
        BookmarkInfo info = new BookmarkInfo();
        info.Title = bm.Title;
        info.Level = level;
        info.PageNumber = bm.PageNumber;
        info.Children = new List<BookmarkInfo>();

        if (bm.ChildItem != null)
        {
            foreach (Bookmark child in bm.ChildItem)
            {
                BookmarkInfo childInfo = ConvertBookmark(child, level + 1);
                info.Children.Add(childInfo);
            }
        }

        return info;
    }
}