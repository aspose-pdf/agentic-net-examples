using System;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "accessible_output.pdf";

        // Enable global auto‑tagging (optional, improves automatic heading detection)
        AutoTaggingSettings.Default.EnableAutoTagging = true;
        AutoTaggingSettings.Default.HeadingRecognitionStrategy = HeadingRecognitionStrategy.Auto;

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Set document language and title for accessibility
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Sample Accessible PDF");

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // -------------------------------------------------
            // Heading (Level 1)
            // -------------------------------------------------
            HeaderElement heading = tagged.CreateHeaderElement(1);
            // heading.SetTitle("Sample Heading"); // SetTitle is not available in current API
            heading.SetText("Sample Heading");
            root.AppendChild(heading);

            // Visual representation of the heading
            TextFragment headingTf = new TextFragment("Sample Heading")
            {
                TextState = { FontSize = 20, FontStyle = FontStyles.Bold }
            };
            page.Paragraphs.Add(headingTf);

            // -------------------------------------------------
            // Paragraph
            // -------------------------------------------------
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            // paragraph.SetTitle("Sample Paragraph"); // SetTitle is not available in current API
            paragraph.SetText("This is a sample paragraph that demonstrates accessibility tagging using Aspose.Pdf.");
            root.AppendChild(paragraph);

            TextFragment paraTf = new TextFragment(
                "This is a sample paragraph that demonstrates accessibility tagging using Aspose.Pdf.")
            {
                TextState = { FontSize = 12 }
            };
            page.Paragraphs.Add(paraTf);

            // -------------------------------------------------
            // Table
            // -------------------------------------------------
            TableElement tableTag = tagged.CreateTableElement();
            // tableTag.SetTitle("Sample Table"); // SetTitle is not available in current API
            root.AppendChild(tableTag);

            // Create a simple 2x2 table
            Table table = new Table
            {
                ColumnWidths = "200 200",
                Border = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Header row
            Row headerRow = table.Rows.Add();
            headerRow.Cells.Add("Header 1");
            headerRow.Cells.Add("Header 2");

            // Data row
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add("Cell 1");
            dataRow.Cells.Add("Cell 2");

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to {outputPath}");
    }
}
