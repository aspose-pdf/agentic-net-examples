using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // Required for form field types

class Program
{
    static void Main()
    {
        const string sourcePdf = "source.pdf";      // PDF containing the original form fields
        const string destinationPdf = "dest.pdf";   // PDF that will receive the copied fields
        const string outputPdf = "merged_form.pdf"; // Resulting PDF with combined form fields

        // Verify that both input files exist before proceeding
        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }
        if (!File.Exists(destinationPdf))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPdf}");
            return;
        }

        // Load both PDFs using the core Document class (no Facades)
        Document srcDoc = new Document(sourcePdf);
        Document destDoc = new Document(destinationPdf);

        // Iterate over each form field in the source document and add a clone to the destination document
        foreach (Field srcField in srcDoc.Form.Fields)
        {
            // Clone the field – the Clone method creates a deep copy that can be added to another document
            Field clonedField = (Field)srcField.Clone();

            // Add the cloned field to the destination document.
            // Since the Field class does not expose a PageNumber property in the current API version,
            // we add the field to the last page of the destination document. Adjust this logic if you
            // need a different placement strategy.
            int targetPageNumber = destDoc.Pages.Count; // last page
            destDoc.Form.Add(clonedField, targetPageNumber);
        }

        // Save the merged document
        destDoc.Save(outputPdf);

        Console.WriteLine($"Form fields copied from '{sourcePdf}' to '{destinationPdf}' and saved as '{outputPdf}'.");
    }
}
