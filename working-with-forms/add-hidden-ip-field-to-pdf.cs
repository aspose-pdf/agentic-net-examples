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
        const string outputPath = "output_with_ip.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define a zero‑size rectangle (in points) – the field will be invisible
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

            // Create a hidden text box field named "UserIP"
            TextBoxField ipField = new TextBoxField(doc.Pages[1], fieldRect);
            ipField.PartialName = "UserIP";
            ipField.Flags = AnnotationFlags.Hidden; // hide the field

            // Add the field to the document's form collection
            doc.Form.Add(ipField);

            // Add an appearance for the field (required even for hidden fields)
            doc.Form.AddFieldAppearance(ipField, 1, fieldRect);

            // JavaScript that (in a real viewer) would capture the user's IP address.
            // Here we assign a placeholder value.
            string jsCode = @"
                var ip = '0.0.0.0'; // placeholder – replace with real logic if supported by the viewer
                this.getField('UserIP').value = ip;
            ";

            // Attach the script to the field. Using OnCalculate works for executing JS when the form is processed.
            ipField.Actions.OnCalculate = new JavascriptAction(jsCode);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with hidden IP field: {outputPath}");
    }
}
