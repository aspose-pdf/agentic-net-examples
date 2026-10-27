using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input parameters
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string oldWord    = "Aspose";
        const string newWord    = "Aspose.Pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragmentAbsorber to locate all occurrences of the target word.
            // The search is case‑sensitive by default; set appropriate options if needed.
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(oldWord);

            // TextSearchOptions requires a constructor argument (searchInAnnotations).
            // Here we set it to false because we only need to search the page content.
            TextSearchOptions searchOptions = new TextSearchOptions(false);
            absorber.TextSearchOptions = searchOptions;

            // Apply the absorber to all pages (1‑based indexing is handled internally).
            doc.Pages.Accept(absorber);

            // Replace each found fragment's text while keeping its formatting.
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.Text = newWord; // Formatting (font, size, color) remains unchanged.
            }

            // Save the modified PDF. The Document.Save(string) overload writes PDF regardless of extension.
            doc.Save(outputPath);
        }

        Console.WriteLine($"All occurrences of \"{oldWord}\" have been replaced with \"{newWord}\" and saved to \"{outputPath}\".");
    }
}
