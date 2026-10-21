using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the stamp will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle that matches the underlying image position.
            // Fully qualified type name avoids ambiguity with System.Drawing.Rectangle.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a TextStamp – we only need the annotation box, so the text is empty.
            TextStamp stamp = new TextStamp(" ");
            stamp.Width = rect.Width;          // set stamp width
            stamp.Height = rect.Height;        // set stamp height
            stamp.XIndent = rect.LLX;          // horizontal position (left)
            stamp.YIndent = rect.LLY;          // vertical position (bottom)
            stamp.RotateAngle = 90f;           // rotate 90° clockwise around the stamp centre
            stamp.Opacity = 0;                 // make the visual part invisible (only the annotation remains)

            // Add the stamp to the page – this creates a StampAnnotation internally.
            page.AddStamp(stamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamp annotation rotated and saved to '{outputPath}'.");
    }
}
