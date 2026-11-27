using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "output.txt";
        const int targetPageNumber = 13;

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Verify the requested page exists (Aspose.Pdf uses 1‑based indexing)
            if (targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Document does not contain page {targetPageNumber}.");
                return;
            }

            // Extract text fragments from the target page using TextFragmentAbsorber
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
            doc.Pages[targetPageNumber].Accept(absorber);

            // Reduce each fragment's font size by 5%
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                float originalSize = fragment.TextState.FontSize;
                fragment.TextState.FontSize = originalSize * 0.95f;
            }

            // Get the adjusted text
            string adjustedText = absorber.Text;

            // Write the result to a text file
            File.WriteAllText(outputTxt, adjustedText);
        }

        Console.WriteLine($"Adjusted text from page {targetPageNumber} written to '{outputTxt}'.");
    }
}
