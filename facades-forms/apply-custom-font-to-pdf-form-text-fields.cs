using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Forms;          // <-- required for TextBoxField
using Aspose.Pdf.Annotations;   // <-- required for DefaultAppearance

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
        Document pdfDoc = new Document(inputPath);

        // Iterate over all form fields in the document
        foreach (var field in pdfDoc.Form)
        {
            // Process only text box fields (TextBoxField)
            if (field is TextBoxField textBox)
            {
                // Set the default appearance: Arial Bold, size 12, black color
                // DefaultAppearance expects System.Drawing.Color, so fully qualify to avoid ambiguity with Aspose.Pdf.Color
                textBox.DefaultAppearance = new DefaultAppearance("Arial,Bold", 12, System.Drawing.Color.Black);
            }
        }

        // Save the updated PDF
        pdfDoc.Save(outputPath);

        Console.WriteLine($"All text fields updated with Arial Bold and saved to '{outputPath}'.");
    }
}
