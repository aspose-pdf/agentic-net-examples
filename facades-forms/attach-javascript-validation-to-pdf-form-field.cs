using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

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
        Document doc = new Document(inputPath);

        // JavaScript that shows a warning if the value is less than 18
        string js = "if (Number(event.value) < 18) { app.alert('You must be at least 18 years old.'); }";

        // Retrieve the form field named "Age"
        TextBoxField ageField = doc.Form["Age"] as TextBoxField;
        if (ageField == null)
        {
            Console.Error.WriteLine("Field 'Age' not found or is not a TextBox.");
            return;
        }

        // Attach the script to the field's OnValidate action (executed when the field is validated)
        ageField.Actions.OnValidate = new JavascriptAction(js);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"JavaScript attached to field 'Age' and saved as '{outputPath}'.");
    }
}
