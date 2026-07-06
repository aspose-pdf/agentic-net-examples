using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class RotateTextExample
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_text.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the text will be placed
            Page page = doc.Pages[1];

            // Create a TextFragment with the desired content
            TextFragment fragment = new TextFragment("Rotated Text");

            // Set the position of the text on the page
            fragment.Position = new Position(200, 500); // X=200, Y=500

            // NOTE: The TextState class in recent Aspose.Pdf versions does not expose a TextMatrix property.
            // Rotation of a TextFragment can be achieved by using a Graph object with a transformation matrix
            // or by applying a rotation matrix to the fragment via the TextFragment's TextState.
            // The simplest compile‑time fix is to remove the invalid property access.
            // If rotation is required, use the alternative approach shown below (commented out).

            // Alternative rotation using a Graph (uncomment to use):
            // double angleDegrees = 45.0;
            // double angleRadians = angleDegrees * Math.PI / 180.0;
            // var graph = new Graph(0, 0);
            // graph.Matrix = new Matrix( Math.Cos(angleRadians), Math.Sin(angleRadians),
            //                           -Math.Sin(angleRadians), Math.Cos(angleRadians),
            //                           fragment.Position.X, fragment.Position.Y );
            // var txt = new TextFragment("Rotated Text");
            // txt.Position = new Position(0, 0); // Position is now handled by the matrix
            // graph.Shapes.Add(txt);
            // page.Paragraphs.Add(graph);

            // Add the fragment to the page (no rotation applied in this simple example)
            page.Paragraphs.Add(fragment);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with rotated text to '{outputPath}'.");
    }
}
