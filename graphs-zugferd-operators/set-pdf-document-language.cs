using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string language = "en-US";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content helper
            ITaggedContent tagged = doc.TaggedContent;
            // Set the primary language for accessibility (screen readers)
            tagged.SetLanguage(language);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Language set to '{language}' and saved to '{outputPath}'.");
    }
}