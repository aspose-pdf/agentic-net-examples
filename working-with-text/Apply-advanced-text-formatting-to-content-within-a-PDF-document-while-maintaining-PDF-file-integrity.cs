using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "formatted_output.pdf";

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

            // Use Pure formatting mode to retain basic formatting while extracting
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

            // Apply the absorber to all pages
            doc.Pages.Accept(absorber);

            // Iterate over each captured text fragment and apply advanced formatting
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Set a new font (e.g., Arial)
                fragment.TextState.Font = FontRepository.FindFont("Arial");

                // Increase font size
                fragment.TextState.FontSize = 14;

                // Change text color to blue
                fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

                // Example of additional styling: underline the text
                fragment.TextState.Underline = true;
            }

            // Save the modified PDF (saving as PDF does not require explicit SaveOptions)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Formatted PDF saved to '{outputPath}'.");
    }
}