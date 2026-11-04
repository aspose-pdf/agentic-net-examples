using System;
using System.IO;
using System.Linq;
using System.Drawing; // for System.Drawing.Color (will be fully qualified where needed)
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // <-- added for DefaultAppearance
using Aspose.Pdf.Text; // <-- added for FontRepository (if needed in future)

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "myTextField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the form contains the specified field using the Fields collection
            bool fieldExists = doc.Form?.Fields?.Any(f => f.Name == fieldName) ?? false;
            if (!fieldExists)
            {
                Console.Error.WriteLine($"Form field '{fieldName}' not found.");
                return;
            }

            // Retrieve the field via the Form indexer and cast to the appropriate type.
            // The indexer returns a WidgetAnnotation, so an explicit cast is required.
            if (doc.Form[fieldName] is TextBoxField textBox)
            {
                // Set the default appearance (font, size, colour) for the text box field.
                // DefaultAppearance lives in Aspose.Pdf.Annotations and its constructor expects
                // a font name, size and a System.Drawing.Color.
                textBox.DefaultAppearance = new DefaultAppearance(
                    "Helvetica",
                    12,
                    System.Drawing.Color.Blue);
            }
            else
            {
                Console.Error.WriteLine($"Field '{fieldName}' is not a text box and does not support DefaultAppearance.");
                return;
            }

            // Save the modified PDF (PDF format, no special SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Default appearance set for field '{fieldName}' and saved to '{outputPath}'.");
    }
}
