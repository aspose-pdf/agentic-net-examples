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
        const string fieldName = "MyField"; // name of the field to monitor

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field by its name and cast to Field
            Field field = doc.Form[fieldName] as Field;
            if (field == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a form field.");
                return;
            }

            // Attach a JavaScript action that runs when the field value changes
            // OnValidate is triggered when the user changes the field contents
            field.Actions.OnValidate = new JavascriptAction("app.alert('Field value changed');");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript listener to '{outputPath}'.");
    }
}
