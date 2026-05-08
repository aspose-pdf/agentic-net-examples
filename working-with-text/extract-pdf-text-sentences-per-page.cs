using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "sentences_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF and extract its full text.
        string fullText;
        using (Document srcDoc = new Document(inputPath))
        {
            TextAbsorber absorber = new TextAbsorber();
            srcDoc.Pages.Accept(absorber);
            fullText = absorber.Text;
        }

        // Split the extracted text into sentences.
        // This regex splits after '.', '!' or '?' followed by whitespace.
        string[] sentences = Regex.Split(fullText, @"(?<=[.!?])\s+");

        // Create a new PDF document to hold each sentence on its own page.
        using (Document outDoc = new Document())
        {
            // Prepare a font to be used for all sentences.
            Aspose.Pdf.Text.Font font = FontRepository.FindFont("Helvetica");

            foreach (string sentence in sentences)
            {
                // Skip empty entries that may result from splitting.
                if (string.IsNullOrWhiteSpace(sentence))
                    continue;

                // Add a new page.
                Page page = outDoc.Pages.Add();

                // Create a TextFragment for the sentence.
                TextFragment tf = new TextFragment(sentence);
                tf.TextState.Font = font;
                tf.TextState.FontSize = 12;
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Position the text near the top-left of the page.
                tf.Position = new Position(50, 800); // X=50, Y=800

                // Add the fragment to the page.
                page.Paragraphs.Add(tf);
            }

            // Save the resulting PDF.
            outDoc.Save(outputPath);
        }

        Console.WriteLine($"Sentences PDF saved to '{outputPath}'.");
    }
}
