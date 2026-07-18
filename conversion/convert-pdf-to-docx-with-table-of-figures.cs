using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDocxPath = "output.docx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Access tagged content (creates a tagged structure if none exists)
            ITaggedContent tagged = pdfDoc.TaggedContent;
            tagged.SetLanguage("en-US");
            // Fully qualify System.IO.Path to avoid ambiguity with Aspose.Pdf.Drawing.Path
            tagged.SetTitle(System.IO.Path.GetFileNameWithoutExtension(inputPdfPath));

            // Root element of the tagged structure
            StructureElement root = tagged.RootElement;

            // Create a table that will become the Table of Figures
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Table of Figures";
            root.AppendChild(table);

            // Header row
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            TableTHElement thFigure = tagged.CreateTableTHElement();
            thFigure.SetText("Figure");
            headerRow.AppendChild(thFigure);

            TableTHElement thDesc = tagged.CreateTableTHElement();
            thDesc.SetText("Description");
            headerRow.AppendChild(thDesc);

            TableTHElement thPage = tagged.CreateTableTHElement();
            thPage.SetText("Page");
            headerRow.AppendChild(thPage);

            // Body of the table
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            int figureIndex = 1;

            // Iterate through pages and their images
            foreach (Page page in pdfDoc.Pages)
            {
                foreach (XImage img in page.Resources.Images)
                {
                    // Create a simple description for the figure
                    string description = $"Figure {figureIndex}";
                    // Set alternative text on the image (useful for accessibility)
                    img.TrySetAlternativeText(description, page);

                    // Add a row for this image
                    TableTRElement row = tagged.CreateTableTRElement();
                    tbody.AppendChild(row);

                    // Figure number cell
                    TableTDElement tdFigure = tagged.CreateTableTDElement();
                    tdFigure.SetText(figureIndex.ToString());
                    row.AppendChild(tdFigure);

                    // Description cell
                    TableTDElement tdDesc = tagged.CreateTableTDElement();
                    tdDesc.SetText(description);
                    row.AppendChild(tdDesc);

                    // Page number cell
                    TableTDElement tdPage = tagged.CreateTableTDElement();
                    tdPage.SetText(page.Number.ToString());
                    row.AppendChild(tdPage);

                    figureIndex++;
                }
            }

            // Convert the PDF (with the added Table of Figures) to DOCX
            pdfDoc.Save(outputDocxPath, SaveFormat.DocX);
        }
    }
}
