using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "styled_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block (document‑disposal‑with‑using rule)
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (must be a tagged PDF to modify structure)
            ITaggedContent taggedContent = doc.TaggedContent;

            // Get the root of the structure tree (no cast needed)
            StructureElement root = taggedContent.RootElement;

            // Find the first paragraph element in the structure tree
            var paragraphs = root.FindElements<ParagraphElement>(true);
            if (paragraphs.Count > 0)
            {
                ParagraphElement paragraph = paragraphs[0];

                // Apply styling via the read‑only StructureTextState property (members are settable)
                paragraph.StructureTextState.Font = FontRepository.FindFont("Arial");
                paragraph.StructureTextState.FontSize = 12;
                paragraph.StructureTextState.FontStyle = FontStyles.Bold;
                paragraph.StructureTextState.ForegroundColor = Color.Red; // Aspose.Pdf.Color
            }
            else
            {
                Console.WriteLine("No paragraph elements found to style.");
            }

            // Save the modified PDF (PDF format, so no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Styled PDF saved to '{outputPath}'.");
    }
}
