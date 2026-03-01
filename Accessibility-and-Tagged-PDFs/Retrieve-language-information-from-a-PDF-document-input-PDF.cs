using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Check if the document contains tagged content
            string language = string.Empty;
            if (doc.IsTagged)
            {
                // Retrieve the language identifier from the tagged content
                language = doc.TaggedContent.Language ?? string.Empty;
            }

            // If the language is not set or the document is not tagged, indicate that it is unavailable
            if (string.IsNullOrEmpty(language))
            {
                language = "(language not specified)";
            }

            Console.WriteLine($"Document language: {language}");
        }
    }
}