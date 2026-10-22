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
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Create an absorber that searches for the text "Confidential"
                TextFragmentAbsorber absorber = new TextFragmentAbsorber("Confidential");
                // Optional: make the search case‑sensitive (true) or case‑insensitive (false)
                absorber.TextSearchOptions = new TextSearchOptions(true);

                // Apply the absorber to all pages of the document
                doc.Pages.Accept(absorber);

                // Replace each found fragment with the new text "Public"
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    fragment.Text = "Public";
                }

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Replacement completed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
