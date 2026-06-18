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
        using (Document doc = new Document(inputPath))
        {
            // Replace each target phrase while preserving formatting
            ReplacePhrase(doc, "hello", "Hi");
            ReplacePhrase(doc, "world", "John");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Replacements saved to '{outputPath}'.");
    }

    /// <summary>
    /// Finds all occurrences of <paramref name="searchPhrase"/> in the document,
    /// replaces them with <paramref name="replacement"/>, and copies the original
    /// TextState to keep font, size, color, etc.
    /// </summary>
    static void ReplacePhrase(Document doc, string searchPhrase, string replacement)
    {
        // Create a TextFragmentAbsorber that searches for the specified phrase
        TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchPhrase);

        // Perform the search on all pages
        doc.Pages.Accept(absorber);

        // Iterate through each found text fragment
        foreach (TextFragment fragment in absorber.TextFragments)
        {
            // Clone the current formatting (TextState)
            TextState originalState = new TextState();
            originalState.ApplyChangesFrom(fragment.TextState);

            // Replace the text content
            fragment.Text = replacement;

            // Reapply the original formatting to the modified fragment
            fragment.TextState.ApplyChangesFrom(originalState);
        }
    }
}