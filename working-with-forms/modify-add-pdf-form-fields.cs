using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (full load is required for core API form manipulation)
        using (Document doc = new Document(inputPdf))
        {
            // Access the AcroForm object
            Form form = doc.Form;

            // Example: Fill an existing text field named "FirstName"
            if (form["FirstName"] != null && form["FirstName"] is TextBoxField existingField)
            {
                existingField.Value = "John";
            }

            // Example: Add a new text box field to page 1
            // Define the rectangle for the new field (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);
            TextBoxField newField = new TextBoxField(doc.Pages[1], rect)
            {
                PartialName = "NewField",
                Value       = "Sample Text"
            };

            // Add the new field to the form
            form.Add(newField);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
    }
}