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
        const string textToInsert = "Identical plain text";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over each page in the document
            foreach (Page page in doc.Pages)
            {
                // Create a TextFragment with the desired text
                TextFragment fragment = new TextFragment(textToInsert);
                // Set position on the page (example coordinates)
                fragment.Position = new Position(100, 700);
                // Set basic text styling
                fragment.TextState.Font = FontRepository.FindFont("Helvetica");
                fragment.TextState.FontSize = 12;
                fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Use TextBuilder to append the fragment to the current page
                TextBuilder builder = new TextBuilder(page);
                builder.AppendText(fragment);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text inserted on all pages. Saved to '{outputPath}'.");
    }
}