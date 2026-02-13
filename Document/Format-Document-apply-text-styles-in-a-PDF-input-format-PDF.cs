using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF paths (adjust as needed)
        const string inputPath = "input.pdf";
        const string outputPath = "output_styled.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Create a TextFragmentAbsorber to capture all text fragments
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();

            // Accept the absorber on all pages
            pdfDocument.Pages.Accept(absorber);

            // Iterate over each found text fragment and apply styling
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Example style changes:
                // - Set font to Arial (fallback to default if not found)
                // - Set font size to 14 points
                // - Apply bold and italic styles
                // - Change text color to dark blue
                fragment.TextState.Font = FontRepository.FindFont("Arial");
                fragment.TextState.FontSize = 14;
                fragment.TextState.FontStyle = FontStyles.Bold | FontStyles.Italic;
                fragment.TextState.ForegroundColor = Color.FromRgb(0, 0, 0.5); // dark blue (values 0..1)
            }

            // Save the modified PDF using the standard save rule
            pdfDocument.Save(outputPath);

            Console.WriteLine($"PDF successfully styled and saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}