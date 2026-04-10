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
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fontName = "Arial";
        const double fontSize = 12;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Find the custom font and ensure it is embedded
            Aspose.Pdf.Text.Font customFont = FontRepository.FindFont(fontName);
            customFont.IsEmbedded = true;

            // Create a DefaultAppearance using System.Drawing.Color (required by the constructor)
            DefaultAppearance appearance = new DefaultAppearance(customFont, fontSize, System.Drawing.Color.Black);

            // Apply the appearance to every form field
            foreach (WidgetAnnotation field in doc.Form)
            {
                field.DefaultAppearance = appearance;
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Custom font applied to all form fields and saved as '{outputPath}'.");
    }
}