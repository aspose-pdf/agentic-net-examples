using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Name of the list box field in the PDF form
            const string listBoxName = "myListBox";

            // Retrieve the field and cast to ListBoxField
            ListBoxField listBox = doc.Form[listBoxName] as ListBoxField;
            if (listBox == null)
            {
                Console.Error.WriteLine($"ListBox field '{listBoxName}' not found.");
                return;
            }

            // Ensure the list box allows multiple selections
            listBox.MultiSelect = true;

            // JavaScript validation to enforce a maximum of three selections.
            // The script runs when the field is validated; if more than three items are selected,
            // it shows an alert and cancels the change.
            string js = @"if (event.value && event.value.length > 3) {
    app.alert('Maximum of 3 items can be selected.');
    event.rc = false;
}";
            listBox.Actions.OnValidate = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
