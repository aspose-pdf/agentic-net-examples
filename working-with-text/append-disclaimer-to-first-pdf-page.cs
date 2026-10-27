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
        const string disclaimer = "Disclaimer: This document is for informational purposes only.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 1)
            {
                Console.Error.WriteLine("The PDF contains no pages.");
                return;
            }

            // Get the first page
            Page firstPage = doc.Pages[1];

            // Create a TextFragment with the disclaimer text
            TextFragment disclaimerFragment = new TextFragment(disclaimer);

            // Append the TextFragment to the end of the page's paragraph collection
            firstPage.Paragraphs.Add(disclaimerFragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Disclaimer appended and saved to '{outputPath}'.");
    }
}
