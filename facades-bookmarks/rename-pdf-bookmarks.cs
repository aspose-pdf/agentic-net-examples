using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Translation dictionary: original bookmark title -> translated title
        Dictionary<string, string> translation = new Dictionary<string, string>();
        translation["Chapter 1"] = "Capítulo 1";
        translation["Chapter 2"] = "Capítulo 2";
        // Add more entries as required

        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPath);

            foreach (KeyValuePair<string, string> pair in translation)
            {
                editor.ModifyBookmarks(pair.Key, pair.Value);
            }

            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks renamed and saved to '{outputPath}'.");
    }
}