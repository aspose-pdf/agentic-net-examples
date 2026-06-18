using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a sample PDF (self‑contained example)
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Save("sample.pdf");
        }

        // Open the sample PDF and add form fields
        using (Document doc = new Document("sample.pdf"))
        {
            Page page = doc.Pages[1];

            // Label field that will be changed by JavaScript
            TextBoxField labelField = new TextBoxField(page, new Rectangle(100, 700, 200, 720));
            labelField.PartialName = "Label1";
            labelField.Value = "Name:";
            doc.Form.Add(labelField);

            // Input field for the name
            TextBoxField nameField = new TextBoxField(page, new Rectangle(210, 700, 350, 720));
            nameField.PartialName = "NameField";
            doc.Form.Add(nameField);

            // Language selection combo box
            ComboBoxField langCombo = new ComboBoxField(page, new Rectangle(100, 650, 200, 670));
            langCombo.PartialName = "LangCombo";
            langCombo.AddOption("English");
            langCombo.AddOption("Spanish");
            langCombo.AddOption("French");

            // JavaScript to change the label based on selected language
            string js = "var lang = event.value;" +
                        "if (lang == 'English') this.getField('Label1').value = 'Name:';" +
                        "else if (lang == 'Spanish') this.getField('Label1').value = 'Nombre:';" +
                        "else if (lang == 'French') this.getField('Label1').value = 'Nom:';";
            langCombo.Actions.OnValidate = new JavascriptAction(js);
            doc.Form.Add(langCombo);

            doc.Save("output.pdf");
        }
    }
}
