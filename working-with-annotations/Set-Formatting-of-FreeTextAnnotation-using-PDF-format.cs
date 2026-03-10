using System;
using System.IO;
using System.Drawing; // Required for DefaultAppearance color and text color
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

// Alias the two Color types to avoid ambiguity
using AsposeColor = Aspose.Pdf.Color;
using DrawingColor = System.Drawing.Color;

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the annotation rectangle (fully qualified)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create DefaultAppearance (constructor requires System.Drawing.Color)
            DefaultAppearance da = new DefaultAppearance("Helvetica", 12, DrawingColor.Blue);

            // Create a FreeTextAnnotation on the first page
            FreeTextAnnotation ft = new FreeTextAnnotation(doc.Pages[1], rect, da)
            {
                Contents = "Formatted FreeText",
                // Annotation border color uses Aspose.Pdf.Color
                Color = AsposeColor.Yellow
            };

            // Apply additional text formatting (bold, italic, font size, color)
            // SetTextStyle expects System.Drawing.Color for the text color
            ft.SetTextStyle(RichTextFontStyles.Bold | RichTextFontStyles.Italic,
                            "Helvetica", 14, DrawingColor.Red);

            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(ft);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"FreeTextAnnotation with formatting saved to '{outputPath}'.");
    }
}
