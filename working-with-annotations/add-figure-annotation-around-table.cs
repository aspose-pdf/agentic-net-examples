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
        const string inputPath = "input.pdf";
        const string outputPath = "output_annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (optional, but useful for locating the table)
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with Table Annotation");

            // Find the first TableElement in the structure tree
            StructureElement root = tagged.RootElement;
            var tables = root.FindElements<TableElement>(true);

            // Determine rectangle coordinates around the table.
            // In a real scenario, compute these from the table's position.
            // Here we use fixed coordinates for demonstration.
            double llx = 100; // lower‑left X
            double lly = 500; // lower‑left Y
            double urx = 400; // upper‑right X
            double ury = 700; // upper‑right Y

            // Create a square annotation (rectangle) around the table.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
            SquareAnnotation square = new SquareAnnotation(doc.Pages[1], rect);
            square.Color = Aspose.Pdf.Color.LightGray;               // border color
            square.Border = new Border(square) { Width = 2 };        // border width
            // Rounded corners are not supported in the current API version; omitted.
            // Use the Contents property to provide alternative text / description.
            square.Contents = "Figure: Table with rounded rectangle border";
            // Optionally set a title for the annotation.
            square.Title = "Table Figure";

            // Add the annotation to the first page
            doc.Pages[1].Annotations.Add(square);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}
