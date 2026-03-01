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
            // The language identifier is stored in the document's Info dictionary under the "Lang" key.
            // ITaggedContent provides only setters (SetLanguage/SetTitle), so we read from Info.
            string language = doc.Info["Lang"];

            if (string.IsNullOrEmpty(language))
            {
                Console.WriteLine("Language information is not present in the PDF.");
            }
            else
            {
                Console.WriteLine($"Document language: {language}");
            }
        }
    }
}