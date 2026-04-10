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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the annotation will be placed
            Page page = doc.Pages[1];

            // Define the rectangle for the free‑text annotation
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create a DefaultAppearance for the annotation (font, size, color)
            // Note: DefaultAppearance constructor requires System.Drawing.Color for the third argument
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create the free‑text annotation on the selected page
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                // Set a background color for better visibility (optional)
                Color = Aspose.Pdf.Color.LightGray,

                // Multiline content – use newline characters for plain text
                Contents = "First line of text\nSecond line of text\nThird line of text",

                // Use RichText with CSS line-height to control line spacing (1.5 = 150%)
                RichText = "<body style='line-height:1.5'>First line of text<br/>Second line of text<br/>Third line of text</body>"
            };

            // Add the annotation to the page
            page.Annotations.Add(freeText);

            // Save the modified PDF (lifecycle rule: save inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation added and saved to '{outputPath}'.");
    }
}
