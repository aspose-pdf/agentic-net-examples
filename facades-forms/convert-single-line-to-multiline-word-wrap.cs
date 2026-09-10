using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // PDF containing the "Address" field
        const string outputPdf = "output.pdf";     // Resulting PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // 1. Convert the existing single‑line "Address" field to a multi‑line
            //    field.  FormEditor.Single2Multiple changes the field type so that
            //    the PDF viewer will automatically wrap text entered by the user.
            // -----------------------------------------------------------------
            FormEditor formEditor = new FormEditor(doc);
            bool converted = formEditor.Single2Multiple("Address");
            if (!converted)
            {
                Console.Error.WriteLine("Failed to convert 'Address' field to multiline.");
                return;
            }

            // Save the changes made by FormEditor
            formEditor.Save();

            // -----------------------------------------------------------------
            // 2. Enable word‑wrap for any text that might be added programmatically
            //    via PdfFileMend.  Setting IsWordWrap to true makes the AddText
            //    methods wrap the supplied FormattedText within the rectangle.
            // -----------------------------------------------------------------
            PdfFileMend pdfMend = new PdfFileMend(doc);
            pdfMend.IsWordWrap = true;   // true enables automatic word wrapping

            // No explicit AddText call is required here because the field itself
            // now supports multiline input.  The PdfFileMend instance is kept
            // only to ensure the document’s internal settings reflect the wrap
            // behavior for any future AddText operations.

            // -----------------------------------------------------------------
            // 3. Save the updated PDF
            // -----------------------------------------------------------------
            pdfMend.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}