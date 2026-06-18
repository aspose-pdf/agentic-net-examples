using System;
using System.IO;
using System.Drawing; // For System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Text; // For Aspose.Pdf.Text.Font
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string fontPath = "customfont.ttf"; // path to the custom TrueType font

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(fontPath))
        {
            Console.Error.WriteLine($"Font file not found: {fontPath}");
            return;
        }

        // Load the custom font from a stream and ensure it will be embedded in the PDF
        Aspose.Pdf.Text.Font customFont;
        using (FileStream fontStream = File.OpenRead(fontPath))
        {
            customFont = FontRepository.OpenFont(fontStream, FontTypes.TTF);
            customFont.IsEmbedded = true;
        }

        // Open the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all form fields in the document
            foreach (Field field in doc.Form.Fields)
            {
                // Set the default appearance for the field: font name, size, and color
                field.DefaultAppearance = new DefaultAppearance(
                    customFont.FontName, // font name from the loaded font
                    12,                  // desired font size
                    System.Drawing.Color.Black // use System.Drawing.Color
                );
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with custom font applied to all form fields: {outputPdf}");
    }
}
