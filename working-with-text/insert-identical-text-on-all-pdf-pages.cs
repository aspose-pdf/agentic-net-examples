using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string textToInsert = "Sample plain text";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a plain text fragment
                TextFragment tf = new TextFragment(textToInsert);

                // Insert the text fragment into the page's Paragraphs collection
                page.Paragraphs.Add(tf);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Inserted text on all pages. Saved to '{outputPath}'.");
    }
}