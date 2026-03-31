using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Example chapter definitions: each array element defines the start page of a chapter.
        // Adjust these arrays to match the actual chapter boundaries of your document.
        int[] chapterStartPages = new int[] { 1, 6, 11 };
        int[] chapterEndPages   = new int[] { 5, 10, 15 };

        using (Document sourceDoc = new Document(inputPath))
        {
            int chapterIndex = 1;
            for (int i = 0; i < chapterStartPages.Length; i++)
            {
                using (Document chapterDoc = new Document())
                {
                    int start = chapterStartPages[i];
                    int end   = chapterEndPages[i];

                    for (int pageNumber = start; pageNumber <= end && pageNumber <= sourceDoc.Pages.Count; pageNumber++)
                    {
                        // Add a copy of the source page to the chapter document.
                        Page copiedPage = chapterDoc.Pages.Add(sourceDoc.Pages[pageNumber]);
                        // Apply uniform scaling (e.g., 90% of original size) using PageInfo.
                        copiedPage.PageInfo.Width  = copiedPage.PageInfo.Width  * 0.9f;
                        copiedPage.PageInfo.Height = copiedPage.PageInfo.Height * 0.9f;
                    }

                    string outputFileName = $"chapter{chapterIndex}.pdf";
                    chapterDoc.Save(outputFileName);
                    Console.WriteLine($"Saved {outputFileName}");
                }
                chapterIndex++;
            }
        }
    }
}
