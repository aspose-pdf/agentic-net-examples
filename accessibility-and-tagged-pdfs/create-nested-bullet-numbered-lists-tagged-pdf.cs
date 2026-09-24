using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "AccessibleList.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Enable tagged PDF and set language/title
            ITaggedContent taggedContent = doc.TaggedContent;
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle("Nested List Example");

            // Root element of the structure tree
            StructureElement root = taggedContent.RootElement;

            // ----- Create visual list content -----
            // Define bullet and numbered list items
            List<string> bulletItems = new List<string>
            {
                "• First bullet item",
                "• Second bullet item",
                "• Third bullet item"
            };

            List<string> numberedItems = new List<string>
            {
                "1. First numbered item",
                "2. Second numbered item",
                "3. Third numbered item"
            };

            // Helper to add a paragraph both to the page and to the tag structure
            void AddParagraph(string text)
            {
                // Visual paragraph on the page
                TextFragment tf = new TextFragment(text);
                tf.Margin = new MarginInfo(0, 0, 0, 0);
                page.Paragraphs.Add(tf);

                // Corresponding tagged structure element
                ParagraphElement para = taggedContent.CreateParagraphElement();
                para.SetText(text);
                root.AppendChild(para);
            }

            // Add bullet list items
            foreach (string item in bulletItems)
                AddParagraph(item);

            // Add a blank line between lists
            AddParagraph(string.Empty);

            // Add numbered list items
            foreach (string item in numberedItems)
                AddParagraph(item);

            // Save the tagged PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with nested list saved to '{outputPath}'.");
    }
}