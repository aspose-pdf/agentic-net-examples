using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "rotated_paragraph.pdf";

        // Ensure the Document is disposed properly
        using (Document doc = new Document())
        {
            // Add a new page (first page)
            Page page = doc.Pages.Add();

            // Create individual TextFragments – each fragment can be rotated.
            // Adding them sequentially to the page results in a single visual paragraph.
            TextFragment fragment1 = new TextFragment("Hello, ");
            TextFragment fragment2 = new TextFragment("world! ");
            TextFragment fragment3 = new TextFragment("This paragraph is rotated.");

            // Apply a 30‑degree rotation to each fragment (the whole visual line will appear rotated)
            fragment1.TextState.Rotation = 30;
            fragment2.TextState.Rotation = 30;
            fragment3.TextState.Rotation = 30;

            // Add the fragments to the page in order – they will be rendered as one paragraph.
            page.Paragraphs.Add(fragment1);
            page.Paragraphs.Add(fragment2);
            page.Paragraphs.Add(fragment3);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
