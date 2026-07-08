using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            Page page = doc.Pages[1];

            // Define the rectangle where the text field will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);

            // Create a TextBoxField on the specified page and rectangle
            TextBoxField textField = new TextBoxField(page, rect);

            // Set the default value that will appear in the field
            textField.Value = "Default Text";

            // Make the field read‑only so the user cannot edit it
            textField.ReadOnly = true;

            // Define the default appearance (font name, size, and color)
            // Note: DefaultAppearance constructor expects System.Drawing.Color for the color parameter
            textField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Add the field to the page's annotation collection
            page.Annotations.Add(textField);

            // Save the modified PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}