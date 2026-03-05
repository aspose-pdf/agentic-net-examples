using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure; // for StructureElement if needed

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "exported.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPath))
            {
                // Access the tagged content of the document
                ITaggedContent tagged = doc.TaggedContent;

                // Example: set language and title (optional)
                tagged.SetLanguage("en-US");
                tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Save the tagged content changes back into the PDF structure
                tagged.Save();

                // Export the (now tagged) PDF to a new file
                doc.Save(outputPath);
            }

            Console.WriteLine($"Tagged PDF exported to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}