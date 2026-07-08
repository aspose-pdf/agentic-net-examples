using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Extract tables from the document
            TableAbsorber tableAbsorber = new TableAbsorber();
            // The TableAbsorber implements IVisitor in recent Aspose.Pdf versions.
            // In some older builds the Accept overload expects an AnnotationSelector, which causes a compile error.
            // Using the Visit method works across versions and avoids the overload conflict.
            tableAbsorber.Visit(doc);

            // Ensure at least one table was found
            if (tableAbsorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables detected in the document.");
                doc.Save(outputPath); // Save unchanged document
                return;
            }

            // Take the first detected table (adjust as needed)
            ITableElement table = tableAbsorber.TableList[0];

            // The rectangle describing the table position
            Aspose.Pdf.Rectangle tableRect = table.Rectangle;

            // Optionally expand the rectangle a little to give visual padding
            const double padding = 5.0;
            tableRect.LLX -= padding;
            tableRect.LLY -= padding;
            tableRect.URX += padding;
            tableRect.URY += padding;

            // Determine the page that contains the table.
            // TableAbsorber places tables in the order they appear; we assume the first page here.
            // If tables span multiple pages, additional logic would be required.
            Page page = doc.Pages[1];

            // Create a square annotation (rectangle) around the table.
            // Rounded corners are not directly supported; a square annotation draws a rectangle.
            SquareAnnotation rectAnnot = new SquareAnnotation(page, tableRect)
            {
                Color = Aspose.Pdf.Color.Blue,                 // Border color
                InteriorColor = Aspose.Pdf.Color.Transparent // No fill
            };

            // Set border width via the Border object (requires the parent annotation in ctor)
            rectAnnot.Border = new Border(rectAnnot) { Width = 2 };

            // Add the annotation to the page
            page.Annotations.Add(rectAnnot);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}
