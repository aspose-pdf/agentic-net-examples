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

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Text to replace
        const string oldText = "Confidential";
        const string newText = "Public";

        // Use TextFragmentAbsorber to find all occurrences of the old text
        TextFragmentAbsorber absorber = new TextFragmentAbsorber(oldText);
        // Configure search options (case‑sensitive in this example; adjust as needed)
        // TextSearchOptions does not have a parameterless constructor; use the constructor that accepts the case‑sensitive flag.
        absorber.TextSearchOptions = new TextSearchOptions(true);

        // Search the whole document
        doc.Pages.Accept(absorber);

        // Replace each found fragment with the new text
        foreach (TextFragment fragment in absorber.TextFragments)
        {
            fragment.Text = newText;
        }

        // Save the modified document
        doc.Save(outputPath);

        Console.WriteLine($"All occurrences replaced. Saved to '{outputPath}'.");
    }
}
