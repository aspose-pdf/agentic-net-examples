using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string headerImage = "header.png";

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(headerImage))
        {
            Console.Error.WriteLine($"Header image not found: {headerImage}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Add a background image as a page header using PdfFileStamp
        // -----------------------------------------------------------------
        // PdfFileStamp is a facade for adding stamps (watermarks, headers, footers)
        // to an existing PDF. We use the parameterless constructor, bind the source
        // PDF, add the image as a header, and save to a temporary file.
        string tempPdf = Path.GetTempFileName();

        using (PdfFileStamp pdfStamp = new PdfFileStamp())
        {
            pdfStamp.BindPdf(inputPdf);                     // Load source PDF
            pdfStamp.AddHeader(headerImage, 20);            // Add image header, 20pt top margin
            pdfStamp.Save(tempPdf);                         // Persist changes
            pdfStamp.Close();                               // Release resources
        }

        // ---------------------------------------------------------------
        // Step 2: Decorate the form field named "Header" (center alignment)
        // ---------------------------------------------------------------
        // FormEditor works with AcroForm fields. We set the Facade to define
        // visual attributes and then apply those attributes to the specific field.
        using (FormEditor formEditor = new FormEditor(tempPdf, outputPdf))
        {
            formEditor.Facade = new FormFieldFacade();                 // Initialize facade
            formEditor.Facade.Alignment = FormFieldFacade.AlignCenter; // Center text alignment
            // Background image for a field is not directly supported; the header
            // image added in step 1 serves as the visual background.
            formEditor.DecorateField("Header");                        // Apply to field
            formEditor.Save();                                         // Write final PDF
        }

        // Clean up the intermediate temporary file
        File.Delete(tempPdf);

        Console.WriteLine($"PDF with decorated field saved to '{outputPdf}'.");
    }
}