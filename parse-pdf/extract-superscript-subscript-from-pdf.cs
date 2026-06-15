using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class SuperscriptSubscriptExtractor
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTextPath = "extracted.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal.
        using (Document doc = new Document(inputPdfPath))
        {
            // Prepare an absorber that extracts text fragments with formatting information.
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

            // Apply the absorber to all pages.
            doc.Pages.Accept(absorber);

            // Open the output file for writing.
            using (StreamWriter writer = new StreamWriter(outputTextPath, false))
            {
                // Iterate over each extracted text fragment.
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    string text = fragment.Text;

                    // Determine if the fragment is marked as superscript or subscript.
                    bool isSuperscript = fragment.TextState.Superscript;
                    bool isSubscript = fragment.TextState.Subscript;

                    // Wrap the text with Unicode markers.
                    if (isSuperscript && isSubscript)
                    {
                        // Rare case where both flags are true – treat as superscript.
                        writer.WriteLine("[SUP]" + text + "[/SUP]");
                    }
                    else if (isSuperscript)
                    {
                        writer.WriteLine("[SUP]" + text + "[/SUP]");
                    }
                    else if (isSubscript)
                    {
                        writer.WriteLine("[SUB]" + text + "[/SUB]");
                    }
                    else
                    {
                        writer.WriteLine(text);
                    }
                }
            }

            Console.WriteLine($"Extraction completed. Results saved to '{outputTextPath}'.");
        }
    }
}