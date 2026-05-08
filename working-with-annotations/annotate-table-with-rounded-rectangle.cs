using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Extract tables from the first page (adjust page index as needed)
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc.Pages[1]);

            // If a table is found, draw a rectangle (square annotation) around it
            if (absorber.TableList.Count > 0)
            {
                // Get the bounding rectangle of the first detected table
                Aspose.Pdf.Rectangle tableRect = absorber.TableList[0].Rectangle;

                // Optionally enlarge the rectangle a little to provide padding
                const double padding = 5.0;
                Aspose.Pdf.Rectangle annotationRect = new Aspose.Pdf.Rectangle(
                    tableRect.LLX - padding,
                    tableRect.LLY - padding,
                    tableRect.URX + padding,
                    tableRect.URY + padding);

                // Create the square (figure) annotation
                SquareAnnotation square = new SquareAnnotation(doc.Pages[1], annotationRect);
                // Set visual properties
                square.Color = Aspose.Pdf.Color.Blue;                     // border color
                square.InteriorColor = Aspose.Pdf.Color.Transparent;      // fill (transparent)
                // NOTE: Rounded corners are not supported in the current Aspose.Pdf version.
                // If a newer version provides the RoundedCornersRadius property, you can enable it like:
                // square.RoundedCornersRadius = 5;
                // Assign a border – must be done after the annotation instance exists
                square.Border = new Border(square) { Width = 2 };

                // Add the annotation to the page's annotation collection
                doc.Pages[1].Annotations.Add(square);
            }
            else
            {
                Console.WriteLine("No tables detected on the first page.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}
