using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "underlined.pdf";
        const string phrase = "Specific phrase to underline";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Search for the phrase on page 4 (1‑based indexing)
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(phrase);
            doc.Pages[4].Accept(absorber);

            // If the phrase is found, obtain its bounding rectangle
            if (absorber.TextFragments.Count > 0)
            {
                // Take the first occurrence; adjust as needed for multiple matches
                TextFragment fragment = absorber.TextFragments[1]; // collection is 1‑based
                Aspose.Pdf.Rectangle rect = fragment.Rectangle;

                // Create an underline annotation and add it to page 4
                Page page = doc.Pages[4];
                var underline = new UnderlineAnnotation(page, rect)
                {
                    // Optional: set the annotation's appearance or contents
                    Contents = "Underline",
                    Color = Color.Blue // requires Aspose.Pdf namespace
                };
                page.Annotations.Add(underline);

                // Save the modified document
                doc.Save(outputPath);
                Console.WriteLine($"Underline annotation added and saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine($"Phrase \"{phrase}\" not found on page 4.");
            }
        }
    }
}
