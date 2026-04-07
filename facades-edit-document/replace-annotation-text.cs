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
        const string sourceText = "old text";
        const string replacementText = "new text";

        // Pages on which to replace annotation text (1‑based)
        int[] targetPages = new int[] { 1, 2 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            foreach (int pageNumber in targetPages)
            {
                if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                {
                    continue;
                }

                Page page = doc.Pages[pageNumber];
                foreach (Annotation annotation in page.Annotations)
                {
                    if (annotation.Contents != null && annotation.Contents.Contains(sourceText))
                    {
                        annotation.Contents = annotation.Contents.Replace(sourceText, replacementText);
                    }
                }
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations updated and saved to '{outputPath}'.");
    }
}