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
            // Ensure there is at least one page to work with
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // Create a TextFragment with multiple lines
            TextFragment tf = new TextFragment("First line\nSecond line\nThird line");

            // Set a custom line height (line spacing) via TextState
            tf.TextState.LineSpacing = 20f; // line height in points

            // Add the text fragment to the page
            page.Paragraphs.Add(tf);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom line height to '{outputPath}'.");
    }
}