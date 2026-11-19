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

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create an absorber for the text to be replaced
            var absorber = new TextFragmentAbsorber("Confidential");
            // Configure case‑insensitive search (false => case‑insensitive)
            absorber.TextSearchOptions = new TextSearchOptions(false);

            // Search all pages
            doc.Pages.Accept(absorber);

            // Replace each found fragment with the new text
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.Text = "Public";
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All occurrences replaced. Output saved to '{outputPath}'.");
    }
}
