using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_numbered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate using 1‑based page indexing
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Process only odd pages
                if (i % 2 == 1)
                {
                    Page page = doc.Pages[i];

                    // Create a text fragment with the page number
                    TextFragment tf = new TextFragment(i.ToString())
                    {
                        // Position near the bottom center of the page
                        // X = half of page width, Y = 20 points from bottom
                        Position = new Position(page.PageInfo.Width / 2, 20),
                        // Center align the text
                        HorizontalAlignment = HorizontalAlignment.Center,
                        // Use a cross‑platform color
                        TextState = { ForegroundColor = Aspose.Pdf.Color.Black }
                    };

                    // Add the fragment to the page's paragraphs collection
                    page.Paragraphs.Add(tf);
                }
            }

            // Save the modified PDF (save inside the using block as per lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added to odd pages. Output saved to '{outputPath}'.");
    }
}