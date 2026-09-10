using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed_input.pdf";
        const string outputPdf = "signed_modified.pdf";

        // -----------------------------------------------------------------
        // Step 1: Create a sample PDF with a signature field so that the file
        //         exists in the sandbox. This mimics a "digitally signed"
        //         document for the purpose of the demo.
        // -----------------------------------------------------------------
        using (Document seed = new Document())
        {
            // Add two pages – we will rotate the second one later.
            seed.Pages.Add(); // Page 1
            seed.Pages.Add(); // Page 2

            // Add a (placeholder) signature field on page 1.
            // The field itself does not contain a cryptographic signature,
            // but its presence allows us to demonstrate that incremental
            // updates preserve existing signatures/fields.
            Rectangle sigRect = new Rectangle(100, 100, 250, 150);
            // NOTE: SignatureField constructor requires a Page, not a Document.
            Page firstPage = seed.Pages[1];
            SignatureField sigField = new SignatureField(firstPage, sigRect);
            sigField.PartialName = "Signature1"; // set the field name after construction
            seed.Form.Add(sigField);

            // Save the seed PDF that will act as the signed input.
            seed.Save(inputPdf);
        }

        // -----------------------------------------------------------------
        // Step 2: Load the (pretend) signed PDF and modify pages using
        //         PdfPageEditor. The editor writes changes as an incremental
        //         update, which keeps existing signatures/fields intact.
        // -----------------------------------------------------------------
        using (Document doc = new Document(inputPdf))
        {
            // Bind the document to PdfPageEditor.
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Rotate only page 2 (1‑based indexing) by 90 degrees.
                editor.Rotation = 90;
                editor.ProcessPages = new int[] { 2 };

                // Change the page size of the edited pages to A4 (595 x 842 points).
                editor.PageSize = new PageSize(595, 842);

                // Optional: set zoom factor (1.0 = 100%).
                editor.Zoom = 1.0f;

                // Apply the queued changes.
                editor.ApplyChanges();

                // Save using incremental update – signatures/fields remain valid.
                editor.Save(outputPdf);
            }
        }

        Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
    }
}
