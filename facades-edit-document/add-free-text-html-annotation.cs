using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using System.Drawing;

public class Program
{
    public static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            Page page = doc.Pages[1];

            // Use fully‑qualified Aspose.Pdf.Rectangle to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // DefaultAppearance constructor expects System.Drawing.Color, not Aspose.Pdf.Color
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                RichText = "<b>Bold text</b> <i>Italic text</i> <u>Underlined</u>",
                Color = Aspose.Pdf.Color.LightGray,
                Opacity = 0.8f
            };

            page.Annotations.Add(freeText);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation added and saved to '{outputPath}'.");
    }
}