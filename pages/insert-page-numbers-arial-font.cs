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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Obtain the Arial font from the system font repository
            Font arial = FontRepository.FindFont("Arial");

            // Iterate over pages using 1‑based indexing
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Prepare the page number text
                string pageNumber = i.ToString();

                // Position the number near the bottom‑right corner (adjust margins as needed)
                double x = page.PageInfo.Width - 50; // horizontal offset from right edge
                double y = 20;                       // vertical offset from bottom edge

                TextFragment tf = new TextFragment(pageNumber)
                {
                    Position = new Position(x, y)
                };
                tf.TextState.Font = arial;
                tf.TextState.FontSize = 14;
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Add the text fragment to the page
                page.Paragraphs.Add(tf);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}