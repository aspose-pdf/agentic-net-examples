using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "Modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Coordinates for the new text field (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
        const double llx = 100; // left
        const double lly = 500; // bottom
        const double urx = 300; // right
        const double ury = 550; // top

        // Page number where the field will be placed (1‑based indexing)
        const int pageNumber = 1;

        // Field name (must be unique within the document)
        const string fieldName = "NewTextField";

        // Load the existing PDF.
        Document doc = new Document(inputPath);
        Page page = doc.Pages[pageNumber];

        // Create the rectangle for the field – fully qualified to avoid ambiguity.
        Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

        // Construct the text box field.
        TextBoxField textBox = new TextBoxField(page, fieldRect)
        {
            PartialName = fieldName,
            Value = "Enter text here"
        };

        // ---- Decoration (colors, border, font) ----
        // Background and border colours use System.Drawing.Color.
        textBox.Characteristics.Background = System.Drawing.Color.LightGray;
        textBox.Characteristics.Border = System.Drawing.Color.Black;

        // Border width and style.
        textBox.Border = new Border(textBox)
        {
            Width = 1,
            Style = BorderStyle.Solid
        };

        // Font, size and text colour – DefaultAppearance expects System.Drawing.Color.
        textBox.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

        // Add the field to the document's form collection on the desired page.
        doc.Form.Add(textBox, pageNumber);

        // Save the modified PDF.
        doc.Save(outputPath);

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
