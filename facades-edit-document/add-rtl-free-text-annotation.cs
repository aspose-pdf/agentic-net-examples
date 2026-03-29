using System;
using System.IO;
using System.Drawing; // needed for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

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

        using (Document document = new Document(inputPath))
        {
            // Define annotation rectangle (left, bottom, right, top) using fully qualified type
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create a DefaultAppearance object (font, size, color)
            // NOTE: DefaultAppearance expects System.Drawing.Color, not Aspose.Pdf.Color
            var defaultAppearance = new DefaultAppearance("Arial Unicode MS", 12, System.Drawing.Color.Black);

            // Create free‑text annotation on the first page
            FreeTextAnnotation freeText = new FreeTextAnnotation(document.Pages[1], rect, defaultAppearance);

            // Set the Arabic text – Unicode rendering with RTL is handled automatically for the font
            freeText.Contents = "مرحبا بالعالم"; // Arabic "Hello World"
            freeText.Title = "RTL Test";

            // Set background color of the annotation using fully qualified Aspose.Pdf.Color
            freeText.Color = Aspose.Pdf.Color.Yellow;

            // Add annotation to the page
            document.Pages[1].Annotations.Add(freeText);

            // Save the modified PDF
            document.Save(outputPath);
        }

        Console.WriteLine($"Annotation added and saved to '{outputPath}'.");
    }
}
