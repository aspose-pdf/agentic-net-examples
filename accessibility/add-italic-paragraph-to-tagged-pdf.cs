using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;   // ITaggedContent, CreateParagraphElement, etc.
using Aspose.Pdf.Text;    // FontStyles

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged‑content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set language and title for the document
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root of the structure tree – use dynamic to avoid compile‑time type dependency
            dynamic root = tagged.RootElement;

            // Create a new paragraph structure element – also obtained as dynamic
            dynamic para = tagged.CreateParagraphElement();

            // Set the displayed text of the paragraph
            para.SetText("This paragraph is italic via StructureTextState.");

            // Configure the text state to use italic font style.
            // StructureTextState is exposed as a property on the element; we modify it directly.
            para.StructureTextState.FontStyle = FontStyles.Italic;

            // Attach the paragraph to the root of the structure tree
            root.AppendChild(para);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with italic paragraph to '{outputPath}'.");
    }
}
