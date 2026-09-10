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
            // Extract tables from the first page (adjust as needed)
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc.Pages[1]);

            // If a table is found, create a rounded‑corner rectangle annotation around it
            if (absorber.TableList.Count > 0)
            {
                // Get the bounding rectangle of the first detected table
                Aspose.Pdf.Rectangle tableRect = absorber.TableList[0].Rectangle;

                // Slightly enlarge the rectangle to give a margin around the table
                const double margin = 5.0;
                Aspose.Pdf.Rectangle annotationRect = new Aspose.Pdf.Rectangle(
                    tableRect.LLX - margin,
                    tableRect.LLY - margin,
                    tableRect.URX + margin,
                    tableRect.URY + margin);

                // Create the square (figure) annotation
                SquareAnnotation square = new SquareAnnotation(doc.Pages[1], annotationRect);
                // Set visual properties
                square.Color = Aspose.Pdf.Color.Blue;                     // border color
                square.Border = new Border(square) { Width = 2 };          // border width
                square.InteriorColor = Aspose.Pdf.Color.FromRgb(0.9, 0.9, 0.9); // fill color (light gray)
                square.Title = "Table Outline";
                square.Contents = "Rounded‑corner rectangle around the table";

                // Note: Aspose.Pdf does not provide a direct property for rounded corners.
                // The SquareAnnotation will appear as a regular rectangle.
                // If rounded corners are required, a custom appearance stream must be created,
                // which is beyond the scope of this simple example.

                // Add the annotation to the page
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
