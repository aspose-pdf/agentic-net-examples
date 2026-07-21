using System;
using System.Drawing; // needed for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // DefaultAppearance class
using Aspose.Pdf.Forms;        // TextBoxField and Form

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document with a single page (seed PDF created inline)
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the field rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create a text box form field
            TextBoxField field = new TextBoxField(page, rect)
            {
                PartialName = "MyField",
                Value = "Sample text"
            };

            // Set the default appearance (font, size, color)
            // DefaultAppearance(string fontName, double fontSize, System.Drawing.Color textColor)
            DefaultAppearance da = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Blue);
            field.DefaultAppearance = da;

            // Add the field to the document's form collection
            doc.Form.Add(field);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated field default appearance: {outputPath}");
    }
}
