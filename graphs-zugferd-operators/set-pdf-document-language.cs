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

        // Load the PDF document within a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Obtain the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Set the primary language of the document (helps screen readers)
            tagged.SetLanguage(language);

            // Optional: set a title (write‑only)
            // tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Language set to '{language}' and saved to '{outputPath}'.");
    }
}