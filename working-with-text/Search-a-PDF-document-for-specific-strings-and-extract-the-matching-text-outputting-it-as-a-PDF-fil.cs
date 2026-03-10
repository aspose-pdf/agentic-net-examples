using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF path, output PDF path and the phrase to search for
        const string inputPath  = "input.pdf";
        const string outputPath = "extracted_text.pdf";
        const string searchPhrase = "Your search string";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source document and search for the phrase
        using (Document srcDoc = new Document(inputPath))
        {
            // Create a TextFragmentAbsorber that looks for the specified phrase
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchPhrase);

            // Perform the search on the whole document
            absorber.Visit(srcDoc);

            // If nothing was found, exit early
            if (absorber.TextFragments.Count == 0)
            {
                Console.WriteLine("No matching text found.");
                return;
            }

            // Concatenate all found fragments into a single string
            string extractedText = string.Empty;
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                extractedText += fragment.Text + Environment.NewLine;
            }

            // Create a new PDF document to hold the extracted text
            using (Document outDoc = new Document())
            {
                // Add a blank page
                Page page = outDoc.Pages.Add();

                // Create a TextFragment with the extracted content
                TextFragment outFragment = new TextFragment(extractedText)
                {
                    // Optional formatting
                    TextState =
                    {
                        Font = FontRepository.FindFont("Arial"),
                        FontSize = 12,
                        ForegroundColor = Aspose.Pdf.Color.Black
                    },
                    // Position the text near the top of the page
                    Position = new Position(0, page.PageInfo.Height - 20)
                };

                // Add the fragment to the page
                page.Paragraphs.Add(outFragment);

                // Save the result PDF
                outDoc.Save(outputPath);
            }

            Console.WriteLine($"Extracted text saved to '{outputPath}'.");
        }
    }
}