using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string language   = "en-US";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF
            using (Document doc = new Document(inputPath))
            {
                // Access tagged content and set the document language
                ITaggedContent tagged = doc.TaggedContent;
                tagged.SetLanguage(language);
                // Optionally set a title for the PDF
                tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Document language set to '{language}' and saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}