using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class InsertTextOnAllPages
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string textToInsert = "Sample plain text";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through each page (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a TextFragment with the desired text
                TextFragment tf = new TextFragment(textToInsert);

                // Optionally set the position of the text on the page
                // (using Aspose.Pdf.Text.Position to avoid ambiguity)
                tf.Position = new Position(100, 700); // X=100, Y=700

                // Add the TextFragment to the page's Paragraphs collection
                page.Paragraphs.Add(tf);
            }

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text inserted on all pages. Saved to '{outputPath}'.");
    }
}