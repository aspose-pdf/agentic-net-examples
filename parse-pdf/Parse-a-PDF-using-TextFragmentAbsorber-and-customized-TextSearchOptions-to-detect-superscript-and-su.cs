using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Configure text search options (regular‑expression mode optional)
            Aspose.Pdf.Text.TextSearchOptions searchOptions = new Aspose.Pdf.Text.TextSearchOptions(true);

            // Create a TextFragmentAbsorber that will collect all text fragments
            Aspose.Pdf.Text.TextFragmentAbsorber absorber = new Aspose.Pdf.Text.TextFragmentAbsorber();
            absorber.TextSearchOptions = searchOptions;

            // Apply the absorber to all pages of the document
            doc.Pages.Accept(absorber);

            // Iterate over the found fragments and report superscript / subscript usage
            int fragmentIndex = 1;
            foreach (Aspose.Pdf.Text.TextFragment fragment in absorber.TextFragments)
            {
                bool isSuperscript = fragment.TextState.Superscript;
                bool isSubscript   = fragment.TextState.Subscript;

                if (isSuperscript || isSubscript)
                {
                    Console.WriteLine(
                        $"Fragment {fragmentIndex}: \"{fragment.Text}\" " +
                        $"Superscript={isSuperscript}, Subscript={isSubscript}");
                }

                fragmentIndex++;
            }

            // Save the (unchanged) document to demonstrate proper save usage
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}