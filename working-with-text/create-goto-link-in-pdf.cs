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
        const int   targetPage = 2; // page to navigate to (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Validate target page number
            if (targetPage < 1 || targetPage > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Target page {targetPage} is out of range.");
                return;
            }

            // Create a text fragment that will act as a hyperlink
            TextFragment linkFragment = new TextFragment($"Go to page {targetPage}");

            // Assign a LocalHyperlink which internally uses a GoToAction
            linkFragment.Hyperlink = new LocalHyperlink { TargetPageNumber = targetPage };

            // Optional styling to make it look like a link
            linkFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            linkFragment.TextState.Underline = true;

            // Add the fragment to the first page (or any page you prefer)
            doc.Pages[1].Paragraphs.Add(linkFragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with navigation link saved to '{outputPath}'.");
    }
}