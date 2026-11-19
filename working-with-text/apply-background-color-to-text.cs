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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to add the text to
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // Create a text fragment with the desired content
            TextFragment tf = new TextFragment("Highlighted text");

            // Apply a background color using Aspose.Pdf.Color (cross‑platform)
            tf.TextState.BackgroundColor = Aspose.Pdf.Color.Yellow;

            // Optionally set the position of the text on the page
            tf.Position = new Position(100, 700);

            // Add the text fragment to the page's paragraph collection
            page.Paragraphs.Add(tf);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with background‑colored text to '{outputPath}'.");
    }
}