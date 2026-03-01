using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;          // Facade classes (e.g., Form) can be used if needed
using Aspose.Pdf.Tagged;          // ITaggedContent for accessibility
using Aspose.Pdf.LogicalStructure; // StructureElement types

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Optional: use a Facade (e.g., Form) to demonstrate Facades usage
            Form formFacade = new Form(doc);
            // No further operations needed for the facade in this example

            // Access tagged content for accessibility processing
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title metadata (write‑only setters)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Retrieve the root structure element (no cast required)
            StructureElement root = tagged.RootElement;
            Console.WriteLine($"Root element type: {root.GetType().Name}");

            // Save the (potentially modified) document
            doc.Save(outputPath); // No SaveOptions needed for PDF output
        }

        Console.WriteLine($"Document processed and saved to '{outputPath}'.");
    }
}