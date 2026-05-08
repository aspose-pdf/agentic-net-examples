using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // Verify that the file exists before attempting to load it
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Access the AcroForm associated with the document
            Form acroForm = pdfDoc.Form;

            // Iterate over all form fields and output basic information
            foreach (Field field in acroForm.Fields)
            {
                Console.WriteLine($"Field name: {field.FullName}, Type: {field.GetType().Name}");

                // Example: if the field is a text box, display its current value
                if (field is TextBoxField txtField)
                {
                    Console.WriteLine($"  Value: {txtField.Value}");
                }
            }
        }
    }
}
