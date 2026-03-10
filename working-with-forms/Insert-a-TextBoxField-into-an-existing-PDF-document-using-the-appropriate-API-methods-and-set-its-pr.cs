using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // required for Border and BorderStyle

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

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the TextBox will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a TextBoxField on the specified page and rectangle
            TextBoxField txtField = new TextBoxField(page, rect)
            {
                Name = "SampleTextBox",
                PartialName = "SampleTextBox",
                Value = "Default text",
                Multiline = true,                     // allow multiple lines
                ReadOnly = false,                    // editable
                Color = Aspose.Pdf.Color.LightGray // background color
            };

            // Configure the border – Border requires the parent annotation (txtField)
            txtField.Border = new Border(txtField)
            {
                Width = 1,
                Style = BorderStyle.Solid
            };

            // Add the field to the document's form on page 1
            doc.Form.Add(txtField, 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"TextBoxField added and saved to '{outputPath}'.");
    }
}
