using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_ip.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Create a hidden text box field to store the IP address
            TextBoxField ipField = new TextBoxField(doc)
            {
                PartialName = "UserIP",   // field name
                Value = ""                // initial empty value
            };

            // Add the field to the form on page 1 (1‑based index)
            doc.Form.Add(ipField, 1);

            // Place the field off‑page so it is not visible to the user
            // Rectangle(left, bottom, width, height) – using zero size at (0,0)
            doc.Form.AddFieldAppearance(ipField, 1, new Aspose.Pdf.Rectangle(0, 0, 0, 0));

            // JavaScript to capture the user's IP address on form load.
            // This is a placeholder; replace with actual JS that obtains the IP.
            string jsCode = "this.getField('UserIP').value = app.getIP();";
            JavascriptAction jsAction = new JavascriptAction(jsCode);

            // Attach the JavaScript to the field so it runs when the document is opened
            ipField.ExecuteFieldJavaScript(jsAction);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with hidden IP field: {outputPath}");
    }
}
