using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF containing the "Discount" field
        const string outputPath = "output.pdf";  // PDF with updated field restrictions

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the document and edit the form using FormEditor (facade API)
        using (Document doc = new Document(inputPath))
        {
            // Access the existing field as a NumberField to restrict allowed characters
            NumberField discountField = doc.Form["Discount"] as NumberField;
            if (discountField != null)
            {
                // Allow only digits and a decimal point
                discountField.AllowedChars = "0123456789.";
                // Optionally limit the total length (e.g., 10 characters)
                discountField.MaxLen = 10;
            }

            // Use FormEditor to attach JavaScript validation for two‑decimal precision
            FormEditor editor = new FormEditor(doc);
            // JavaScript: value must match digits optionally followed by a dot and up to two digits
            string validationJs = "event.rc = /^[0-9]+(\\.[0-9]{0,2})?$/.test(event.value);";
            editor.SetFieldScript("Discount", validationJs);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}