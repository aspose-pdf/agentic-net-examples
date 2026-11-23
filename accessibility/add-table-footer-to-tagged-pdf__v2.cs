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

        // Load the PDF document inside a using block for deterministic disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Access the tagged content interface
            Aspose.Pdf.Tagged.ITaggedContent taggedContent = doc.TaggedContent;

            // Optionally set language and title for accessibility
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element (no cast required)
            Aspose.Pdf.LogicalStructure.StructureElement root = taggedContent.RootElement;

            // Create a table element and attach it to the root
            Aspose.Pdf.LogicalStructure.TableElement table = taggedContent.CreateTableElement();
            table.AlternativeText = "Sample table with footer";
            root.AppendChild(table);

            // Create the table footer element and attach it to the table
            Aspose.Pdf.LogicalStructure.TableTFootElement tfoot = taggedContent.CreateTableTFootElement();
            table.AppendChild(tfoot);

            // Add a row to the footer
            Aspose.Pdf.LogicalStructure.TableTRElement footRow = taggedContent.CreateTableTRElement();
            tfoot.AppendChild(footRow);

            // Add a cell to the footer row and set its text
            Aspose.Pdf.LogicalStructure.TableTDElement footCell = taggedContent.CreateTableTDElement();
            footCell.SetText("Footer text");
            footRow.AppendChild(footCell);

            // NOTE: TableTFootElement does not expose a VerticalAlignment property.
            // Alignment of footer content can be controlled via cell styling if required.

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}