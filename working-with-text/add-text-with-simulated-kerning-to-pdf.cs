using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_kerning.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the text will be added (first page in this example)
            Page page = doc.Pages[1];

            // Create a new TextFragment with the desired content
            TextFragment fragment = new TextFragment("Kerning enabled text");

            // Set the position of the fragment on the page
            fragment.Position = new Position(100, 700);

            // Kerning is not directly exposed via TextFragmentState. Use CharacterSpacing to simulate
            fragment.TextState.CharacterSpacing = 0.5f; // adjust as needed for visual effect

            // Optional: set font and size for better visibility
            fragment.TextState.Font = FontRepository.FindFont("Arial");
            fragment.TextState.FontSize = 12;

            // Append the fragment to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(fragment);

            // Save the modified PDF (PDF format, so no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with kerning simulated: {outputPath}");
    }
}
