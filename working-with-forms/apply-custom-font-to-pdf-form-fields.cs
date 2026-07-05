using System;
using System.IO;
using System.Drawing;                     // Required for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fontName   = "Arial";   // replace with your custom font name
        const double fontSize   = 12;        // desired font size

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all form fields
            foreach (Field field in doc.Form)
            {
                // Set a new DefaultAppearance for each field.
                // DefaultAppearance constructor requires System.Drawing.Color for the text color.
                field.DefaultAppearance = new DefaultAppearance(fontName, fontSize, System.Drawing.Color.Black);
            }

            // Save the modified PDF (lifecycle rule: use Save without extra options for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Custom font applied to all form fields. Saved as '{outputPath}'.");
    }
}