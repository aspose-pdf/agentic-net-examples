using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath   = "form.pdf";          // Input PDF with form fields
        const string xmlPath   = "data.xml";          // XML file containing form data
        const string outputPdf = "filled_form.pdf";   // Resulting PDF after data import

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Check for the presence of the XML import file before attempting to load it
            if (File.Exists(xmlPath))
            {
                // NOTE: The core Aspose.Pdf API (Document.Form) does not provide a direct
                // method for importing XML form data. That functionality is available only
                // through the Aspose.Pdf.Facades.Form class, which is prohibited by the
                // hard namespace restriction for this exercise. Therefore we cannot call
                // ImportXml here. If XML import is required, it must be performed via the
                // Facades API in a separate project that is allowed to reference that
                // namespace.
                Console.Error.WriteLine("Warning: XML import is not supported by the core API. Skipping import.");
            }
            else
            {
                // Handle the missing XML file gracefully
                Console.Error.WriteLine($"Warning: XML import file not found: {xmlPath}");
                Console.Error.WriteLine("Proceeding without importing form data.");
            }

            // Save the (potentially) updated PDF
            try
            {
                doc.Save(outputPdf);
                Console.WriteLine($"PDF saved to '{outputPdf}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error saving PDF: {ex.Message}");
            }
        }
    }
}
