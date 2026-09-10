using System;
using System.IO;
using System.Drawing; // needed for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations; // for DefaultAppearance

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string customFontPath = "custom.ttf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(customFontPath))
        {
            Console.Error.WriteLine($"Font file not found: {customFontPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Load the external TrueType font (embedding is handled automatically when the font name is used)
            Aspose.Pdf.Text.Font customFont = FontRepository.OpenFont(customFontPath);

            // Extract the font name to be used in DefaultAppearance (DefaultAppearance expects a string font name)
            string fontName = Path.GetFileNameWithoutExtension(customFontPath);

            // Define the rectangle where the form field will be placed (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a text box form field on page 1
            TextBoxField txtField = new TextBoxField(doc.Pages[1], fieldRect);
            txtField.PartialName = "CustomFontField";

            // Set the field's default appearance using the custom font name and a System.Drawing.Color
            DefaultAppearance appearance = new DefaultAppearance(fontName, 12, System.Drawing.Color.Black);
            txtField.DefaultAppearance = appearance;

            // Add the field to the document's form (page number is 1‑based)
            doc.Form.Add(txtField, 1);

            // Add the visual appearance of the field to the specified page and rectangle
            doc.Form.AddFieldAppearance(txtField, 1, fieldRect);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with custom font field: {outputPdfPath}");
    }
}
