using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths for source PDF (with form fields), destination PDF (template), and result PDF
        const string sourcePdfPath = "source_form.pdf";
        const string destinationPdfPath = "destination_template.pdf";
        const string outputPdfPath = "merged_form.pdf";

        // Verify that input files exist
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

        try
        {
            // Load source and destination documents inside using blocks (lifecycle rule)
            using (Document sourceDoc = new Document(sourcePdfPath))
            using (Document destDoc = new Document(destinationPdfPath))
            {
                // ------------------------------------------------------------
                // Copy all form fields from source to destination using core API
                // ------------------------------------------------------------
                foreach (Field srcField in sourceDoc.Form.Fields)
                {
                    // Clone the field – Field implements ICloneable, so we can use Clone()
                    // The returned object must be cast back to Field.
                    Field clonedField = (Field)srcField.Clone();

                    // Add the cloned field to the destination document.
                    // The overload without a page number adds the field to the same page
                    // as the original field when possible; otherwise it defaults to the first page.
                    destDoc.Form.Add(clonedField);
                }

                // Save the resulting PDF (save rule – no extra SaveOptions needed for PDF)
                destDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Form fields copied successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
