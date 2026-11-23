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
        const string inputPath  = "input.pdf";
        const string outputPath = "tagged_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent taggedContent = doc.TaggedContent;

            // Set document language and title (write‑only setters)
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root of the structure tree (no cast required)
            StructureElement root = taggedContent.RootElement;

            // ---------- Create a Header element ----------
            // Level 1 header (equivalent to <H1>)
            HeaderElement header1 = taggedContent.CreateHeaderElement(1);
            header1.SetText("Chapter 1: Introduction");

            // Apply distinct font styling to the header via StructureTextState
            header1.StructureTextState.Font = FontRepository.FindFont("Helvetica-Bold");
            header1.StructureTextState.FontSize = 24f;
            header1.StructureTextState.FontStyle = FontStyles.Bold;

            // Attach the header to the root
            root.AppendChild(header1);

            // ---------- Create a Paragraph element ----------
            ParagraphElement para1 = taggedContent.CreateParagraphElement();
            para1.SetText("This paragraph provides an overview of the document. It uses a different font style from the header.");

            // Apply distinct font styling to the paragraph via StructureTextState
            para1.StructureTextState.Font = FontRepository.FindFont("Times New Roman");
            para1.StructureTextState.FontSize = 12f;
            para1.StructureTextState.FontStyle = FontStyles.Regular;

            // Attach the paragraph to the root (after the header)
            root.AppendChild(para1);

            // ---------- Create a second Header element ----------
            HeaderElement header2 = taggedContent.CreateHeaderElement(2);
            header2.SetText("Section 1.1: Details");

            // Styling for the second header
            header2.StructureTextState.Font = FontRepository.FindFont("Arial");
            header2.StructureTextState.FontSize = 18f;
            header2.StructureTextState.FontStyle = FontStyles.Italic;

            root.AppendChild(header2);

            // ---------- Create a second Paragraph element ----------
            ParagraphElement para2 = taggedContent.CreateParagraphElement();
            para2.SetText("Further details are described in this section, using a smaller font size.");

            // Styling for the second paragraph
            para2.StructureTextState.Font = FontRepository.FindFont("Courier New");
            para2.StructureTextState.FontSize = 10f;
            // "Oblique" is not a valid FontStyles value; use Italic instead
            para2.StructureTextState.FontStyle = FontStyles.Italic;

            root.AppendChild(para2);

            // Save the modified PDF (no PreSave call required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}
