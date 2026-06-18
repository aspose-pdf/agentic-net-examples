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
        const string plainText  = "Sample plain text";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a TextFragment with the desired text
                TextFragment fragment = new TextFragment(plainText);

                // Optional: set the position of the text on the page
                fragment.Position = new Position(100, 700); // X=100, Y=700

                // Append the text fragment to the page using TextBuilder
                TextBuilder builder = new TextBuilder(page);
                builder.AppendText(fragment);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Plain text added to all pages. Saved as '{outputPath}'.");
    }
}