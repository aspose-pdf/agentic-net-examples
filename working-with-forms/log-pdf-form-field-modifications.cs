using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // needed for Border class

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string logFile = "field_modifications.log";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Open the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPdf))
        {
            // Open the audit log for appending
            using (StreamWriter log = new StreamWriter(logFile, true))
            {
                // -------------------------------------------------
                // Add a new text box field to the document
                // -------------------------------------------------
                TextBoxField txtField = new TextBoxField(doc);
                txtField.PartialName = "SampleTextBox";
                txtField.Rect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);
                txtField.Value = "Initial value";
                txtField.Color = Aspose.Pdf.Color.LightGray;

                // Add the field to page 1 (Form.Add overload)
                doc.Form.Add(txtField, 1);
                log.WriteLine($"{DateTime.Now:u} Added TextBoxField '{txtField.PartialName}' on page 1 at {txtField.Rect}");

                // -------------------------------------------------
                // Update the field's value
                // -------------------------------------------------
                txtField.Value = "Updated value";
                log.WriteLine($"{DateTime.Now:u} Updated TextBoxField '{txtField.PartialName}' value to '{txtField.Value}'");

                // -------------------------------------------------
                // Change visual appearance (border)
                // -------------------------------------------------
                // Border requires the parent annotation (the field) in its constructor.
                txtField.Border = new Border(txtField)
                {
                    Width = 1
                };
                // Border color is controlled by the field's own Color property; already set to LightGray.
                log.WriteLine($"{DateTime.Now:u} Set border for TextBoxField '{txtField.PartialName}' (Width=1)");

                // -------------------------------------------------
                // Save the modified PDF (lifecycle rule: use Save)
                // -------------------------------------------------
                doc.Save(outputPdf);
                log.WriteLine($"{DateTime.Now:u} Saved modified PDF to '{outputPdf}'");
            }
        }

        Console.WriteLine("Form field modifications have been logged.");
    }
}
