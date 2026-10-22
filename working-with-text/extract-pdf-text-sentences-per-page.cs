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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF and extract its text using the core TextAbsorber API.
        Document srcDoc = new Document(inputPath);
        TextAbsorber absorber = new TextAbsorber();
        srcDoc.Pages.Accept(absorber);
        string fullText = absorber.Text ?? string.Empty;

        // Split the extracted text into individual sentences.
        List<string> sentences = SplitIntoSentences(fullText);

        // Create a new PDF where each sentence occupies its own page.
        Document outDoc = new Document();
        // Aspose.Pdf requires at least one page to exist before adding content.
        outDoc.Pages.Add();

        foreach (string sentence in sentences)
        {
            // Add a blank page for the current sentence.
            Page page = outDoc.Pages.Add();

            // Create a TextFragment for the sentence.
            TextFragment tf = new TextFragment(sentence)
            {
                TextState = { FontSize = 12 }
            };

            // Try to use Arial; if not found, the default font will be used.
            Font font = FontRepository.FindFont("Arial");
            if (font != null)
                tf.TextState.Font = font;

            // Add the fragment to the page.
            page.Paragraphs.Add(tf);
        }

        // Save the resulting PDF.
        outDoc.Save(outputPath);
        Console.WriteLine($"Sentences PDF saved to '{outputPath}'.");
    }

    // Splits a block of text into sentences using a simple regular expression.
    static List<string> SplitIntoSentences(string text)
    {
        var result = new List<string>();
        if (string.IsNullOrWhiteSpace(text))
            return result;

        // Split after a period, exclamation mark, or question mark followed by whitespace.
        const string pattern = @"(?<=[\.\!\?])\s+";
        foreach (string part in Regex.Split(text, pattern))
        {
            string trimmed = part.Trim();
            if (!string.IsNullOrEmpty(trimmed))
                result.Add(trimmed);
        }
        return result;
    }
}