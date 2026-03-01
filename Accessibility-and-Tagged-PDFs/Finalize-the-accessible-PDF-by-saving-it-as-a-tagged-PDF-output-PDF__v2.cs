using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "tagged_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Access the tagged content API
                ITaggedContent tagged = doc.TaggedContent;

                // Set language and title for the PDF (write‑only setters)
                tagged.SetLanguage("en-US");
                tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Add a simple paragraph to the logical structure tree
                StructureElement root = tagged.RootElement; // no cast needed
                ParagraphElement para = tagged.CreateParagraphElement();
                para.SetText("This PDF has been made accessible using Aspose.Pdf.");
                root.AppendChild(para); // AppendChild with a single argument

                // Persist the tagged structure changes to the document
                tagged.Save();

                // Save the final tagged PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}