using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Use PdfPageEditor for page‑level transition editing
        using (PdfPageEditor editor = new PdfPageEditor(doc))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Retrieve a custom title for the current page from the document's Info dictionary.
                // Expected key format: "Title_1", "Title_2", ...
                string titleKey = $"Title_{i}";
                string title = doc.Info[titleKey] ?? string.Empty;
                int titleLength = title.Length;

                // Compute transition duration: 1 s + 1 s per 10 characters, capped at 5 s
                int duration = 1 + titleLength / 10;
                if (duration > 5) duration = 5;

                // Configure the editor for the current page
                editor.ProcessPages = new int[] { i };
                editor.TransitionType = 4;          // 4 = Wipe transition (integer value from Aspose.Pdf enum)
                editor.TransitionDuration = duration;

                // Apply the transition to the page
                editor.ApplyChanges();
            }

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF with page transitions saved to '{outputPath}'.");
    }
}
