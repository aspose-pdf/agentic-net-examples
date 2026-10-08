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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Format page number with leading zero for single‑digit pages (e.g., 01, 02)
                string pageNumber = i.ToString("D2");

                // Create a text fragment containing the page number
                TextFragment tf = new TextFragment(pageNumber);
                tf.TextState.FontSize = 12;
                tf.TextState.Font = FontRepository.FindFont("Arial");

                // Position the number near the bottom‑center of the page
                // Adjust X coordinate to roughly center the text
                double x = (page.PageInfo.Width / 2) - 10; // 10 points offset for centering approximation
                double y = 20; // 20 points from the bottom edge
                tf.Position = new Position(x, y);

                // Add the fragment to the page's paragraph collection
                page.Paragraphs.Add(tf);
            }

            // Save the modified PDF (PDF format by default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}