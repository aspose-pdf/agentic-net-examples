using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string phrase = "your specific phrase";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a TextAbsorber to extract all text from the document
            TextAbsorber absorber = new TextAbsorber();

            // Extract text from all pages
            doc.Pages.Accept(absorber);
            string fullText = absorber.Text;

            // Locate every occurrence of the specified phrase (case‑insensitive)
            int startIndex = 0;
            int occurrence = 0;
            while ((startIndex = fullText.IndexOf(phrase, startIndex, StringComparison.OrdinalIgnoreCase)) >= 0)
            {
                occurrence++;
                Console.WriteLine($"Occurrence {occurrence}: character position {startIndex}");
                startIndex += phrase.Length; // Move past the current match
            }

            if (occurrence == 0)
            {
                Console.WriteLine("Phrase not found in the document.");
            }
        }
    }
}