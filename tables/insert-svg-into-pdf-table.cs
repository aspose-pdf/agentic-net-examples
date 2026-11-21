using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string svgPath   = "image.svg";

        // Verify required files exist
        if (!File.Exists(inputPdf) || !File.Exists(svgPath))
        {
            Console.Error.WriteLine("Input PDF or SVG file not found.");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Access tagged content (creates a tagged structure if none exists)
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPdf));

            // Root element of the structure tree
            StructureElement root = tagged.RootElement;

            // -------------------------------------------------
            // Create a table with two columns: description + image
            // -------------------------------------------------
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Table containing an SVG image";
            root.AppendChild(table);

            // Header row
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            TableTHElement thDesc = tagged.CreateTableTHElement();
            thDesc.SetText("Description");
            headerRow.AppendChild(thDesc);

            TableTHElement thImg = tagged.CreateTableTHElement();
            thImg.SetText("Image");
            headerRow.AppendChild(thImg);

            // Body row
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);
            TableTRElement bodyRow = tagged.CreateTableTRElement();
            tbody.AppendChild(bodyRow);

            // Description cell
            TableTDElement tdDesc = tagged.CreateTableTDElement();
            tdDesc.SetText("Sample SVG graphic");
            bodyRow.AppendChild(tdDesc);

            // Image cell – we place a Figure element here
            TableTDElement tdImg = tagged.CreateTableTDElement();
            FigureElement figure = tagged.CreateFigureElement();
            figure.AlternativeText = "Embedded SVG image";
            tdImg.AppendChild(figure);
            bodyRow.AppendChild(tdImg);

            // -------------------------------------------------
            // Load the SVG and add it to the first page
            // -------------------------------------------------
            Page page = doc.Pages[1]; // 1‑based indexing
            using (FileStream svgStream = File.OpenRead(svgPath))
            {
                // Add the SVG as an image to the page content
                Image svgImage = new Image
                {
                    ImageStream = svgStream,
                    // Set a reasonable size (points)
                    FixWidth  = 100,
                    FixHeight = 100
                };
                page.Paragraphs.Add(svgImage);
            }

            // Save the modified document as PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with table and SVG saved to '{outputPdf}'.");
    }
}