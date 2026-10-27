using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, add a text box form field with placeholder text, and save.
        using (Document doc = new Document(inputPath))
        {
            // Define the field rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a TextBoxField on the first page.
            TextBoxField textField = new TextBoxField(doc.Pages[1], fieldRect);
            textField.PartialName = "MyTextField"; // set the field name
            textField.Value = "Enter your name here..."; // placeholder / default text

            // Associate the field with the document's /Form (AcroForm) element.
            doc.Form.Add(textField);

            // Save the updated PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form field added and saved to '{outputPath}'.");
    }
}