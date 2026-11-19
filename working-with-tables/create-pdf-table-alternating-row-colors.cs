using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_alternating.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Verify that the PDF is tagged by checking TaggedContent (IsTagged does not exist)
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("Document is not tagged. Table structure cannot be accessed.");
                return;
            }

            ITaggedContent tagged = doc.TaggedContent;

            // Find the first table element in the structure tree
            TableElement table = tagged.RootElement
                                        .FindElements<TableElement>(true)
                                        .FirstOrDefault();

            if (table == null)
            {
                Console.WriteLine("No table element found in the PDF.");
                return;
            }

            // Iterate over each row (TableTRElement) and apply alternating background colors
            int rowIndex = 0;
            foreach (Element rowElem in table.ChildElements)
            {
                if (rowElem is TableTRElement row)
                {
                    // Choose background color based on row parity
                    Aspose.Pdf.Color bgColor = (rowIndex % 2 == 0)
                        ? Aspose.Pdf.Color.White
                        : Aspose.Pdf.Color.LightGray;

                    // Apply the background color to each cell in the row
                    foreach (Element cellElem in row.ChildElements)
                    {
                        switch (cellElem)
                        {
                            case TableTHElement th:
                                th.BackgroundColor = bgColor;
                                break;
                            case TableTDElement td:
                                td.BackgroundColor = bgColor;
                                break;
                        }
                    }

                    rowIndex++;
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Alternating row colors applied. Saved to '{outputPath}'.");
    }
}
