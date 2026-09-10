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

        // Ensure the input PDF exists with at least four pages.
        if (!File.Exists(inputPath))
        {
            using (Document placeholder = new Document())
            {
                // Add four blank pages.
                for (int i = 0; i < 4; i++)
                {
                    placeholder.Pages.Add();
                }
                placeholder.Save(inputPath);
            }
        }

        // Load the source PDF.
        using (Document doc = new Document(inputPath))
        {
            // Multiline stamp content – use newline characters for separate lines.
            string multilineText = "First line of stamp\nSecond line of stamp\nThird line of stamp";

            // Create a TextStamp with the multiline text.
            TextStamp stamp = new TextStamp(multilineText)
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                BottomMargin = 10 // points from the bottom edge
            };

            // Add the stamp only to page 4 (1‑based index).
            if (doc.Pages.Count >= 4)
            {
                doc.Pages[4].AddStamp(stamp);
            }
            else
            {
                Console.Error.WriteLine("The document has fewer than 4 pages.");
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp applied to page 4 and saved as '{outputPath}'.");
    }
}
