using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Paths to the input PDF form and the output XFDF (XML) log file
        const string inputPdfPath  = "form.pdf";
        const string outputXfdfPath = "interaction_log.xfdf";

        // Verify that the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (which contains the form)
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // OPTIONAL: fill form fields here if needed, e.g.
                // pdfDocument.Form["NameField"].Value = "John Doe";

                // Export all annotations (including form field values) to XFDF.
                // XFDF is an XML representation of the form data and annotation interactions.
                using (FileStream xfdfStream = new FileStream(outputXfdfPath, FileMode.Create, FileAccess.Write))
                {
                    pdfDocument.ExportAnnotationsToXfdf(xfdfStream);
                }

                Console.WriteLine($"Interaction log exported to '{outputXfdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}