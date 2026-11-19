using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";
        const string searchText = "Hello"; // text to locate and rotate

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Find the first occurrence of the specified text
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);
            doc.Pages.Accept(absorber);

            // Ensure at least one fragment was found
            if (absorber.TextFragments.Count > 0)
            {
                // TextFragment collections are 1‑based
                TextFragment fragment = absorber.TextFragments[1];

                // Rotate the fragment by 45 degrees
                fragment.TextState.Rotation = 45;
            }
            else
            {
                Console.WriteLine($"Text \"{searchText}\" not found in the document.");
            }

            // Save the modified PDF (PDF format is default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}