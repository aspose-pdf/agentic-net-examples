using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            int totalPages = doc.Pages.Count; // 1‑based page count

            // Iterate using 1‑based indexing as required by Aspose.Pdf
            for (int i = 1; i <= totalPages; i++)
            {
                Page page = doc.Pages[i];

                // Create the page number text in the format "current/total"
                string pageNumberText = $"{i}/{totalPages}";

                TextFragment tf = new TextFragment(pageNumberText);
                tf.TextState.FontSize = 12;
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                tf.Position = new Position(50, 20); // Adjust X/Y as needed (bottom‑left corner)

                // Add the text fragment to the page's paragraphs collection
                page.Paragraphs.Add(tf);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers inserted and saved to '{outputPath}'.");
    }
}