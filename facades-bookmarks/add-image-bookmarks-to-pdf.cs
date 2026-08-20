using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Collect page numbers for each image in the document
        var imageLocations = new List<(int pageNumber, int imageIndex)>();
        using (Document doc = new Document(inputPath))
        {
            int imgCounter = 1;
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                foreach (XImage img in page.Resources.Images)
                {
                    imageLocations.Add((i, imgCounter));
                    imgCounter++;
                }
            }
        }

        // Create bookmarks that point to the pages containing images
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);
        foreach (var loc in imageLocations)
        {
            string bookmarkName = $"Image {loc.imageIndex} (Page {loc.pageNumber})";
            editor.CreateBookmarkOfPage(bookmarkName, loc.pageNumber);
        }
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Bookmarks added. Output saved to '{outputPath}'.");
    }
}