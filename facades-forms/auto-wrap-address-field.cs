using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "Address";
        const string longText = "123 Main Street, Apartment 4B, Some City, Some State, 12345-6789";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the document first – FormEditor’s non‑obsolete constructor expects a Document instance.
        using (Document doc = new Document(inputPath))
        {
            // Convert the single‑line field to a multiline field so it can wrap text.
            using (FormEditor formEditor = new FormEditor(doc))
            {
                bool converted = formEditor.Single2Multiple(fieldName);
                if (!converted)
                {
                    Console.Error.WriteLine($"Failed to convert field '{fieldName}' to multiline.");
                    return;
                }
            }

            // Enable automatic font size reduction to fit the rectangle.
            Field.FitIntoRectangle = true;

            // Set the field value and adjust its height.
            if (doc.Form[fieldName] is TextBoxField txtField)
            {
                txtField.Value = longText;

                // Rough estimation: assume ~30 characters per line for the current font size.
                int approxLines = (int)Math.Ceiling((double)longText.Length / 30);
                const double lineHeight = 12.0; // points per line – adjust if you change the font size.
                double newHeight = approxLines * lineHeight + 4; // extra padding

                // Preserve the original left‑bottom coordinates and width.
                double llx = txtField.Rect.LLX;
                double lly = txtField.Rect.LLY;
                double urx = txtField.Rect.URX;
                double ury = lly + newHeight;

                txtField.Rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
            }
            else
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a TextBoxField.");
                return;
            }

            // Save the updated document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' updated and saved to '{outputPath}'.");
    }
}
