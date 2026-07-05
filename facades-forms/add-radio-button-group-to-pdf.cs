using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf";  // PDF with the new radio button group

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Initialize FormEditor with the loaded document
            FormEditor formEditor = new FormEditor(doc);

            // Configure radio button layout (horizontal arrangement, small gap)
            formEditor.RadioHoriz = true;          // arrange options horizontally
            formEditor.RadioGap   = 4;             // gap between options in pixels

            // Define the options for the radio button group
            formEditor.Items = new string[] { "Credit", "PayPal" };

            // Add the radio button field on page 3.
            // Rectangle coordinates: lower‑left (llx, lly) and upper‑right (urx, ury)
            // Adjust these values as needed for your layout.
            formEditor.AddField(
                FieldType.Radio,          // type of field to add
                "PaymentMethod",          // field name (group name)
                3,                        // page number (1‑based)
                100, 500,                 // llx, lly
                200, 520);                // urx, ury

            // Save the modified document (the FormEditor works directly on the Document instance)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Radio button group 'PaymentMethod' added to page 3 and saved as '{outputPdf}'.");
    }
}