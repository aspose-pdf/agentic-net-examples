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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragmentAbsorber to capture all text fragments on all pages
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();

            // Optional: set extraction options (Pure mode preserves basic formatting)
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

            // Apply the absorber to all pages
            doc.Pages.Accept(absorber);

            // Define the desired style
            Font newFont = FontRepository.FindFont("Arial");
            float newFontSize = 12f; // FontSize expects a float, not double
            Aspose.Pdf.Color newColor = Aspose.Pdf.Color.Blue;

            // Apply the style to each captured text fragment
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.TextState.Font = newFont;
                fragment.TextState.FontSize = newFontSize;
                fragment.TextState.ForegroundColor = newColor;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Styled PDF saved to '{outputPath}'.");
    }
}
