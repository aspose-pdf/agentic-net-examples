using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class UnicodeFormFieldHandler
{
    static void Main()
    {
        const string pdfPath       = "input_form.pdf";      // source PDF with form fields
        const string exportXmlPath = "form_data.xml";       // exported form data (UTF‑8)
        const string importXmlPath = "form_data.xml";       // same file used for import

        // Ensure the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Export form field values to XML using UTF‑8 encoding
        // -----------------------------------------------------------------
        using (Document doc = new Document(pdfPath))          // Document lifecycle – deterministic disposal
        using (Form form = new Form(doc))                     // Form facade works on the opened document
        {
            // Export the fields to a memory stream first
            using (MemoryStream ms = new MemoryStream())
            {
                form.ExportXml(ms);                           // Writes XML representation of the fields
                ms.Position = 0;                              // Reset for reading

                // Write the XML bytes to a file using UTF‑8 encoding.
                // The XML produced by ExportXml already contains an XML declaration,
                // but we enforce UTF‑8 by writing the raw bytes (which are UTF‑8 encoded).
                File.WriteAllBytes(exportXmlPath, ms.ToArray());
                Console.WriteLine($"Form data exported to '{exportXmlPath}' (UTF‑8).");
            }
        }

        // -----------------------------------------------------------------
        // Import form field values from the UTF‑8 XML back into the PDF
        // -----------------------------------------------------------------
        using (Document doc = new Document(pdfPath))          // Load the same PDF (or a copy) for import
        using (Form form = new Form(doc))
        {
            // Open the exported XML file as a UTF‑8 stream
            using (FileStream fs = new FileStream(importXmlPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportXml(fs);                           // Reads field values from the UTF‑8 XML
                Console.WriteLine($"Form data imported from '{importXmlPath}'.");
            }

            // Save the PDF with the updated field values
            const string outputPdf = "output_form_filled.pdf";
            doc.Save(outputPdf);
            Console.WriteLine($"Updated PDF saved to '{outputPdf}'.");
        }
    }
}