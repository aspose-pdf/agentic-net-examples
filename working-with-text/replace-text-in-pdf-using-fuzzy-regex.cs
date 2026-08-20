using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string replacement = "hi universe";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Build a fuzzy regex pattern: allow any non‑word characters between "hello" and "world", case‑insensitive
        string pattern = @"hello\W*world";
        Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragmentAbsorber that uses the regex and enables regular‑expression search
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(regex, new TextSearchOptions(true));

            // Search all pages
            doc.Pages.Accept(absorber);

            // Replace each matched fragment with the desired text
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.Text = replacement;
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}