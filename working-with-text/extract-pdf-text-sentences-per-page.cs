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
        const string outputPath = "sentences.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load source PDF and extract all its text
        using (Document srcDoc = new Document(inputPath))
        {
            TextAbsorber absorber = new TextAbsorber();
            srcDoc.Pages.Accept(absorber);
            string fullText = absorber.Text ?? string.Empty;

            // Split the extracted text into sentences.
            // This simple regex splits on period, exclamation or question mark followed by whitespace.
            string[] sentences = Regex.Split(fullText, @"(?<=[\.!\?])\s+");

            // Create a new PDF to hold each sentence on its own page
            using (Document outDoc = new Document())
            {
                bool firstPageUsed = true; // Document() already contains one page

                foreach (string rawSentence in sentences)
                {
                    string sentence = rawSentence.Trim();
                    if (string.IsNullOrEmpty(sentence))
                        continue; // skip empty entries

                    Page page;
                    if (firstPageUsed)
                    {
                        // Use the initially created page for the first sentence
                        page = outDoc.Pages[1];
                        firstPageUsed = false;
                    }
                    else
                    {
                        // Add a new page for subsequent sentences
                        outDoc.Pages.Add();
                        page = outDoc.Pages[outDoc.Pages.Count];
                    }

                    // Create a TextFragment with the sentence
                    TextFragment tf = new TextFragment(sentence);
                    // Position the text near the top-left of the page
                    tf.Position = new Position(50, 800);
                    // Optional: set font and color
                    tf.TextState.FontSize = 12;
                    tf.TextState.Font = FontRepository.FindFont("Helvetica");
                    tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                    // Append the fragment to the page
                    TextBuilder builder = new TextBuilder(page);
                    builder.AppendText(tf);
                }

                // Save the resulting PDF
                outDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"Sentences PDF saved to '{outputPath}'.");
    }
}