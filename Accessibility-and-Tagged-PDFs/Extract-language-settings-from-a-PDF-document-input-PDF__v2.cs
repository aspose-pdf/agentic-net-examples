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

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Access tagged content (if any)
                ITaggedContent tagged = doc.TaggedContent;

                // Try to read language from the tagged content.
                // If the PDF is not tagged, Language will be null.
                string language = tagged?.Language ?? "(not a tagged PDF)";

                // Standard metadata (Title, Author, etc.) are stored in doc.Info
                string title = doc.Info.Title ?? "(no title)";
                string author = doc.Info.Author ?? "(no author)";

                Console.WriteLine($"Title   : {title}");
                Console.WriteLine($"Author  : {author}");
                Console.WriteLine($"Language: {language}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}