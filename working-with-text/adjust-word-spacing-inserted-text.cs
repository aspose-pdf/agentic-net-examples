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

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // Create a text fragment with the desired content
            TextFragment tf = new TextFragment("Sample spaced text");

            // Configure the text appearance and word spacing
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 14;
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue; // corrected property
            tf.TextState.WordSpacing = 2.0f; // increase spacing between words

            // Position the text on the page
            tf.Position = new Position(100, 700);

            // Add the fragment to the page
            page.Paragraphs.Add(tf);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved modified PDF to '{outputPath}'.");
    }
}
