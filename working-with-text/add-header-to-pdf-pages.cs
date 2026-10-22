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
        const string headerText = "Confidential Document";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based; iterate through each page
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a text fragment for the header
                TextFragment header = new TextFragment(headerText)
                {
                    Position = new Position(50, 800), // adjust X/Y as needed
                    TextState =
                    {
                        FontSize = 12,
                        FontStyle = FontStyles.Bold,
                        ForegroundColor = Aspose.Pdf.Color.DarkGray
                    }
                };

                // Insert the header at the beginning of the page's paragraph collection
                page.Paragraphs.Insert(0, header);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Header added to all pages. Saved as '{outputPath}'.");
    }
}
