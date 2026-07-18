using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string phrase = "your specific phrase";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragmentAbsorber that searches for the given phrase
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(phrase);

            // Perform the search on the entire document
            absorber.Visit(doc);

            // Report the number of occurrences found
            Console.WriteLine($"Found {absorber.TextFragments.Count} occurrence(s) of \"{phrase}\":");

            // Iterate through each found fragment and display its text
            int idx = 1;
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                Console.WriteLine($"{idx}: \"{fragment.Text}\"");
                idx++;
            }
        }
    }
}