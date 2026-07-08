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

        // Open the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the free‑text annotation will appear
            // Fully qualified type to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create a DefaultAppearance instance.
            // The constructor expects a System.Drawing.Color for the text color.
            DefaultAppearance appearance = new DefaultAppearance(
                "Helvetica",               // font name
                12,                        // font size
                System.Drawing.Color.Black // text color
            );

            // Create the free‑text annotation on the specified page
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance);

            // Multiline content – use line‑feed characters to separate lines.
            // Aspose.Pdf renders each line separately.
            freeText.Contents = "First line\nSecond line\nThird line";

            // Aspose.Pdf does not expose a direct line‑spacing property on FreeTextAnnotation.
            // To approximate a line spacing of 1.5× the font size, we insert empty lines
            // with the required additional spacing using the AppendLine overload that
            // accepts a line‑spacing value. This is done via the annotation's
            // DefaultAppearanceObject.TextState if needed, but here we rely on the
            // newline characters; for precise control a TextParagraph could be used
            // instead of a FreeTextAnnotation.

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation added and saved to '{outputPath}'.");
    }
}