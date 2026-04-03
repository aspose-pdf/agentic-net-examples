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
            // Choose the page where the label should appear (e.g., first page)
            Page page = doc.Pages[1];

            // Create a TextFragment that will act as the form field label
            TextFragment label = new TextFragment("Field Label");

            // Set the position of the label on the page
            label.Position = new Position(100, 700); // adjust coordinates as needed

            // Rotate the label by 45 degrees (arbitrary angle)
            label.TextState.Rotation = 45; // degrees

            // Optional styling for better visibility
            label.TextState.Font = FontRepository.FindFont("Helvetica");
            label.TextState.FontSize = 12;
            label.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Add the rotated label to the page content
            page.Paragraphs.Add(label);

            // Save the modified PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated label PDF saved to '{outputPath}'.");
    }
}