using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a new text box field (you can also retrieve an existing field via doc.Form["fieldName"])
            TextBoxField txtField = new TextBoxField(doc, new Rectangle(100, 600, 300, 650))
            {
                Name = "ScreenOnlyField",
                Value = "Editable on screen",
                // Ensure the field is not read‑only so the user can interact with it
                ReadOnly = false
            };

            // Add the field to the document's form
            doc.Form.Add(txtField);

            // Set the field's annotation flags:
            //   - Ensure the field is visible (do not set Hidden flag)
            //   - Clear the Print flag so it will not appear when the PDF is printed
            txtField.Flags &= ~AnnotationFlags.Print;          // make non‑printable
            txtField.Flags &= ~AnnotationFlags.Hidden;         // ensure it is visible

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'. Field is visible on screen but will not be printed.");
    }
}
