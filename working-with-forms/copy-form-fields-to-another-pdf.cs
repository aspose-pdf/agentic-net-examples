using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string sourcePdf = "source.pdf";
        const string destPdf   = "destination.pdf";

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }
        if (!File.Exists(destPdf))
        {
            Console.Error.WriteLine($"Destination file not found: {destPdf}");
            return;
        }

        try
        {
            // Load the source and destination PDFs using the core Document API
            Document sourceDoc = new Document(sourcePdf);
            Document destDoc   = new Document(destPdf);

            // Ensure both documents have a Form object (AcroForm)
            if (sourceDoc.Form == null || destDoc.Form == null)
            {
                Console.Error.WriteLine("One of the documents does not contain a form.");
                return;
            }

            // Iterate over each form field in the source document
            foreach (var srcField in sourceDoc.Form?.Fields ?? Array.Empty<Field>())
            {
                // Clone the field so it can be added to another Document instance
                Field clonedField = (Field)srcField.Clone();

                // PageIndex is zero‑based; Form.Add expects a 1‑based page number
                int pageNumber = srcField.PageIndex + 1;

                // Add the cloned field to the destination document's form
                destDoc.Form.Add(clonedField, pageNumber);
            }

            // Save the updated destination PDF
            destDoc.Save(destPdf);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
