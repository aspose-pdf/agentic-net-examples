using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_text.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a text fragment with the desired content
            TextFragment textFragment = new TextFragment("Rotated Text");

            // Set the position where the text will be placed (X=100, Y=500)
            textFragment.Position = new Position(100, 500);

            // Rotate the text by 45 degrees using the TextState's Rotation property
            textFragment.TextState.Rotation = 45; // angle in degrees

            // Add the text fragment to the first page of the document
            Page firstPage = doc.Pages[1];
            firstPage.Paragraphs.Add(textFragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated text PDF saved to '{outputPath}'.");
    }
}