using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a text fragment with the desired content
            TextFragment fragment = new TextFragment("This text is underlined.");

            // Set the underline property before inserting the fragment
            fragment.TextState.Underline = true;

            // Optional: set position on the page (coordinates are in points)
            fragment.Position = new Position(100, 700);

            // Append the fragment to the first page
            Page page = doc.Pages[1];
            page.Paragraphs.Add(fragment);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Underlined text added and saved to '{outputPath}'.");
    }
}