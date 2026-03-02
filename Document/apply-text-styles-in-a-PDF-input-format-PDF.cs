using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "styled_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Absorb all text fragments from the document
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            doc.Pages.Accept(absorber);

            // Choose a font to apply (e.g., Arial)
            Font font = FontRepository.FindFont("Arial");

            // Apply the chosen font and a new size to every fragment
            absorber.ApplyForAllFragments(font, 14);

            // Apply a new text color to each fragment
            foreach (TextFragment tf in absorber.TextFragments)
            {
                tf.TextState.ForegroundColor = Color.Blue;
            }

            // Save the styled PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Styled PDF saved to '{outputPath}'.");
    }
}