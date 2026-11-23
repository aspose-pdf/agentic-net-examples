using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text; // for FontStyles, Color, etc.

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
            // Verify that the document contains a tagged structure
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("Document is not tagged. No logical structure to modify.");
                return;
            }

            // Access the tagged content API
            ITaggedContent taggedContent = doc.TaggedContent;

            // Get the root of the structure tree
            var root = taggedContent.RootElement;

            // Find the first List element in the structure (recursive search)
            var lists = root.FindElements<ListElement>(true);
            if (lists.Count == 0)
            {
                Console.WriteLine("No List element found in the document.");
                return;
            }

            ListElement list = lists[0]; // use the first list found

            // ------------------------------------------------------------
            // Helper method to create a ListLI (list item) with label and body text
            // ------------------------------------------------------------
            ListLIElement CreateListItem(string labelText, string bodyText)
            {
                // Create the list item container
                ListLIElement li = taggedContent.CreateListLIElement();

                // Create and fill the label part (e.g., "1.")
                ListLblElement lbl = taggedContent.CreateListLblElement();
                SpanElement lblSpan = taggedContent.CreateSpanElement();
                lblSpan.SetText(labelText);
                lbl.AppendChild(lblSpan);

                // Create and fill the body part (the actual list item text)
                ListLBodyElement body = taggedContent.CreateListLBodyElement();
                ParagraphElement bodyPara = taggedContent.CreateParagraphElement();
                bodyPara.SetText(bodyText);
                body.AppendChild(bodyPara);

                // Assemble the list item
                li.AppendChild(lbl);
                li.AppendChild(body);

                return li;
            }

            // Create the first list item and insert it as the last child of the list
            ListLIElement listItem1 = CreateListItem("1.", "First inserted list item");
            // Append at the end: use the current child count as index and set isAppend = true
            list.InsertChild(listItem1, list.ChildElements.Count, true);

            // Create the second list item and insert it at index 1 (second position)
            ListLIElement listItem2 = CreateListItem("2.", "Second inserted list item");
            // Insert at position 1 without appending (isAppend = false)
            list.InsertChild(listItem2, 1, false);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
