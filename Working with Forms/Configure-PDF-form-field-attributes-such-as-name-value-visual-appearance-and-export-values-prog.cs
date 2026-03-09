using System;
using System.IO;
using System.Drawing; // Needed for DefaultAppearance color
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Annotations; // For Border class

class Program
{
    static void Main()
    {
        const string inputPath = "template.pdf";
        const string outputPath = "filled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the field will appear (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a text box field on the specified rectangle
            TextBoxField txtField = new TextBoxField(doc, fieldRect);

            // ----- Visual appearance -----
            txtField.Color = Aspose.Pdf.Color.LightGray; // background color
            // Border must be created after the field instance exists because it requires the parent annotation
            txtField.Border = new Border(txtField) { Width = 1 };
            // DefaultAppearance expects System.Drawing.Color, not Aspose.Pdf.Color
            txtField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.DarkBlue);
            txtField.ReadOnly = false;
            txtField.Required = true;

            // ----- Field attributes -----
            txtField.PartialName = "CustomerName"; // logical name used in form data
            txtField.Value = "John Doe";          // initial value displayed in the field
            txtField.Exportable = true;           // include this field when exporting form data
            txtField.MappingName = "CustName";    // export name (alternative to PartialName)

            // Add the field to the form on page 1 (Aspose.Pdf uses 1‑based page indexing)
            doc.Form.Add(txtField, txtField.PartialName, 1);

            // Optional: add an additional appearance on another page
            // doc.Form.AddFieldAppearance(txtField, 2, fieldRect);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form field configured and saved to '{outputPath}'.");
    }
}
