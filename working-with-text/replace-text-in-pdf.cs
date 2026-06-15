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

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Create an absorber to find all occurrences of the old text
                TextFragmentAbsorber absorber = new TextFragmentAbsorber("Confidential");
                // Optional: configure search options (case‑sensitive or not)
                absorber.TextSearchOptions = new TextSearchOptions(false); // false = case‑insensitive

                // Accept the absorber on all pages
                doc.Pages.Accept(absorber);

                // Replace each found fragment with the new text
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    fragment.Text = "Public";
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"All occurrences replaced. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
