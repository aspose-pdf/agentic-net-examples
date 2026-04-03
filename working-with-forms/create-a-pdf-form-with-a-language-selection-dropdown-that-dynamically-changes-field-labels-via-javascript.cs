using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "LanguageForm.pdf";

        // Create a new PDF document and add a single page
        using (Document doc = new Document())
        {
            doc.Pages.Add();

            // Initialize FormEditor with the document
            FormEditor formEditor = new FormEditor(doc);

            // Add a combo box for language selection
            // Field coordinates: llx, lly, urx, ury (points)
            formEditor.AddField(FieldType.ComboBox, "Language", 1, 100, 700, 250, 720);
            // Define the items that appear in the combo box
            formEditor.Items = new string[] { "English", "Spanish" };

            // Add two text fields that will serve as dynamic labels
            formEditor.AddField(FieldType.Text, "Label1", 1, 100, 650, 200, 670);
            formEditor.AddField(FieldType.Text, "Label2", 1, 100, 600, 200, 620);

            // Add JavaScript to the combo box to change label values based on selection
            // The script runs on the combo box's value change event
            string js = @"
                var lbl1 = this.getField('Label1');
                var lbl2 = this.getField('Label2');
                if (event.value == 'English') {
                    lbl1.value = 'Name:';
                    lbl2.value = 'Address:';
                } else if (event.value == 'Spanish') {
                    lbl1.value = 'Nombre:';
                    lbl2.value = 'Dirección:';
                }
            ";
            formEditor.AddFieldScript("Language", js);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form created: {outputPath}");
    }
}