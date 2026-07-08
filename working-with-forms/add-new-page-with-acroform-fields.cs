using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF (document-disposal-with-using rule)
        using (Document doc = new Document(inputPath))
        {
            // Add a new blank page at the end (Pagecollection.Add rule)
            Page newPage = doc.Pages.Add();

            // Define rectangle for a text box field (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle txtRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);
            // Create a text box field on the new page
            TextBoxField txtField = new TextBoxField(newPage, txtRect)
            {
                PartialName = "NewTextBox",
                Value = "Enter text here"
            };
            // Add the field to the form (Form.Add(Field) rule)
            doc.Form.Add(txtField);

            // Define rectangle for a checkbox field
            Aspose.Pdf.Rectangle chkRect = new Aspose.Pdf.Rectangle(100, 550, 120, 570);
            // Create a checkbox field on the new page
            CheckboxField chkField = new CheckboxField(newPage, chkRect)
            {
                PartialName = "NewCheckBox",
                // Set default state (checked)
                Value = "Yes"
            };
            // Add the checkbox field to the form
            doc.Form.Add(chkField);

            // Save the modified PDF (standard Save for PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}