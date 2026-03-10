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
        const string searchText = "Important";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Find all occurrences of the target text
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);
            // Use Pure extraction mode to keep layout information
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
            doc.Pages.Accept(absorber);

            // Apply visual effects to each found fragment
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Set text color to red
                fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Red;
                // Highlight background with yellow
                fragment.TextState.BackgroundColor = Aspose.Pdf.Color.Yellow;
                // Make text bold
                fragment.TextState.FontStyle = FontStyles.Bold;
                // Underline the text (Underline is a separate property, not part of FontStyles)
                fragment.TextState.Underline = true;
                // Slightly increase font size to preserve readability
                fragment.TextState.FontSize = fragment.TextState.FontSize + 1;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Visual text effects applied and saved to '{outputPath}'.");
    }
}