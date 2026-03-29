using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string srcText = "old annotation text";
        const string destText = "new annotation text";

        // Specify the pages on which annotation text should be replaced (1‑based indexing)
        int[] pagesToProcess = new int[] { 1, 2 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            foreach (int pageNumber in pagesToProcess)
            {
                if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                {
                    continue; // skip invalid page numbers
                }

                Page page = doc.Pages[pageNumber];
                for (int i = 1; i <= page.Annotations.Count; i++)
                {
                    Annotation annotation = page.Annotations[i];
                    if (!string.IsNullOrEmpty(annotation.Contents) && annotation.Contents.Contains(srcText))
                    {
                        annotation.Contents = annotation.Contents.Replace(srcText, destText);
                    }
                }
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations updated and saved to '{outputPath}'.");
    }
}