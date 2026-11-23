using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for GoToURIAction and LinkAnnotation
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "tagged_input.pdf";
        const string outputPath = "tagged_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing tagged PDF
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent taggedContent = doc.TaggedContent;
            // Optional: set language and title for the document
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root of the structure tree
            StructureElement root = taggedContent.RootElement;

            // Create a table element and attach it to the root
            TableElement table = taggedContent.CreateTableElement();
            table.AlternativeText = "Sample data table with a link";
            root.AppendChild(table);

            // ----- Table Header -----
            TableTHeadElement thead = taggedContent.CreateTableTHeadElement();
            table.AppendChild(thead);

            TableTRElement headerRow = taggedContent.CreateTableTRElement();
            thead.AppendChild(headerRow);

            TableTHElement th1 = taggedContent.CreateTableTHElement();
            th1.SetText("Product");
            headerRow.AppendChild(th1);

            TableTHElement th2 = taggedContent.CreateTableTHElement();
            th2.SetText("Link");
            headerRow.AppendChild(th2);

            // ----- Table Body -----
            TableTBodyElement tbody = taggedContent.CreateTableTBodyElement();
            table.AppendChild(tbody);

            TableTRElement dataRow = taggedContent.CreateTableTRElement();
            tbody.AppendChild(dataRow);

            // First cell: plain text
            TableTDElement tdProduct = taggedContent.CreateTableTDElement();
            tdProduct.SetText("Widget A");
            dataRow.AppendChild(tdProduct);

            // Second cell: a link element (structure element only)
            TableTDElement tdLink = taggedContent.CreateTableTDElement();
            dataRow.AppendChild(tdLink);

            // Create the link *structure* element and set its accessible properties
            LinkElement linkElement = taggedContent.CreateLinkElement();
            linkElement.Title = "Visit Site";                     // accessible name
            linkElement.AlternativeText = "Link to example.com"; // description for assistive tech
            tdLink.AppendChild(linkElement);

            // Create a visual link annotation on the first page so the PDF actually contains a clickable link
            // (the annotation rectangle is placed arbitrarily; adjust as needed for real layout)
            Page firstPage = doc.Pages[1];
            var linkRect = new Rectangle(100, 700, 200, 720); // left, bottom, right, top
            LinkAnnotation linkAnnot = new LinkAnnotation(firstPage, linkRect)
            {
                // The visible text of the annotation is stored in Contents; the actual URI is set via Action
                Contents = "Visit Site"
            };
            linkAnnot.Action = new GoToURIAction("https://example.com");
            firstPage.Annotations.Add(linkAnnot);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with link saved to '{outputPath}'.");
    }
}
