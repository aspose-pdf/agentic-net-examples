using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragmentAbsorber to capture all text fragments
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();

            // Use Flatten mode to preserve positioning information (helps retain superscript/subscript flags)
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Flatten);

            // Extract text from all pages
            doc.Pages.Accept(absorber);

            // Containers for extracted superscript and subscript characters
            var superscriptChars = new System.Text.StringBuilder();
            var subscriptChars   = new System.Text.StringBuilder();

            // Iterate over all found text fragments and their segments
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                foreach (TextSegment segment in fragment.Segments)
                {
                    // TextState may contain Superscript and Subscript flags (nullable booleans)
                    // Check for superscript
                    if (segment.TextState.Superscript == true)
                    {
                        superscriptChars.Append(segment.Text);
                    }

                    // Check for subscript
                    if (segment.TextState.Subscript == true)
                    {
                        subscriptChars.Append(segment.Text);
                    }
                }
            }

            // Output the results
            Console.WriteLine("Superscript characters found:");
            Console.WriteLine(superscriptChars.Length > 0 ? superscriptChars.ToString() : "(none)");

            Console.WriteLine("\nSubscript characters found:");
            Console.WriteLine(subscriptChars.Length > 0 ? subscriptChars.ToString() : "(none)");
        }
    }
}