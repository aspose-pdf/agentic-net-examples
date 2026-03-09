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
        const string searchPhrase = "hello world";
        const string replacement = "hi world";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use Document constructor)
            using (Document doc = new Document(inputPath))
            {
                // Create a TextFragmentAbsorber to find all occurrences of the search phrase
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchPhrase);

                // Search the entire document (page-indexing is handled internally)
                doc.Pages.Accept(absorber);

                // Replace each found text fragment while preserving its formatting
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    fragment.Text = replacement;
                }

                // Save the modified PDF (lifecycle rule: use Document.Save)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Text replacement completed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}