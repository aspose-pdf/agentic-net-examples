using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "aligned_page2.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least two pages
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document does not contain a second page.");
                return;
            }

            // Create a text fragment that will be placed on page 2
            TextFragment fragment = new TextFragment("Left‑justified text on page 2");
            // Set left alignment explicitly (default is left, but we set it for clarity)
            fragment.TextState.HorizontalAlignment = HorizontalAlignment.Left;
            // Position the text near the top‑left corner of the page
            fragment.Position = new Position(50, 750); // X = 50 points, Y = 750 points

            // Add the fragment to page 2
            Page pageTwo = doc.Pages[2];
            pageTwo.Paragraphs.Add(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with left‑justified content on page 2: {outputPath}");
    }
}