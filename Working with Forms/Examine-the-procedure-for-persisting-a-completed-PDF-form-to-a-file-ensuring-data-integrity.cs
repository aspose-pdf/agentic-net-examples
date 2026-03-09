using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input_form.pdf";
        const string outputPath = "filled_form.pdf";
        const string validationLog = "validation_log.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document containing the interactive form
        using (Document pdfDoc = new Document(inputPath))
        {
            // Example: set a text field named "Name"
            if (pdfDoc.Form.HasField("Name"))
            {
                // The Form indexer returns a Field (not WidgetAnnotation). Cast safely.
                if (pdfDoc.Form["Name"] is Field nameField)
                {
                    nameField.Value = "John Doe";
                }
            }

            // Example: set a checkbox field named "Subscribe"
            if (pdfDoc.Form.HasField("Subscribe"))
            {
                if (pdfDoc.Form["Subscribe"] is Field subscribeField)
                {
                    // For checkboxes, the value "Yes" (or "On") checks the box
                    subscribeField.Value = "Yes";
                }
            }

            // Optional: flatten the form to make fields non‑editable and embed values into the page content
            pdfDoc.Form.Flatten();

            // Persist the completed form to a PDF file
            pdfDoc.Save(outputPath);

            // Validate the saved PDF (e.g., against PDF/A‑1B) and write a log file
            bool isValid = pdfDoc.Validate(validationLog, PdfFormat.PDF_A_1B);
            Console.WriteLine($"Validation result: {isValid}");
        }

        Console.WriteLine($"Completed form saved to '{outputPath}'.");
    }
}
