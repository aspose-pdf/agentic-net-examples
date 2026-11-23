using System;
using System.IO;
using System.Text;
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
        const string reportPath = "tag_change_report.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        StringBuilder reportBuilder = new StringBuilder();

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent taggedContent = doc.TaggedContent;

            // Optionally set language and title for the tagged PDF
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the logical structure tree (no cast required)
            StructureElement root = taggedContent.RootElement;

            // Iterate over each page, extract its plain text and create a Paragraph tag
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                // Extract text from the current page
                TextAbsorber absorber = new TextAbsorber {
                    ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
                };
                doc.Pages[pageNumber].Accept(absorber);
                string pageText = absorber.Text?.Trim();

                if (!string.IsNullOrEmpty(pageText))
                {
                    // Create a new paragraph element and set its text
                    ParagraphElement paragraph = taggedContent.CreateParagraphElement();
                    paragraph.SetText(pageText);

                    // Attach the paragraph to the root of the structure tree
                    root.AppendChild(paragraph);

                    // Record the change in the report
                    reportBuilder.AppendLine($"Page {pageNumber}: Added Paragraph tag (text length = {pageText.Length})");
                }
                else
                {
                    reportBuilder.AppendLine($"Page {pageNumber}: No text extracted; no tag added.");
                }
            }

            // Save the modified PDF (no PreSave call required)
            doc.Save(outputPath);
        }

        // Write the tag change report to a text file
        File.WriteAllText(reportPath, reportBuilder.ToString());

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
        Console.WriteLine($"Tag change report written to '{reportPath}'.");
    }
}