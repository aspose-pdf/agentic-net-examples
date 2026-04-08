using System;
using System.IO;
using System.Text;
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Use TextFragmentAbsorber to capture text fragments with style information
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            doc.Pages.Accept(absorber);

            StringBuilder output = new StringBuilder();

            // Iterate over each fragment and check its TextState for superscript/subscript flags
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                string text = fragment.Text;

                // If the fragment has a TextState, inspect Superscript and Subscript properties
                if (fragment.TextState != null)
                {
                    if (fragment.TextState.Superscript)
                    {
                        // Wrap superscript text with Unicode marker '^' (U+005E)
                        output.Append('⁽'); // Unicode left superscript parenthesis as marker start
                        output.Append(text);
                        output.Append('⁾'); // Unicode right superscript parenthesis as marker end
                    }
                    else if (fragment.TextState.Subscript)
                    {
                        // Wrap subscript text with Unicode marker '_' (U+005F)
                        output.Append('₍'); // Unicode left subscript parenthesis as marker start
                        output.Append(text);
                        output.Append('₎'); // Unicode right subscript parenthesis as marker end
                    }
                    else
                    {
                        output.Append(text);
                    }
                }
                else
                {
                    output.Append(text);
                }
            }

            // Output the processed string
            Console.WriteLine(output.ToString());
        }
    }
}