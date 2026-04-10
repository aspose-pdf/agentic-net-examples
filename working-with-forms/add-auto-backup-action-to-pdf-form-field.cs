using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "template.pdf";
        const string outputPath = "form_with_backup.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Document lifecycle must be wrapped in a using block (see document-disposal-with-using rule)
        using (Document doc = new Document(inputPath))
        {
            // Access the form object (creates one if it does not exist)
            Form form = doc.Form;
            const string fieldName = "NameField";

            // Add a text box field if it does not already exist
            if (!form.HasField(fieldName))
            {
                // Create the field on page 1 with a rectangle (llx, lly, urx, ury)
                TextBoxField txt = new TextBoxField(doc, new Rectangle(100, 700, 300, 730))
                {
                    PartialName = fieldName,
                    Value = string.Empty,
                    // Border colour is set via the annotation's Color property (Border has no Color property)
                    Color = Aspose.Pdf.Color.Black
                };

                // Border requires the parent annotation in the constructor and has no Color property
                txt.Border = new Border(txt) { Width = 1 };

                // Add the field to page 1
                form.Add(txt, 1);
            }

            // Retrieve the field (newly added or existing) as a WidgetAnnotation to access Actions
            WidgetAnnotation widget = (WidgetAnnotation)form[fieldName];

            // JavaScript that saves a backup copy each time the field value changes
            string js = "this.saveAs('backup_copy.pdf');";

            // Use a valid action property – OnCalculate fires when the field value changes
            widget.Actions.OnCalculate = new JavascriptAction(js);

            // Save the modified PDF (Document.Save(string) writes a PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with auto‑backup functionality saved to '{outputPath}'.");
    }
}
