using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class XmpValidator
{
    // List of mandatory XMP fields (keys) that must be present in the PDF.
    // Keys follow the XMP namespace prefixes, e.g., "dc:title", "dc:creator".
    private static readonly string[] RequiredXmpFields = new[]
    {
        "dc:title",
        "dc:creator",
        "dc:description"
    };

    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "validated_output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use Document constructor for loading).
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Bind the PDF to the XMP metadata facade (facade API).
                using (PdfXmpMetadata xmp = new PdfXmpMetadata())
                {
                    xmp.BindPdf(pdfDoc);

                    // Verify each required XMP field exists.
                    foreach (string key in RequiredXmpFields)
                    {
                        if (!xmp.ContainsKey(key))
                        {
                            Console.Error.WriteLine($"Missing mandatory XMP field: {key}");
                            // Abort publication because a required field is absent.
                            return;
                        }
                    }

                    // All mandatory fields are present – proceed to publish (save) the PDF.
                    // Save using the Document's Save method (lifecycle rule: use provided save).
                    pdfDoc.Save(outputPdfPath);
                    Console.WriteLine($"PDF validated and saved to '{outputPdfPath}'.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during validation: {ex.Message}");
        }
    }
}