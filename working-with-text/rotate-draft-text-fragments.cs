using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_draft.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create an absorber that searches for the word "Draft"
            TextFragmentAbsorber absorber = new TextFragmentAbsorber("Draft");

            // Perform the search on the entire document
            absorber.Visit(doc);

            // Rotate each found text fragment by 15 degrees
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.TextState.Rotation = 15;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}