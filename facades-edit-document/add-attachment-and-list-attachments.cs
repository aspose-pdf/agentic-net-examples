using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the attachment file, and the output PDF
        const string sourcePdfPath      = "input.pdf";
        const string attachmentFilePath = "attachment.txt";
        const string attachmentDesc     = "Sample attachment";
        const string outputPdfPath      = "output_with_attachment.pdf";

        // ---------------------------------------------------------------
        // Create a minimal source PDF so the sandbox has a file to open
        // ---------------------------------------------------------------
        using (Document seed = new Document())
        {
            seed.Pages.Add(); // add a blank page
            seed.Save(sourcePdfPath);
        }

        // ---------------------------------------------------------------
        // Create a simple attachment file that will be embedded
        // ---------------------------------------------------------------
        File.WriteAllText(attachmentFilePath, "This is a sample attachment file.");

        // -----------------------------------------------------------------
        // Add an attachment to the PDF using PdfContentEditor (Facades API)
        // -----------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the existing PDF
            editor.BindPdf(sourcePdfPath);

            // Attach the file (no visual annotation is added)
            editor.AddDocumentAttachment(attachmentFilePath, attachmentDesc);

            // Save the modified PDF
            editor.Save(outputPdfPath);
        }

        // ---------------------------------------------------------------
        // List all attachment names in the resulting PDF using PdfExtractor
        // ---------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF that now contains the attachment
            extractor.BindPdf(outputPdfPath);

            // Must call ExtractAttachment before retrieving names
            extractor.ExtractAttachment();

            // Retrieve the list of attachment names (generic IList<string>)
            IList<string> attachmentNames = extractor.GetAttachNames();

            // Output each attachment name to the console
            Console.WriteLine("Attachments in the PDF:");
            foreach (string name in attachmentNames)
            {
                Console.WriteLine($"- {name}");
            }
        }
    }
}
