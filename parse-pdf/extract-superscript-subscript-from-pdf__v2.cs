using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Use TextFragmentAbsorber to get access to TextFragments collection
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            // Pure extraction keeps formatting information (superscript / subscript)
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
            doc.Pages.Accept(absorber);

            // Build the output string, marking superscript and subscript characters
            string result = string.Empty;

            const string superscriptMarker = "\u02B0"; // ˰
            const string subscriptMarker   = "\u02B9"; // ˹

            foreach (TextFragment fragment in absorber.TextFragments)
            {
                bool isSuperscript = fragment.TextState.Superscript;
                bool isSubscript   = fragment.TextState.Subscript;

                string markedText = fragment.Text;

                if (isSuperscript)
                    markedText = superscriptMarker + markedText;
                else if (isSubscript)
                    markedText = subscriptMarker + markedText;

                result += markedText;
            }

            // Output the processed text
            Console.WriteLine("Extracted text with Unicode markers:");
            Console.WriteLine(result);
        }
    }
}
