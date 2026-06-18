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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragment with the desired text
            TextFragment fragment = new TextFragment("Office");

            // Configure the TextState via the read‑only TextState property of the fragment
            fragment.TextState.Font      = FontRepository.FindFont("Times New Roman"); // choose a font that supports ligatures
            fragment.TextState.FontSize  = 12;
            // Note: The current Aspose.Pdf version does not expose a Ligatures property.
            // Ligatures are automatically applied when the selected font supports them.

            // Position the text on the page (coordinates are in points)
            fragment.Position = new Position(100, 700);

            // Add the fragment to the first page
            doc.Pages[1].Paragraphs.Add(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with text added: {outputPath}");
    }
}
