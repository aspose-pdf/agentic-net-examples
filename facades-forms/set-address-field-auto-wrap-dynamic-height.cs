using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

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

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Convert the existing single‑line "Address" field to a multi‑line field
            FormEditor formEditor = new FormEditor(doc);
            formEditor.Single2Multiple("Address");

            // Retrieve the field as a TextBoxField to set additional properties
            TextBoxField addressField = doc.Form["Address"] as TextBoxField;
            if (addressField != null)
            {
                // Enable multiline (required for word‑wrap)
                addressField.Multiline = true;

                // Reduce font size automatically to fit the rectangle
                Field.FitIntoRectangle = true;

                // Optionally, you can set a maximum length if desired
                // addressField.MaxLen = 200;

                // Height can be left as is; the field will expand vertically when the user types
                // (Aspose.Pdf automatically handles dynamic height for multiline fields).
            }
            else
            {
                Console.Error.WriteLine("Field \"Address\" not found or is not a text box.");
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
    }
}