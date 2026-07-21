using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // source PDF
        const string outputPdfPath = "output.pdf";         // result PDF
        const string customFontPath = "customfont.ttf";    // external TTF font file

        // Ensure the input files exist
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
            // Load the custom font from the external file
            Font customFont;
            using (FileStream fontStream = File.OpenRead(customFontPath))
            {
                // OpenFont reads the font data; specify TTF type
                customFont = FontRepository.OpenFont(fontStream, FontTypes.TTF);
                // Ensure the font will be embedded in the PDF
                customFont.IsEmbedded = true;
            }

            // Define the rectangle where the form field will be placed (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a textbox form field on the first page
            TextBoxField textField = new TextBoxField(doc.Pages[1], fieldRect)
            {
                PartialName = "CustomFontField"
            };

            // Set the field's default appearance to use the custom font
            // Use the constructor that accepts a Font, size, and color (Aspose.Pdf.Color)
            textField.DefaultAppearance = new DefaultAppearance(customFont, 12, System.Drawing.Color.Black);

            // Add the field to the document's form collection
            doc.Form.Add(textField);

            // Optionally add an additional appearance (same rectangle on page 1)
            doc.Form.AddFieldAppearance(textField, 1, fieldRect);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with custom font field: {outputPdfPath}");
    }
}