using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bookmarks.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);
        Bookmarks rootBookmarks = editor.ExtractBookmarks();

        List<Dictionary<string, object>> jsonList = BuildBookmarkList(rootBookmarks);

        string json = JsonSerializer.Serialize(jsonList, new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText(outputPath, json);
        Console.WriteLine($"Bookmarks JSON saved to '{outputPath}'.");
    }

    static List<Dictionary<string, object>> BuildBookmarkList(Bookmarks bookmarks)
    {
        var list = new List<Dictionary<string, object>>();
        foreach (Bookmark bm in bookmarks)
        {
            var dict = new Dictionary<string, object>
            {
                ["title"] = bm.Title,
                ["page"] = bm.PageNumber
            };

            if (bm.ChildItem != null && bm.ChildItem.Count > 0)
            {
                dict["children"] = BuildBookmarkList(bm.ChildItem);
            }

            list.Add(dict);
        }
        return list;
    }
}
