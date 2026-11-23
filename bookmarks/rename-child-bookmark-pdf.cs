using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "childtitle.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Find the top‑level bookmark titled "Chapter Two"
            OutlineItemCollection chapterTwo = null;
            foreach (OutlineItemCollection topItem in doc.Outlines)
            {
                if (string.Equals(topItem.Title, "Chapter Two", StringComparison.Ordinal))
                {
                    chapterTwo = topItem;
                    break;
                }
            }

            if (chapterTwo == null)
            {
                Console.Error.WriteLine("Bookmark 'Chapter Two' not found.");
                return;
            }

            // Within "Chapter Two", find the child bookmark titled "Section 2.2"
            OutlineItemCollection section = null;
            // An OutlineItemCollection is its own collection of children, so iterate it directly
            foreach (OutlineItemCollection child in chapterTwo)
            {
                if (string.Equals(child.Title, "Section 2.2", StringComparison.Ordinal))
                {
                    section = child;
                    break;
                }
            }

            if (section == null)
            {
                Console.Error.WriteLine("Child bookmark 'Section 2.2' not found under 'Chapter Two'.");
                return;
            }

            // Rename the child bookmark
            section.Title = "Section 2.2 Revised";

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bookmark renamed and saved to '{outputPath}'.");
    }
}
