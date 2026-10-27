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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field by its name (replace "myField" with the actual field name)
            if (doc.Form["myField"] is TextBoxField field)
            {
                // JavaScript that runs when the field loses focus (OnBlur event)
                // Example: calculate the sum of two other fields (fieldA and fieldB) and set the result in this field
                string jsCode = @"
                    var a = this.getField('fieldA').value;
                    var b = this.getField('fieldB').value;
                    var sum = Number(a) + Number(b);
                    this.getField('myField').value = sum;
                ";

                // Assign the JavaScript action to the OnLostFocus event (blur)
                field.Actions.OnLostFocus = new JavascriptAction(jsCode);
            }
            else
            {
                Console.Error.WriteLine("Form field 'myField' not found or is not a TextBoxField.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript action to '{outputPath}'.");
    }
}
