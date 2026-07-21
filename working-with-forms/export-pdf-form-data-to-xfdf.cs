using System;
using System.IO;
using Aspose.Pdf;

class ExportFormDataToXml
{
    static void Main()
    {
        // Input PDF containing the form.
        const string pdfPath = "input.pdf";

        // Desired output XML (XFDF) file path.
        const string xmlPath = "formData.xml";

        // Verify the source PDF exists.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal.
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Export all annotations (including form fields) to XFDF.
                // XFDF is an XML representation of form data, satisfying the requirement.
                pdfDocument.ExportAnnotationsToXfdf(xmlPath);
            }

            Console.WriteLine($"Form data successfully exported to XML (XFDF) at '{xmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}