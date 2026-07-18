using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

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

        // Amount to move the graphic (in points)
        const double deltaX = 50; // shift right by 50 points
        const double deltaY = 30; // shift up by 30 points

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Work with the first page; adjust as needed
                Page page = doc.Pages[1];

                // Correct absorber for vector graphics
                GraphicsAbsorber absorber = new GraphicsAbsorber();
                absorber.Visit(page);

                if (absorber.Elements.Count == 0)
                {
                    Console.WriteLine("No vector graphics found on the page.");
                }
                else
                {
                    // Take the first extracted vector graphic
                    var graphic = absorber.Elements[0];

                    // Current position of the graphic
                    Point currentPos = graphic.Position;

                    // Compute new position
                    Point newPos = new Point(currentPos.X + deltaX, currentPos.Y + deltaY);

                    // Apply the new position (updates the internal matrix)
                    graphic.Position = newPos;
                }

                // Save the modified PDF
                doc.Save(outputPath);
                Console.WriteLine($"Vector graphic moved and saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
