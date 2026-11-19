using System;
using System.IO;
using System.Collections.Generic;
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

        // Extract all text from the source PDF
        List<string> sentences = new List<string>();
        using (Document srcDoc = new Document(inputPath))
        {
            TextAbsorber absorber = new TextAbsorber();
            srcDoc.Pages.Accept(absorber);
            string fullText = absorber.Text ?? string.Empty;

            // Split the text into sentences (simple delimiters)
            char[] delimiters = new char[] { '.', '!', '?' };
            string[] rawSentences = fullText.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in rawSentences)
            {
                string trimmed = s.Trim();
                if (trimmed.Length > 0)
                {
                    // Preserve a period at the end of each sentence
                    sentences.Add(trimmed + ".");
                }
            }
        }

        // Create a new PDF where each sentence occupies its own page
        using (Document outDoc = new Document())
        {
            foreach (string sentence in sentences)
            {
                // Add a blank page
                Page page = outDoc.Pages.Add();

                // Create a TextFragment for the sentence
                TextFragment tf = new TextFragment(sentence);
                tf.Position = new Position(50, 750); // 50 points from left, 750 from bottom
                tf.TextState.FontSize = 12;
                tf.TextState.Font = FontRepository.FindFont("Arial");

                // Add the fragment to the page
                page.Paragraphs.Add(tf);
            }

            // Save the resulting PDF
            outDoc.Save(outputPath);
        }

        Console.WriteLine($"Sentences PDF saved to '{outputPath}'.");
    }
}