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
        const string searchPhrase = "old phrase";
        const string replacePhrase = "new phrase";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragmentAbsorber that searches for the target phrase
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchPhrase);

            // Run the absorber on every page of the document
            foreach (Page page in doc.Pages)
            {
                page.Accept(absorber);
            }

            // Iterate over all found text fragments
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Preserve the current formatting (font, size, color, etc.)
                TextState originalState = fragment.TextState;

                // Replace the text with the new phrase
                fragment.Text = replacePhrase;

                // Reapply the original formatting to the modified fragment
                fragment.TextState.ApplyChangesFrom(originalState);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Replaced \"{searchPhrase}\" with \"{replacePhrase}\" and saved to {outputPath}");
    }
}