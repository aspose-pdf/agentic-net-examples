using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Add a new blank page at the end of the document
            Page newPage = doc.Pages.Add();

            // ----- Create a TextBox field -----
            Rectangle txtRect = new Rectangle(100, 500, 300, 530);
            TextBoxField txtField = new TextBoxField(newPage, txtRect);
            txtField.PartialName = "NewTextBox";
            txtField.Value       = "Enter text";
            doc.Form.Add(txtField);

            // ----- Create a CheckBox field -----
            Rectangle chkRect = new Rectangle(100, 450, 120, 470);
            CheckboxField chkField = new CheckboxField(newPage, chkRect);
            chkField.PartialName = "NewCheckBox";
            chkField.Checked     = false;
            doc.Form.Add(chkField);

            // Save the updated PDF (extension determines format; PDF is default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with new page and AcroForm fields: {outputPath}");
    }
}