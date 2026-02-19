using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDoc = new Document(inputPath);

            // Obtain the tagged‑content interface (used to build logical structure)
            ITaggedContent tagged = pdfDoc.TaggedContent;
            StructureElement root = tagged.RootElement;

            int pageNumber = 1;
            foreach (Page page in pdfDoc.Pages)
            {
                // Use TableAbsorber to find tables on the current page
                TableAbsorber absorber = new TableAbsorber();
                absorber.Visit(page);

                // Process each detected table
                foreach (AbsorbedTable absorbedTable in absorber.TableList)
                {
                    // Create a logical Table element and give it a title
                    TableElement tableElement = tagged.CreateTableElement();
                    tableElement.Title = $"Table on page {pageNumber}";

                    // Create a table body (TBody) element and attach it to the table
                    TableTBodyElement tBody = tagged.CreateTableTBodyElement();
                    tableElement.AppendChild(tBody, true);

                    // Iterate over rows and cells of the absorbed table
                    foreach (AbsorbedRow row in absorbedTable.RowList)
                    {
                        // Create a table row (TR) inside the body
                        TableTRElement tr = tBody.CreateTR();

                        foreach (AbsorbedCell cell in row.CellList)
                        {
                            // Create a table cell (TD) inside the row
                            TableTDElement td = tr.CreateTD();

                            // Concatenate all text fragments inside the cell
                            string cellText = string.Empty;
                            foreach (TextFragment fragment in cell.TextFragments)
                            {
                                cellText += fragment.Text;
                            }

                            // Create a paragraph element to hold the cell's text
                            ParagraphElement para = tagged.CreateParagraphElement();
                            para.ActualText = cellText; // logical text for accessibility
                            para.Title = cellText;      // optional visual title

                            // Attach the paragraph to the cell
                            td.AppendChild(para, true);
                        }
                    }

                    // Attach the completed table element to the document root
                    root.AppendChild(tableElement, true);
                }

                // No need to clear absorber.TableList – it is a read‑only collection.
                pageNumber++;
            }

            // Prepare the tagged content for saving
            tagged.PreSave();

            // Save the modified PDF
            pdfDoc.Save(outputPath);
            Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
