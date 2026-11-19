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
            // Load the PDF document inside a using block for deterministic disposal
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
            {
                // -------------------------------------------------
                // Extract all text from the document using TextAbsorber
                // -------------------------------------------------
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages.Accept(absorber);
                string extractedText = absorber.Text;
                Console.WriteLine($"Extracted text length: {extractedText.Length}");

                // -------------------------------------------------
                // Add a new TextFragment with hyphenation enabled (via reflection for compatibility)
                // -------------------------------------------------
                // Get the first page (pages are 1‑based)
                Page page = doc.Pages[1];

                // Create the text fragment
                TextFragment fragment = new TextFragment(
                    "This is a sample long text that will demonstrate hyphenation when the line wraps across multiple lines in the PDF document."
                );

                // Set font and size
                fragment.TextState.Font = FontRepository.FindFont("Arial");
                fragment.TextState.FontSize = 12;

                // Enable hyphenation – the Hyphenation property may not exist in older versions,
                // so we set it via reflection when it is available.
                var hyphenProp = fragment.TextState.GetType().GetProperty("Hyphenation");
                if (hyphenProp != null && hyphenProp.CanWrite)
                {
                    hyphenProp.SetValue(fragment.TextState, true);
                }

                // Position the fragment on the page
                fragment.Position = new Position(50, 700);

                // Add the fragment to the page's paragraph collection
                page.Paragraphs.Add(fragment);

                // -------------------------------------------------
                // Save the modified document
                // -------------------------------------------------
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
