using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // 1. Convert the single‑line "Address" field to a multi‑line field.
            //    This makes the field capable of expanding its height
            //    automatically when the content wraps.
            // -----------------------------------------------------------------
            FormEditor formEditor = new FormEditor(doc);
            // Single2Multiple changes a single‑line text field into a
            // multi‑line field (enables word‑wrap and dynamic height).
            formEditor.Single2Multiple("Address");

            // -----------------------------------------------------------------
            // 2. Ensure the field is set to multiline (word‑wrap) explicitly.
            //    Use the indexer on Document.Form to retrieve the field.
            // -----------------------------------------------------------------
            // Retrieve the field by its partial name; the indexer returns a Field.
            TextBoxField addressField = doc.Form["Address"] as TextBoxField;
            if (addressField != null)
            {
                addressField.Multiline = true;   // enable word wrap
                addressField.Scrollable = false; // allow height to grow
                addressField.MaxLen = 500;        // optional maximum length
            }
            else
            {
                Console.Error.WriteLine("Address field not found or is not a TextBoxField.");
            }

            // -----------------------------------------------------------------
            // 3. Save the modified PDF.
            // -----------------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Address field updated and saved to '{outputPdf}'.");
    }
}
