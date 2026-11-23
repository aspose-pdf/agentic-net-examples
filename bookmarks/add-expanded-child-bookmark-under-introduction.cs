using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "noteexpanded.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Locate the outline (bookmark) titled "Introduction"
            OutlineItemCollection? introBookmark = null;
            foreach (OutlineItemCollection item in doc.Outlines)
            {
                if (string.Equals(item.Title, "Introduction", StringComparison.OrdinalIgnoreCase))
                {
                    introBookmark = item;
                    break;
                }
            }

            if (introBookmark == null)
            {
                Console.Error.WriteLine("Bookmark 'Introduction' not found.");
                return;
            }

            // Create a child bookmark under "Introduction"
            OutlineItemCollection noteBookmark = new OutlineItemCollection(doc.Outlines)
            {
                Title = "Note",
                Open = true // Ensure the new bookmark is expanded when the PDF is opened
            };

            // Add the child to the parent outline collection
            introBookmark.Add(noteBookmark);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
