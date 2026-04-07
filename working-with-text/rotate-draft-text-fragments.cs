using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Create an absorber to find all occurrences of the word "Draft"
                TextFragmentAbsorber absorber = new TextFragmentAbsorber("Draft");

                // Search the entire document
                doc.Pages.Accept(absorber);

                // Apply a 15‑degree rotation to each found text fragment
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    fragment.TextState.Rotation = 15;
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}