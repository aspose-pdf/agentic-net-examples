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

        // ------------------------------------------------------------
        // 1. Extract all text from the source PDF using TextAbsorber.
        // ------------------------------------------------------------
        string fullText;
        using (Document srcDoc = new Document(inputPath))
        {
            TextAbsorber absorber = new TextAbsorber
            {
                // Use pure formatting to get readable text.
                ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            };
            srcDoc.Pages.Accept(absorber);
            fullText = absorber.Text;
        }

        // ------------------------------------------------------------
        // 2. Split the extracted text into individual sentences.
        //    This simple splitter uses punctuation marks as delimiters.
        // ------------------------------------------------------------
        List<string> sentences = SplitIntoSentences(fullText);

        // ------------------------------------------------------------
        // 3. Create a new PDF and place each sentence on its own page.
        // ------------------------------------------------------------
        using (Document outDoc = new Document())
        {
            foreach (string sentence in sentences)
            {
                // Add a blank page (1‑based indexing is handled internally).
                Page page = outDoc.Pages.Add();

                // Create a TextFragment containing the sentence.
                TextFragment tf = new TextFragment(sentence)
                {
                    // Position the text near the top‑left corner.
                    Position = new Position(50, 800)
                };

                // Optional styling (font, size, color).
                tf.TextState.FontSize = 12;
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Append the fragment to the page.
                TextBuilder builder = new TextBuilder(page);
                builder.AppendText(tf);
            }

            // Save the resulting PDF.
            outDoc.Save(outputPath);
        }

        Console.WriteLine($"Sentences PDF saved to '{outputPath}'.");
    }

    // Helper method: splits a block of text into sentences.
    // Keeps the trailing punctuation and trims whitespace.
    static List<string> SplitIntoSentences(string text)
    {
        var result = new List<string>();
        if (string.IsNullOrWhiteSpace(text))
            return result;

        // Regex: look for ., !, or ? followed by whitespace.
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