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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a text fragment with the desired content
            TextFragment fragment = new TextFragment("Highlighted text");

            // Set the background color of the text via TextState
            fragment.TextState.BackgroundColor = Aspose.Pdf.Color.Yellow;

            // Optional: set additional text properties
            fragment.TextState.FontSize = 14;
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");

            // Position the text on the page (coordinates are in points)
            fragment.Position = new Position(100, 700);

            // Add the fragment to the first page of the document
            doc.Pages[1].Paragraphs.Add(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with background-colored text to '{outputPath}'.");
    }
}