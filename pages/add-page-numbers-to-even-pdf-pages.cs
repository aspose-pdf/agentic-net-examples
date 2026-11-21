using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "even_pages_numbered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Process only even pages
                if (i % 2 == 0)
                {
                    Page page = doc.Pages[i];

                    // Create a text fragment with the page number
                    TextFragment tf = new TextFragment(i.ToString())
                    {
                        // Position the fragment at the bottom‑right corner
                        // (adjust coordinates as needed for your PDF layout)
                        Position = new Position(page.PageInfo.Width - 50, 20),
                        // Set appearance
                        TextState = { FontSize = 12, ForegroundColor = Aspose.Pdf.Color.Black }
                    };

                    // Add the fragment to the page's paragraph collection
                    page.Paragraphs.Add(tf);
                }
            }

            // Save the modified PDF (output format is PDF by default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Even‑page numbers added. Output saved to '{outputPath}'.");
    }
}