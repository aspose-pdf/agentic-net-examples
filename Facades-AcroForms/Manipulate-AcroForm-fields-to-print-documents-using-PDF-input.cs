using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF paths (generic names for cross‑platform use)
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "filled_output.pdf";

        // Verify that the input file exists before attempting to load it
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF file not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // ------------------------------------------------------------
            // Manipulate AcroForm fields
            // ------------------------------------------------------------
            foreach (Field field in pdfDocument.Form.Fields)
            {
                // Assign a generic value; real code would use field.Name.
                field.Value = "Sample Value";
            }

            // NOTE: The Form.PresentationMode property and the PdfPrinter class
            // are not available in the cross‑platform Aspose.Pdf library. They are
            // Windows‑only features and cause compile‑time errors on other platforms.
            // Therefore they have been removed. If printing is required on Windows,
            // use the Aspose.Pdf.Facades.PdfConverter to convert to an image format
            // and send it to a printer via OS‑specific APIs.

            // Save the modified PDF
            pdfDocument.Save(outputPdfPath);

            Console.WriteLine("PDF processing completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}