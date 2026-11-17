using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (required to locate the table structure element)
            ITaggedContent taggedContent = doc.TaggedContent;

            // Find the first TableElement in the structure tree
            var tables = taggedContent.RootElement.FindElements<TableElement>(true);
            if (tables.Count == 0)
            {
                Console.WriteLine("No table element found in the document.");
                doc.Save(outputPath); // Save unchanged document
                return;
            }

            TableElement table = tables[0];

            // Determine the page that contains the table.
            // For simplicity we assume the table is on page 1.
            // In a real scenario you could retrieve the page number from the element if available.
            Page page = doc.Pages[1];

            // Determine the rectangle that should surround the table.
            // If the TableElement provides a bounding rectangle you can use it.
            // Here we use placeholder coordinates; replace with actual values as needed.
            double llx = 50;   // lower‑left X
            double lly = 500;  // lower‑left Y
            double urx = 550;  // upper‑right X
            double ury = 750;  // upper‑right Y

            // Create a square annotation. Aspose.Pdf does not expose a RoundedCorners property;
            // rounded corners would require a custom stamp image. We therefore create a normal
            // square annotation and set its border.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
            SquareAnnotation square = new SquareAnnotation(page, rect)
            {
                Title    = "Table Border",
                Contents = "Rectangle around the table",
                // The annotation's Color property defines the border colour.
                Color    = Aspose.Pdf.Color.Blue
            };

            // Border must be created with the annotation as its parent and cannot be set in the
            // object initializer (see Fix: border-requires-parent-annotation).
            square.Border = new Border(square) { Width = 2 };

            // Add the annotation to the page
            page.Annotations.Add(square);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}
