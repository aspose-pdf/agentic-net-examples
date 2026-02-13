using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades; // Provides PdfConverter

class Program
{
    static void Main()
    {
        // Paths for input and output PDFs
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the input PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Manipulate AcroForm fields if the document contains a form
            if (pdfDocument.Form != null && pdfDocument.Form.Count > 0)
            {
                // Example: set a text field named "NameField"
                try
                {
                    // The Form indexer returns a WidgetAnnotation; cast it to Field to access Field members
                    Field nameField = pdfDocument.Form["NameField"] as Field;
                    if (nameField != null)
                    {
                        nameField.Value = "John Doe";               // Set field value
                        nameField.AlternateName = "Full Name";      // Tooltip (alternate name)
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unable to set 'NameField': {ex.Message}");
                }

                // Example: set a date field named "DateField"
                try
                {
                    Field dateField = pdfDocument.Form["DateField"] as Field;
                    if (dateField != null)
                    {
                        dateField.Value = DateTime.Now.ToShortDateString();
                    }
                }
                catch (Exception) { /* ignore if field not present */ }

                // Flatten the form so that field appearances are baked into the page (useful for printing)
                try
                {
                    // Use the parameter‑less overload which is available in all Aspose.Pdf versions.
                    pdfDocument.Form.Flatten();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Form flattening failed: {ex.Message}");
                }
            }

            // Convert the PDF (e.g., to images) using PdfConverter.
            // No FormPresentationMode is set because the enum may be unavailable in older library versions.
            using (PdfConverter converter = new PdfConverter())
            {
                converter.BindPdf(pdfDocument);
                // Additional conversion steps (e.g., DoConvert, SaveAsTIFF) can be added here.
            }

            // Save the modified PDF document
            pdfDocument.Save(outputPath);
            Console.WriteLine($"Modified PDF saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while processing the PDF: {ex.Message}");
        }
    }
}
