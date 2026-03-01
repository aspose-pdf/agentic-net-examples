using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "titled_output.pdf";
        const string title      = "Accessible Document Title";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Access tagged content and set the document title for accessibility
                ITaggedContent tagged = doc.TaggedContent;
                tagged.SetTitle(title);

                // Save the updated PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with title to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}