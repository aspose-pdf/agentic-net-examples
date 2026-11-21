using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Add a fresh page at the end of the document
            Page newPage = doc.Pages.Add();

            // Page numbers are 1‑based in Aspose.Pdf
            int newPageNumber = doc.Pages.Count;

            // ----- TextBox field -----
            var txtRect = new Rectangle(100, 600, 300, 620);
            TextBoxField txtField = new TextBoxField(newPage, txtRect);
            txtField.PartialName = "NewTextBox";
            txtField.Value = "Sample text";
            doc.Form.Add(txtField, newPageNumber);

            // ----- Checkbox field -----
            var chkRect = new Rectangle(100, 560, 120, 580);
            CheckboxField chkField = new CheckboxField(newPage, chkRect);
            chkField.PartialName = "NewCheckBox";
            chkField.Checked = true;
            doc.Form.Add(chkField, newPageNumber);

            // ----- Radio button group -----
            RadioButtonField radioField = new RadioButtonField(newPage);
            radioField.PartialName = "NewRadioGroup";
            // Option 1
            var opt1Rect = new Rectangle(100, 520, 120, 540);
            radioField.AddOption("Option1", opt1Rect);
            // Option 2
            var opt2Rect = new Rectangle(100, 500, 120, 520);
            radioField.AddOption("Option2", opt2Rect);
            // Select the first option by default (1‑based index)
            radioField.Selected = 1;
            doc.Form.Add(radioField, newPageNumber);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with new page and AcroForm fields saved to '{outputPath}'.");
    }
}
