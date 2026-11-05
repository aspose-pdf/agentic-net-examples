using System;
using System.IO;
using System.Drawing; // System.Drawing.Color for field characteristics
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroForm.pdf";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // Create a TextBox (AcroForm field) at absolute coordinates
            // -------------------------------------------------
            // Rectangle coordinates: lower‑left X, lower‑left Y, upper‑right X, upper‑right Y
            // Example: place the field at (100, 600) with width 200 and height 20
            Aspose.Pdf.Rectangle txtRect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);
            TextBoxField txtField = new TextBoxField(page, txtRect);
            txtField.PartialName = "CustomerName";
            txtField.Value = "John Doe";
            // Appearance settings (background, border colour, border width/style)
            txtField.Characteristics.Background = System.Drawing.Color.LightGray;
            txtField.Characteristics.Border = System.Drawing.Color.Black;
            txtField.Border = new Border(txtField) { Width = 1, Style = BorderStyle.Solid };
            // Text appearance (font, size, colour) – DefaultAppearance expects System.Drawing.Color
            txtField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            // Optional tooltip
            txtField.AlternateName = "Enter customer name";
            doc.Form.Add(txtField, 1);

            // -------------------------------------------------
            // Create a CheckBox field at absolute coordinates
            // -------------------------------------------------
            // Place the checkbox at (100, 560) with size 15x15
            Aspose.Pdf.Rectangle chkRect = new Aspose.Pdf.Rectangle(100, 560, 115, 575);
            CheckboxField chkField = new CheckboxField(page, chkRect);
            chkField.PartialName = "SubscribeNewsletter";
            chkField.Checked = true;
            chkField.Characteristics.Background = System.Drawing.Color.White;
            chkField.Characteristics.Border = System.Drawing.Color.Black;
            chkField.Border = new Border(chkField) { Width = 1, Style = BorderStyle.Solid };
            doc.Form.Add(chkField, 1);

            // -------------------------------------------------
            // Create a RadioButton group with two options (Male / Female)
            // -------------------------------------------------
            RadioButtonField genderField = new RadioButtonField(page);
            genderField.PartialName = "Gender";
            // Add options with their own rectangles
            genderField.AddOption("Male", new Aspose.Pdf.Rectangle(100, 520, 115, 535));
            genderField.AddOption("Female", new Aspose.Pdf.Rectangle(150, 520, 165, 535));
            // Visual appearance for both options
            genderField.Characteristics.Background = System.Drawing.Color.White;
            genderField.Characteristics.Border = System.Drawing.Color.Black;
            genderField.Border = new Border(genderField) { Width = 1, Style = BorderStyle.Solid };
            // Select the first option (Male) by default (1‑based index)
            genderField.Selected = 1;
            doc.Form.Add(genderField, 1);

            // -------------------------------------------------
            // Save the PDF. Document.Save(string) always writes PDF,
            // no SaveOptions needed for PDF output.
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm fields saved to '{outputPath}'.");
    }
}
