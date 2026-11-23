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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // The TaggedContent property is read‑only. If the PDF is not tagged, we cannot create a tagged structure
                // via this API, so we simply abort with a clear message.
                ITaggedContent? tagged = doc.TaggedContent;
                if (tagged == null)
                {
                    Console.Error.WriteLine("The PDF does not contain a tagged structure and cannot be modified as a tagged PDF.");
                    return;
                }

                // Optional metadata for the tagged PDF.
                tagged.SetLanguage("en-US");
                tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Locate a DivElement (section) to which the paragraph will be added.
                // If none exists, fall back to the root element.
                var divElements = tagged.RootElement.FindElements<DivElement>(true);
                StructureElement targetSection = divElements.Count > 0 ? divElements[0] : tagged.RootElement;

                // Create a new paragraph element and set its text.
                ParagraphElement paragraph = tagged.CreateParagraphElement();
                paragraph.SetText("This paragraph was appended programmatically.");

                // Append the paragraph to the chosen section.
                targetSection.AppendChild(paragraph);

                // Save the modified PDF.
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
