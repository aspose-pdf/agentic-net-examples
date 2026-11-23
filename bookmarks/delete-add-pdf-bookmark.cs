using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "dedicationrestored.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Locate and remove the bookmark titled "Dedication Updated"
            OutlineItemCollection itemToRemove = null;
            foreach (OutlineItemCollection item in doc.Outlines)
            {
                if (item.Title == "Dedication Updated")
                {
                    itemToRemove = item;
                    break;
                }
            }
            if (itemToRemove != null)
                doc.Outlines.Remove(itemToRemove);

            // Add a new top‑level bookmark "Dedication" that points to page 1
            OutlineItemCollection newBookmark = new OutlineItemCollection(doc.Outlines)
            {
                Title = "Dedication",
                // Use a GoToAction to jump to the first page
                Action = new GoToAction(doc.Pages[1])
            };
            doc.Outlines.Add(newBookmark);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved restored PDF to '{outputPath}'.");
    }
}
