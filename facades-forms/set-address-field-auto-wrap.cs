using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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
            // Convert the single‑line "Address" field to a multiline field.
            // This enables automatic word‑wrap inside the field.
            FormEditor formEditor = new FormEditor(doc);
            bool success = formEditor.Single2Multiple("Address");
            if (!success)
            {
                Console.Error.WriteLine("Failed to convert the 'Address' field to multiline.");
            }

            // Enable word‑wrap for any AddText operations performed via PdfFileMend.
            // (Not required for the field itself, but useful if you later add text programmatically.)
            PdfFileMend mend = new PdfFileMend(doc);
            mend.IsWordWrap = true;

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with auto‑wrapped 'Address' field saved to '{outputPdf}'.");
    }
}