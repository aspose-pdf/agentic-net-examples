using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a new empty PDF document that can hold a form.
        // The using block ensures deterministic disposal of the Document.
        using (Document doc = new Document())
        {
            // Access the AcroForm associated with the document.
            // This object allows you to add fields, set appearance, etc.
            var form = doc.Form;

            // Example: enable automatic recalculation of form fields.
            form.AutoRecalculate = true;

            // Optionally save the newly created (empty) form PDF.
            // Document.Save(string) always writes PDF regardless of extension.
            doc.Save("empty_form.pdf");
        }
    }
}