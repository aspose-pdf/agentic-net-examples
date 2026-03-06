using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Path for the generated PDF
        const string outputPath = "form.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Define rectangles for the fields (left, bottom, width, height)
            // Use fully qualified type to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle textBoxRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);
            Aspose.Pdf.Rectangle checkBoxRect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            // Create a TextBoxField on the page
            TextBoxField txtField = new TextBoxField(page, textBoxRect)
            {
                Name = "SampleTextBox",
                Value = "Enter text here"
            };

            // Create a CheckBoxField on the page
            CheckboxField chkField = new CheckboxField(page, checkBoxRect)
            {
                Name = "SampleCheckBox",
                Checked = false // unchecked by default
            };

            // Add the fields to the document's form
            doc.Form.Add(txtField);
            doc.Form.Add(chkField);

            // Save the PDF document (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with form fields saved to '{outputPath}'.");
    }
}