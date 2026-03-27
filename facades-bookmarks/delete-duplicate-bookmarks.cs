using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

public class Program
{
    public static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            HashSet<string> seenTitles = new HashSet<string>(StringComparer.Ordinal);
            for (int i = doc.Outlines.Count - 1; i >= 0; i--)
            {
                OutlineItemCollection outline = doc.Outlines[i];
                string title = outline.Title;
                if (seenTitles.Contains(title))
                {
                    outline.Delete();
                }
                else
                {
                    seenTitles.Add(title);
                }
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Duplicate bookmarks removed. Saved to '{outputPath}'.");
    }
}
