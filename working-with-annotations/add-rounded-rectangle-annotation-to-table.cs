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
            // Assume the table is on the first page; adjust as needed
            Page page = doc.Pages[1];

            // Extract tables from the page
            TableAbsorber tableAbsorber = new TableAbsorber();
            tableAbsorber.Visit(page);

            if (tableAbsorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables found on the page.");
                doc.Save(outputPath);
                return;
            }

            // Take the first detected table
            ITableElement table = tableAbsorber.TableList[0];
            Aspose.Pdf.Rectangle tableRect = table.Rectangle;

            // Create a square (rectangle) annotation around the table
            SquareAnnotation square = new SquareAnnotation(page, tableRect)
            {
                Color = Aspose.Pdf.Color.Blue,          // Border color
                InteriorColor = Aspose.Pdf.Color.LightGray // Fill color (optional)
            };

            // If the API supports rounded corners, set the radius (example value 5)
            // Note: CornerRadius property exists in recent versions; remove if unavailable.
            // square.CornerRadius = 5;

            // Add the annotation to the page
            page.Annotations.Add(square);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Figure annotation added and saved to '{outputPath}'.");
    }
}