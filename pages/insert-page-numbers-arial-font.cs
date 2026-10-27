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
            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a text fragment containing the page number
                TextFragment tf = new TextFragment(i.ToString());

                // Set custom font Arial, size 14 points
                tf.TextState.Font = FontRepository.FindFont("Arial");
                tf.TextState.FontSize = 14;

                // Position the page number (example: 20 points from bottom, 50 points from right)
                tf.Position = new Position(page.PageInfo.Width - 50, 20);

                // Add the text fragment to the page
                page.Paragraphs.Add(tf);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}