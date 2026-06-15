using System;
using System.IO;
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

        // Load the source PDF
        using (Document srcDoc = new Document(inputPath))
        {
            // Extract all text from the document
            TextAbsorber absorber = new TextAbsorber();
            srcDoc.Pages.Accept(absorber);
            string fullText = absorber.Text;

            // Split the extracted text into sentences
            string[] sentences = Regex.Split(fullText, @"(?<=[.!?])\s+");

            // Create a new PDF to store each sentence on a separate page
            using (Document outDoc = new Document())
            {
                foreach (string rawSentence in sentences)
                {
                    string sentence = rawSentence.Trim();
                    if (string.IsNullOrEmpty(sentence))
                        continue;

                    // Add a new page for the sentence
                    Page page = outDoc.Pages.Add();

                    // Create a TextFragment containing the sentence
                    TextFragment tf = new TextFragment(sentence);

                    // Position the fragment near the top-left of the page
                    tf.Position = new Position(50, 750);

                    // Add the fragment to the page
                    page.Paragraphs.Add(tf);
                }

                // Save the resulting PDF
                outDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"Sentences PDF saved to '{outputPath}'.");
    }
}