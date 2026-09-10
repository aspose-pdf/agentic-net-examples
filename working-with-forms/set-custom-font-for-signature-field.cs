using System;
using System.IO;
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
        const string fontPath = "fonts/Custom.ttf";
        const string signatureFieldName = "Signature";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(fontPath))
        {
            Console.Error.WriteLine($"Font file not found: {fontPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Register the custom TrueType font so Aspose.Pdf can resolve it by name
            FontRepository.Sources.Add(new FileFontSource(fontPath));

            // Open the font to obtain its internal name (FontName)
            Font customFont = FontRepository.OpenFont(fontPath);

            // Retrieve the signature field by its name
            if (doc.Form[signatureFieldName] is SignatureField signatureField)
            {
                // Set the default appearance using the DefaultAppearance object.
                // Parameters: font name, font size, and color (System.Drawing.Color required).
                signatureField.DefaultAppearance = new DefaultAppearance(customFont.FontName, 12, System.Drawing.Color.Black);
            }
            else
            {
                Console.Error.WriteLine($"Signature field '{signatureFieldName}' not found.");
                return;
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with custom font applied to signature field: {outputPdfPath}");
    }
}
