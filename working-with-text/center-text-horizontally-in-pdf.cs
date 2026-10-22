using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "centered_output.pdf";
        const string text = "Centered Text Example";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Ensure there is at least one page
                Page page = doc.Pages[1];

                // Create a text fragment with the desired content
                TextFragment fragment = new TextFragment(text);

                // Center the text horizontally on the page
                fragment.TextState.HorizontalAlignment = HorizontalAlignment.Center;

                // Optional styling
                fragment.TextState.FontSize = 14;
                fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Add the fragment to the page
                page.Paragraphs.Add(fragment);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Centered text saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}