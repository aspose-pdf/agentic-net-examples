using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class SuperscriptSubscriptExtractor
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";

        // Verify the file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // TextFragmentAbsorber extracts text fragments together with their formatting
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();

            // Apply the absorber to all pages
            doc.Pages.Accept(absorber);

            // StringBuilder to collect the output with Unicode markers
            StringBuilder result = new StringBuilder();

            // Iterate over each extracted fragment
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Determine the formatting flags
                bool isSuperscript = fragment.TextState.Superscript;
                bool isSubscript   = fragment.TextState.Subscript;

                // Choose markers:
                //   ^  for superscript
                //   _  for subscript
                // If both are false, output the text as‑is.
                // If a fragment contains multiple characters, apply the same marker to the whole fragment.
                if (isSuperscript)
                {
                    result.Append('^');
                    result.Append(fragment.Text);
                }
                else if (isSubscript)
                {
                    result.Append('_');
                    result.Append(fragment.Text);
                }
                else
                {
                    result.Append(fragment.Text);
                }
            }

            // Output the collected string to console
            Console.WriteLine("Extracted text with markers:");
            Console.WriteLine(result.ToString());

            // Optionally, write the result to a text file
            const string outputPath = "extracted_with_markers.txt";
            File.WriteAllText(outputPath, result.ToString(), Encoding.UTF8);
            Console.WriteLine($"Result written to '{outputPath}'.");
        }
    }
}