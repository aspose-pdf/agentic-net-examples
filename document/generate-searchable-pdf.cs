using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "scanned.pdf";
        const string outputPath = "searchable.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Example hidden text to overlay on each page (replace with OCR result as needed)
            string hiddenText = "Sample hidden searchable text";

            foreach (Page page in doc.Pages)
            {
                // Create a TextFragment with invisible text
                TextFragment fragment = new TextFragment(hiddenText);
                fragment.TextState.Invisible = true;
                // Position the text (optional – here at the lower‑left corner)
                fragment.Position = new Position(0, 0);
                // Add the fragment to the page
                page.Paragraphs.Add(fragment);
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Searchable PDF saved to '{outputPath}'.");
    }
}