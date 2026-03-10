using System;
using System.Collections.Generic;
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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Prepare a TextFragmentAbsorber to capture all text fragments
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();

            // Use Pure formatting mode to retain formatting flags (e.g., superscript/subscript)
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

            // Extract text from all pages
            doc.Pages.Accept(absorber);

            // Collections to hold extracted superscript and subscript characters
            List<string> superscripts = new List<string>();
            List<string> subscripts = new List<string>();

            // Iterate over each captured text fragment
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // The TextState of a fragment contains formatting flags
                TextFragmentState state = fragment.TextState;

                // Superscript detection (property exists on TextFragmentState)
                if (state.Superscript)
                {
                    superscripts.Add(fragment.Text);
                }

                // Subscript detection – TextFragmentState also exposes Subscript (nullable bool)
                // Use true check after null‑coalescing to avoid NullReferenceException
                if (state.Subscript == true)
                {
                    subscripts.Add(fragment.Text);
                }
            }

            // Output results
            Console.WriteLine("Superscript characters found:");
            foreach (string txt in superscripts)
                Console.WriteLine(txt);

            Console.WriteLine("\nSubscript characters found:");
            foreach (string txt in subscripts)
                Console.WriteLine(txt);
        }
    }
}