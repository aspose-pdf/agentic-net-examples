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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least two pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document does not contain a second page.");
                return;
            }

            // Get the second page
            Page secondPage = doc.Pages[2];

            // Create the text fragment to be inserted
            TextFragment fragment = new TextFragment("Rotated Text");

            // Set the position where the text will start (coordinates are in points)
            fragment.Position = new Position(200, 400);

            // Rotate the text by 45 degrees
            fragment.TextState.Rotation = 45;

            // Use TextBuilder to add the fragment to the page
            TextBuilder builder = new TextBuilder(secondPage);
            builder.AppendText(fragment);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated text added to page 2 and saved as '{outputPath}'.");
    }
}