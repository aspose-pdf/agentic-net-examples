using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "titled_output.pdf";
        const string title = "Accessible Document Title";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF
            using (Document doc = new Document(inputPath))
            {
                // Access tagged content and set the document title
                ITaggedContent tagged = doc.TaggedContent;
                tagged.SetTitle(title);

                // Save the PDF with the new title
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