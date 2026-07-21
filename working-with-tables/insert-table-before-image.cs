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

        // Load the PDF
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content
            ITaggedContent tagged = doc.TaggedContent;

            // Find the first image represented as a FigureElement in the structure tree
            FigureElement imageFigure = null;
            var figures = tagged.RootElement.FindElements<FigureElement>(true);
            if (figures.Count > 0)
                imageFigure = figures[0];

            if (imageFigure != null)
            {
                // Determine the parent element of the image
                StructureElement parent = imageFigure.ParentElement as StructureElement;
                if (parent != null)
                {
                    // Locate the index of the image within its parent's children
                    int index = -1;
                    for (int i = 0; i < parent.ChildElements.Count; i++)
                    {
                        if (parent.ChildElements[i] == imageFigure)
                        {
                            index = i;
                            break;
                        }
                    }

                    if (index >= 0)
                    {
                        // Create a new TableElement
                        TableElement table = tagged.CreateTableElement();
                        table.AlternativeText = "Inserted table before image";

                        // Build a simple table structure (header + one data row)
                        TableTHeadElement thead = tagged.CreateTableTHeadElement();
                        table.AppendChild(thead);
                        TableTRElement headerRow = tagged.CreateTableTRElement();
                        thead.AppendChild(headerRow);
                        TableTHElement th1 = tagged.CreateTableTHElement();
                        th1.SetText("Header 1");
                        headerRow.AppendChild(th1);
                        TableTHElement th2 = tagged.CreateTableTHElement();
                        th2.SetText("Header 2");
                        headerRow.AppendChild(th2);

                        TableTBodyElement tbody = tagged.CreateTableTBodyElement();
                        table.AppendChild(tbody);
                        TableTRElement dataRow = tagged.CreateTableTRElement();
                        tbody.AppendChild(dataRow);
                        TableTDElement td1 = tagged.CreateTableTDElement();
                        td1.SetText("Cell 1");
                        dataRow.AppendChild(td1);
                        TableTDElement td2 = tagged.CreateTableTDElement();
                        td2.SetText("Cell 2");
                        dataRow.AppendChild(td2);

                        // Insert the table before the image element
                        parent.InsertChild(table, index, true);
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}