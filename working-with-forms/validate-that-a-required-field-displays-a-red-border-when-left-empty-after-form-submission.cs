using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // for PropertyFlag enum

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF with a form field
        const string outputPdf = "output_required.pdf"; // PDF after marking field as required
        const string logFile   = "validation.log";     // validation log file
        const string fieldName = "MyTextField";        // full name of the field to be required

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document contains a form
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.Error.WriteLine("No form fields found in the document.");
                return;
            }

            // Use FormEditor (Facade) to set the Required flag on the specified field
            FormEditor formEditor = new FormEditor(doc);
            // PropertyFlag.Required marks the field as required; the viewer will draw a red border
            // when the field is left empty after form submission.
            formEditor.SetFieldAttribute(fieldName, PropertyFlag.Required);

            // Save the modified PDF
            doc.Save(outputPdf);
            Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");

            // Optional: run PDF validation to generate a log (use any PdfFormat, e.g., PDF/A-1B)
            bool validationResult = doc.Validate(logFile, PdfFormat.PDF_A_1B);
            Console.WriteLine($"Validation completed. Success: {validationResult}. Log saved to '{logFile}'.");
        }
    }
}