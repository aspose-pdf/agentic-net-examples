using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_updated.pdf";

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
                // Access and modify standard metadata via DocumentInfo
                DocumentInfo info = doc.Info;
                info.Title = "Updated Document Title";
                info.Author = "John Doe";
                info.Subject = "Accessibility Update";
                info.Keywords = "PDF, Accessibility, Aspose";
                info.Creator = "My Application";

                // Update accessibility tags using the tagged content API
                ITaggedContent tagged = doc.TaggedContent;
                tagged.SetLanguage("en-US");          // Set document language
                tagged.SetTitle(info.Title);          // Sync tagged title with metadata

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}