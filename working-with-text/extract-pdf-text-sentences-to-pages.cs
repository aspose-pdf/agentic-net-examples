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

        // Load source PDF and extract all text
        string fullText;
        using (Document srcDoc = new Document(inputPath))
        {
            TextAbsorber absorber = new TextAbsorber();
            srcDoc.Pages.Accept(absorber);
            fullText = absorber.Text;
        }

        // Split the extracted text into sentences (simple regex)
        string[] sentences = Regex.Split(fullText, @"(?<=[.!?])\s+");

        // Create a new PDF document to hold each sentence on its own page
        using (Document outDoc = new Document())
        {
            foreach (string sentence in sentences)
            {
                // Skip empty entries that may result from splitting
                if (string.IsNullOrWhiteSpace(sentence))
                    continue;

                // Add a new page
                Page page = outDoc.Pages.Add();

                // Create a TextFragment for the sentence
                TextFragment fragment = new TextFragment(sentence);
                // Position the text near the top-left of the page
                fragment.Position = new Position(50, 800);

                // Append the fragment to the page
                TextBuilder builder = new TextBuilder(page);
                builder.AppendText(fragment);
            }

            // Save the resulting PDF
            outDoc.Save(outputPath);
        }

        Console.WriteLine($"Sentences PDF saved to '{outputPath}'.");
    }
}