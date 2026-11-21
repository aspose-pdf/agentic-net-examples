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

        // Regular expression pattern to find (example: any word starting with "Foo" followed by digits)
        const string pattern = @"\\bFoo\\d+\\b";
        const string replacement = "Bar";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document.
            using (Document pdfDocument = new Document(inputPath))
            {
                // Configure search options to treat the pattern as a regular expression.
                TextSearchOptions searchOptions = new TextSearchOptions(true); // true => RegEx mode

                // Create an absorber that finds all fragments matching the regex.
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(pattern, searchOptions);

                // Apply the absorber to all pages.
                pdfDocument.Pages.Accept(absorber);

                // Replace each found fragment with the replacement text.
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    fragment.Text = replacement;
                }

                // Save the modified PDF. Formatting of the replaced text is preserved.
                pdfDocument.Save(outputPath);
            }

            Console.WriteLine($"Regex replacement completed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
