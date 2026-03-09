using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "form_fields.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define rectangles for the fields (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle txtRect   = new Aspose.Pdf.Rectangle(100, 600, 300, 630);
            Aspose.Pdf.Rectangle chkRect   = new Aspose.Pdf.Rectangle(100, 540, 120, 560);
            Aspose.Pdf.Rectangle comboRect = new Aspose.Pdf.Rectangle(100, 480, 300, 510);

            // ----- TextBoxField -----
            TextBoxField txtField = new TextBoxField(page, txtRect);
            txtField.PartialName = "NameField";          // field identifier
            txtField.Contents    = "Enter name";        // tooltip / default text
            txtField.Color       = Aspose.Pdf.Color.LightGray;

            // ----- CheckBoxField -----
            CheckboxField chkField = new CheckboxField(page, chkRect);
            chkField.PartialName = "Subscribe";         // field identifier
            chkField.Contents    = "Subscribe to newsletter";
            chkField.Checked     = false;               // default unchecked

            // ----- ComboBoxField -----
            ComboBoxField comboField = new ComboBoxField(page, comboRect);
            comboField.PartialName = "Country";          // field identifier
            comboField.Contents    = "Select country";
            comboField.AddOption("USA");
            comboField.AddOption("Canada");
            comboField.AddOption("Mexico");
            comboField.Selected    = 0;                 // default to first option

            // Add the fields to the document's form
            doc.Form.Add(txtField);
            doc.Form.Add(chkField);
            doc.Form.Add(comboField);

            // Save the PDF (output format is PDF by default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with interactive form fields saved to '{outputPath}'.");
    }
}