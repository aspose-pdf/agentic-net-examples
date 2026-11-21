using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define the phrases to replace and their new texts.
        var replacements = new Dictionary<string, string>(StringComparer.Ordinal)
        {
            { "Old Phrase One", "New Phrase One" },
            { "Sample Text",     "Updated Text" },
            { "Placeholder",     "Real Content" }
        };

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Iterate over each phrase that needs to be replaced.
                foreach (var kvp in replacements)
                {
                    string oldPhrase = kvp.Key;
                    string newPhrase = kvp.Value;

                    // TextFragmentAbsorber finds exact text fragments.
                    TextFragmentAbsorber absorber = new TextFragmentAbsorber(oldPhrase);
                    // Accept the absorber on all pages.
                    doc.Pages.Accept(absorber);

                    // Replace each found fragment while keeping its original TextState.
                    foreach (TextFragment fragment in absorber.TextFragments)
                    {
                        // Preserve formatting by leaving the existing TextState untouched.
                        fragment.Text = newPhrase;
                    }
                }

                // Save the modified PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Replacements completed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}