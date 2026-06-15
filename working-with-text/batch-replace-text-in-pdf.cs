using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Define old‑new string pairs
        var replacements = new Dictionary<string, string>
        {
            { "OldString1", "NewString1" },
            { "Foo",        "Bar"        },
            // add additional pairs as needed
        };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load, process, and save the PDF
        using (Document doc = new Document(inputPath))
        {
            foreach (var pair in replacements)
            {
                // Find all occurrences of the old text on every page
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(pair.Key);
                // The Document class no longer exposes Accept; use the Pages collection instead
                doc.Pages.Accept(absorber);

                // Replace each found fragment with the new text
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    fragment.Text = pair.Value;
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Batch replacement completed. Output saved to '{outputPath}'.");
    }
}
