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

        // Define old‑new string pairs
        var replacements = new Dictionary<string, string>(StringComparer.Ordinal)
        {
            { "OldString1", "NewString1" },
            { "OldString2", "NewString2" },
            { "Sample Text", "Replaced Text" }
            // add more pairs as needed
        };

        // Load the PDF, process replacements, and save
        using (Document doc = new Document(inputPath))
        {
            foreach (var kvp in replacements)
            {
                // Search for the old string across all pages
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(kvp.Key)
                {
                    // Use the constructor that accepts the Scope enum to replace all occurrences
                    TextReplaceOptions = new TextReplaceOptions(TextReplaceOptions.Scope.REPLACE_ALL)
                };

                // Apply absorber to the whole document
                doc.Pages.Accept(absorber);

                // Replace each found fragment with the new string
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    fragment.Text = kvp.Value;
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Replacements completed. Output saved to '{outputPath}'.");
    }
}
