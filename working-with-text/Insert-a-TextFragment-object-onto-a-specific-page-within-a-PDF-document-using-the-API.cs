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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Specify the target page (1‑based indexing)
            int targetPageNumber = 1; // change as needed
            Page page = doc.Pages[targetPageNumber];

            // Create a TextFragment with the desired text
            TextFragment textFragment = new TextFragment("Hello Aspose.Pdf!");

            // Set the position where the text will appear on the page
            textFragment.Position = new Position(100, 600);

            // Optional: customize the appearance of the text
            textFragment.TextState.FontSize = 12;
            textFragment.TextState.Font = FontRepository.FindFont("TimesNewRoman");
            textFragment.TextState.ForegroundColor = Color.Blue;

            // Use TextBuilder to append the TextFragment to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(textFragment);

            // Save the modified PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text fragment inserted and saved to '{outputPath}'.");
    }
}