using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string targetPath = "target.pdf";
        const string sourcePath = "source.pdf";
        const string outputPath = "output.pdf";

        const int sourcePageNumber = 1; // 1‑based index of the page to copy from source PDF
        const int insertPosition = 2;   // 1‑based position where the page will be inserted in target PDF

        if (!File.Exists(targetPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPath}");
            return;
        }

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        try
        {
            using (Document targetDoc = new Document(targetPath))
            using (Document sourceDoc = new Document(sourcePath))
            {
                if (sourcePageNumber < 1 || sourcePageNumber > sourceDoc.Pages.Count)
                {
                    Console.Error.WriteLine("Invalid source page number.");
                    return;
                }

                Page pageToInsert = sourceDoc.Pages[sourcePageNumber];
                targetDoc.Pages.Insert(insertPosition, pageToInsert);
                targetDoc.Save(outputPath);
            }

            Console.WriteLine($"Page inserted and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}