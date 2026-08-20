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

        using (Document doc = new Document(inputPath))
        {
            // Find every occurrence of "Confidential"
            TextFragmentAbsorber absorber = new TextFragmentAbsorber("Confidential");
            // Optional: make the search case‑sensitive (true) or case‑insensitive (false)
            absorber.TextSearchOptions = new TextSearchOptions(true);

            // Apply the absorber to all pages
            doc.Pages.Accept(absorber);

            // Replace the found text with "Public"
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.Text = "Public";
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Replaced text saved to '{outputPath}'.");
    }
}
