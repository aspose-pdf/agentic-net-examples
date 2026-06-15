using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // required for TextFragment, TextState, Position

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragment with the desired text
            TextFragment fragment = new TextFragment("Background highlighted text");

            // Set the background color via TextState before adding to the page
            fragment.TextState.BackgroundColor = Aspose.Pdf.Color.Yellow;

            // Optionally set the position on the page (coordinates in points)
            fragment.Position = new Position(100, 700); // X=100, Y=700

            // Add the fragment to the first page
            Page page = doc.Pages[1];
            page.Paragraphs.Add(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with background-colored text to '{outputPath}'.");
    }
}