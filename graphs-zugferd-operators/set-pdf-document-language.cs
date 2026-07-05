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

        // Load the PDF, set the document language, and save
        using (Document doc = new Document(inputPath))
        {
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage(language);
            doc.Save(outputPath);
        }

        Console.WriteLine($"Language set to '{language}' and saved to '{outputPath}'.");
    }
}