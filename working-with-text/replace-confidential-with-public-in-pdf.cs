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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create an absorber that finds all occurrences of "Confidential"
            TextFragmentAbsorber absorber = new TextFragmentAbsorber("Confidential");
            // Optional: configure search options (case‑sensitive = false)
            absorber.TextSearchOptions = new TextSearchOptions(false);

            // Apply the absorber to all pages
            doc.Pages.Accept(absorber);

            // Replace each found fragment with "Public"
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.Text = "Public";
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Replacement completed. Saved to '{outputPath}'.");
    }
}
