using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_bates.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Settings for Bates numbering
            const string prefix = "ABC";   // Prefix for the Bates number
            const int numberOfDigits = 6;   // Six‑digit format
            int currentNumber = 1;          // Starting number

            // Iterate through each page and add a centered text fragment with the formatted number
            foreach (Page page in doc.Pages)
            {
                // Build the Bates number string (e.g., ABC000001)
                string batesNumber = prefix + currentNumber.ToString().PadLeft(numberOfDigits, '0');

                // Create a text fragment for the Bates number
                TextFragment tf = new TextFragment(batesNumber)
                {
                    // Center the text horizontally on the page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    // Position the text near the bottom margin (Y coordinate from bottom)
                    Position = new Position(0, 20) // X = 0 works with Center alignment, Y = 20 points from bottom
                };

                // Optional: set font size and style
                tf.TextState.FontSize = 12;
                tf.TextState.Font = FontRepository.FindFont("Arial");

                // Add the fragment to the page
                page.Paragraphs.Add(tf);

                currentNumber++;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates numbering added and saved to '{outputPath}'.");
    }
}
