using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Use TextFragmentAbsorber (not TextAbsorber) to get access to TextFragments collection
            var absorber = new TextFragmentAbsorber();
            // Enable pure formatting extraction so Superscript/Subscript flags are populated
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

            // Iterate over each page and extract fragments
            foreach (Page page in doc.Pages)
            {
                page.Accept(absorber);

                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    // Guard against null TextState (should not happen with Pure mode, but safe)
                    if (fragment.TextState != null)
                    {
                        if (fragment.TextState.Superscript)
                        {
                            // Output superscript characters with a Unicode marker
                            Console.WriteLine($"[SUPERSCRIPT] {fragment.Text}");
                        }
                        else if (fragment.TextState.Subscript)
                        {
                            // Output subscript characters with a Unicode marker
                            Console.WriteLine($"[SUBSCRIPT] {fragment.Text}");
                        }
                    }
                }
                // Clear the collection before processing the next page
                absorber.TextFragments.Clear();
            }
        }
    }
}
