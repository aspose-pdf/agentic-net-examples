using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Needed for JavascriptAction

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "myField"; // name of the form field to monitor

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define a document‑level JavaScript function that will be called when the field changes.
            // The function is stored as the document's OpenAction – this is the supported way to add
            // JavaScript to a PDF with the core Aspose.Pdf API.
            doc.OpenAction = new JavascriptAction(
                "function myFunction() { app.alert('Field value changed'); }"
            );

            // Retrieve the form field by its name; cast to the appropriate field type.
            // Adjust the cast if you need to work with a different field type.
            TextBoxField field = doc.Form[fieldName] as TextBoxField;

            if (field != null)
            {
                // Create a JavaScript action that calls the function defined above.
                JavascriptAction onChangeAction = new JavascriptAction("myFunction();");

                // Attach the action to a supported event. For value changes the most reliable
                // event is OnCalculate (executed when the field's value is recalculated).
                field.Actions.OnCalculate = onChangeAction;
            }
            else
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or not a TextBoxField.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript listener saved to '{outputPath}'.");
    }
}
