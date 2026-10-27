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

        try
        {
            // Load the existing PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Verify that a second page exists (Aspose.Pdf uses 1‑based indexing)
                if (doc.Pages.Count < 2)
                {
                    Console.Error.WriteLine("The document does not contain a second page.");
                    return;
                }

                // Retrieve the second page
                Page page = doc.Pages[2];

                // Create a TextFragment with the desired text
                TextFragment fragment = new TextFragment("Rotated Text");
                // Set the position where the text will appear
                fragment.Position = new Position(100, 500);
                // Rotate the text by 45 degrees
                fragment.TextState.Rotation = 45;
                // Optional styling
                fragment.TextState.FontSize = 24;
                fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

                // Use TextBuilder to insert the fragment onto the page
                TextBuilder builder = new TextBuilder(page);
                builder.AppendText(fragment);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Rotated text added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}