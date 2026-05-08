using System;
using System.IO;
using Aspose.Pdf;               // Core API (Document, Page, PageLabel, NumberingStyle)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF (Document implements IDisposable, so wrap in using)
        using (Document doc = new Document(inputPath))
        {
            // Insert a blank page at the very beginning.
            // PageCollection.Insert expects a 1‑based position.
            doc.Pages.Insert(1);

            // Create a custom page label.
            // NumberingStyle.None means no automatic number is added;
            // Prefix supplies the literal label text.
            PageLabel customLabel = new PageLabel
            {
                NumberingStyle = NumberingStyle.None,
                Prefix = "i"
            };

            // PageLabelCollection uses zero‑based indexes.
            // Update the label for the newly inserted first page (index 0).
            doc.PageLabels.UpdateLabel(0, customLabel);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank page with label \"i\" added. Saved to '{outputPath}'.");
    }
}