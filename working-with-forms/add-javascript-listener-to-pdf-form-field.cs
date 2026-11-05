using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "MyTextField"; // name of the form field to monitor

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has a form and the field exists
            if (doc.Form != null && doc.Form.Fields.Any(f => f.Name == fieldName))
            {
                // Retrieve the field via the indexer and cast to TextBoxField (adjust if needed)
                TextBoxField txtField = doc.Form[fieldName] as TextBoxField;
                if (txtField != null)
                {
                    // Assign a JavaScript action that will be executed when the field value changes.
                    // Aspose.Pdf uses the OnModifyCharacter or OnCalculate actions for value‑change events.
                    txtField.Actions.OnModifyCharacter = new JavascriptAction("app.alert('Field value has changed.');");
                }
                else
                {
                    Console.Error.WriteLine($"Field '{fieldName}' is not a TextBoxField.");
                }
            }
            else
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found in the PDF.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript listener saved to '{outputPath}'.");
    }
}
