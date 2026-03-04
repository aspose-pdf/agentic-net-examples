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
        const string outputPath = "output_js.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The PDF has no pages.");
                return;
            }

            // Define a rectangle for the new field (left, bottom, width, height)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 200, 20);

            // Create a text box field on the first page
            TextBoxField jsField = new TextBoxField(doc, fieldRect)
            {
                Name = "jsField",
                PartialName = "jsField",
                Value = "Click Me"
            };

            // Add the field to the document's form collection
            doc.Form.Add(jsField);

            // Create a JavaScript action
            JavascriptAction jsAction = new JavascriptAction("app.alert('Hello from JavaScript!');");

            // Attach the JavaScript action to the field (field‑level script)
            jsField.ExecuteFieldJavaScript(jsAction);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with embedded JavaScript saved to '{outputPath}'.");
    }
}