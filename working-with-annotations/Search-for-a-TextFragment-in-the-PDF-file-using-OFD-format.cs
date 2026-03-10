using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.ofd";          // OFD source file
        const string outputPath = "output.pdf";        // Resulting PDF file
        const string searchPhrase = "sample";          // Text to search for

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the OFD file as a PDF document using the appropriate load options
        using (Document doc = new Document(inputPath, new OfdLoadOptions()))
        {
            // Create a TextFragmentAbsorber for the desired phrase
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchPhrase);

            // Perform the search on all pages
            doc.Pages.Accept(absorber);

            // Output each found fragment with its page number and position
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                Console.WriteLine(
                    $"Found \"{fragment.Text}\" on page {fragment.Page.Number} " +
                    $"at X={fragment.Position.XIndent}, Y={fragment.Position.YIndent}");
            }

            // Example modification: replace the text of the first occurrence
            if (absorber.TextFragments.Count > 0)
            {
                absorber.TextFragments[1].Text = "replacement";
            }

            // Save the (potentially modified) document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Search completed. Output saved to '{outputPath}'.");
    }
}