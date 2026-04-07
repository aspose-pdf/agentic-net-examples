using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;   // for XfdfReader
using Aspose.Pdf.Facades;      // optional, not used directly here

class Program
{
    static void Main()
    {
        // Paths to the source PDF, the XFDF (XML) file that contains visibility states,
        // and the output PDF where the changes will be saved.
        const string pdfPath      = "input.pdf";
        const string xfdfPath     = "visibility_states.xfdf";
        const string outputPdfPath = "output.pdf";

        // Verify that the required files exist.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block to ensure proper disposal.
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Open the XFDF file as a stream.
                using (FileStream xfdfStream = File.OpenRead(xfdfPath))
                {
                    // Import field values (including visibility flags) from the XFDF stream
                    // into the loaded PDF document. This updates the form fields according
                    // to the data defined in the XML.
                    XfdfReader.ReadFields(xfdfStream, pdfDocument);
                }

                // Save the modified PDF to the specified output path.
                pdfDocument.Save(outputPdfPath);
            }

            Console.WriteLine($"Visibility states imported and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}