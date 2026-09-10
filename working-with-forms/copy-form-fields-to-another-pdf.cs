using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to the source PDF (contains the form fields) and the destination PDF
        const string sourcePdfPath = "source.pdf";
        const string destinationPdfPath = "destination.pdf";
        const string outputPdfPath = "merged_with_form_fields.pdf";

        // Verify that the input files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(destinationPdfPath))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPdfPath}");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal
        using (Document srcDoc = new Document(sourcePdfPath))
        using (Document dstDoc = new Document(destinationPdfPath))
        {
            // Ensure the destination document has a Form object. Accessing dstDoc.Form will create one if it does not exist.
            var form = dstDoc.Form; // read‑only property, but accessing it lazily creates the AcroForm when needed.

            // Choose a page to place the copied fields. For simplicity we use the first page of the destination document.
            int targetPageNumber;
            if (dstDoc.Pages.Count > 0)
            {
                targetPageNumber = 1; // first existing page
            }
            else
            {
                // Add a new page and use its page number (which will be 1 after the addition)
                dstDoc.Pages.Add();
                targetPageNumber = dstDoc.Pages.Count;
            }

            // Copy each form field from the source document to the destination document
            if (srcDoc.Form != null)
            {
                foreach (Field srcField in srcDoc.Form.Fields)
                {
                    // Clone the field – this creates a deep copy of the field definition and its appearance
                    Field clonedField = (Field)srcField.Clone();

                    // Add the cloned field to the destination document on the chosen page
                    form.Add(clonedField, targetPageNumber);
                }
            }

            // Save the resulting PDF that now contains the combined form fields
            dstDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Form fields copied successfully. Output saved to '{outputPdfPath}'.");
    }
}
