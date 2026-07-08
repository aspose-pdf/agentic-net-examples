using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "sentences.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Extract all text from the source PDF
        string fullText;
        using (Document srcDoc = new Document(inputPath))
        {
            TextAbsorber absorber = new TextAbsorber
            {
                // Use pure formatting mode for cleaner text
                ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            };
            srcDoc.Pages.Accept(absorber);
            fullText = absorber.Text;
        }

        // Split the extracted text into individual sentences
        List<string> sentences = SplitIntoSentences(fullText);

        // Create a new PDF and place each sentence on its own page
        using (Document outDoc = new Document())
        {
            foreach (string sentence in sentences)
            {
                // Add a blank page
                Page page = outDoc.Pages.Add();

                // Create a TextFragment containing the sentence
                TextFragment tf = new TextFragment(sentence)
                {
                    // Position the text near the top-left corner
                    Position = new Position(50, 800),
                    // Optional styling
                    TextState = { FontSize = 12, Font = FontRepository.FindFont("Helvetica"), ForegroundColor = Color.Black }
                };

                // Append the fragment to the page
                TextBuilder builder = new TextBuilder(page);
                builder.AppendText(tf);
            }

            // Save the resulting PDF
            outDoc.Save(outputPath);
        }

        Console.WriteLine($"Sentences PDF saved to '{outputPath}'.");
    }

    // Simple sentence splitter using a regular expression
    static List<string> SplitIntoSentences(string text)
    {
        var result = new List<string>();
        if (string.IsNullOrWhiteSpace(text))
            return result;

        // Split on punctuation followed by whitespace
        string[] parts = Regex.Split(text, @"(?<=[\.!\?])\s+");
        foreach (string part in parts)
        {
            string trimmed = part.Trim();
            if (!string.IsNullOrEmpty(trimmed))
                result.Add(trimmed);
        }
        return result;
    }
}