using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath   = "filled.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(templatePath))
        {
            // ----- Fill text field "Name" -----
            var nameField = doc.Form?.Fields?.FirstOrDefault(f => f.FullName == "Name") as TextBoxField;
            if (nameField == null)
            {
                Console.Error.WriteLine("Field 'Name' not found.");
            }
            else
            {
                nameField.Value = "John Doe";
            }

            // ----- Fill text field "Date" -----
            var dateField = doc.Form?.Fields?.FirstOrDefault(f => f.FullName == "Date") as TextBoxField;
            if (dateField == null)
            {
                Console.Error.WriteLine("Field 'Date' not found.");
            }
            else
            {
                dateField.Value = DateTime.Today.ToShortDateString();
            }

            // Add additional field assignments here as needed, e.g.:
            // var checkBox = doc.Form?.Fields?.FirstOrDefault(f => f.FullName == "Agree") as CheckBoxField;
            // if (checkBox != null) checkBox.Checked = true;

            // Save the filled PDF
            doc.Save(outputPath);
        } // Document disposed here, releasing unmanaged resources
    }
}
