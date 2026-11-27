using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "output.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdfPath))
        {
            // Use TextFragmentAbsorber to access individual text fragments.
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            // Optional: use pure text extraction mode to get raw fragments.
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

            // Accept the absorber on all pages of the document.
            doc.Pages.Accept(absorber);

            // Reduce the font size of each extracted text fragment.
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Decrease the font size by 1 point, but keep it at least 1.
                fragment.TextState.FontSize = Math.Max(1, fragment.TextState.FontSize - 1);
            }

            // Extract the combined text after font size adjustment.
            string extractedText = absorber.Text;

            // Write the extracted text to the output file.
            File.WriteAllText(outputTxtPath, extractedText);
        }

        Console.WriteLine($"Text extracted and saved to '{outputTxtPath}'.");
    }
}
