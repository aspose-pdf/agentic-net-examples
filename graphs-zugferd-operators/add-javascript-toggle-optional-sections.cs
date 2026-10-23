using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input_form.pdf";
        const string outputPath = "output_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document contains form fields
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                doc.Save(outputPath);
                return;
            }

            // ------------------------------------------------------------
            // Example: a CheckBox field named "ShowDetails" controls the
            // visibility of two other fields: "Detail1" and "Detail2".
            // ------------------------------------------------------------
            var showDetails = doc.Form["ShowDetails"];
            if (showDetails != null)
            {
                // JavaScript that toggles the display state of the target fields
                string js = @"
var show = this.getField('ShowDetails').value == 'Yes';
var fields = ['Detail1','Detail2'];
for (var i = 0; i < fields.length; i++) {
    var f = this.getField(fields[i]);
    if (f) f.display = show ? display.visible : display.hidden;
}
";
                // Assign the script to the OnPressMouseBtn action (supported action) of the field
                showDetails.Actions.OnPressMouseBtn = new JavascriptAction(js);
            }

            // ------------------------------------------------------------
            // Additional example: a RadioButton group named "OptionGroup"
            // that shows different sections based on the selected option.
            // ------------------------------------------------------------
            var optionGroup = doc.Form["OptionGroup"];
            if (optionGroup != null)
            {
                string js = @"
var selected = this.getField('OptionGroup').value;
var sections = {
    'A': ['SectionA1','SectionA2'],
    'B': ['SectionB1','SectionB2']
};

for (var key in sections) {
    var visible = (key == selected);
    var fields = sections[key];
    for (var i = 0; i < fields.length; i++) {
        var f = this.getField(fields[i]);
        if (f) f.display = visible ? display.visible : display.hidden;
    }
}
";
                // Use the same supported action property for radio buttons
                optionGroup.Actions.OnPressMouseBtn = new JavascriptAction(js);
            }

            // Save the modified PDF (no PreSave call required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
