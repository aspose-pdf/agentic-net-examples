using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string attachmentFile = "invoice2023.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachmentFile))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFile}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the content editor facade
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Add the attachment with the required description
            editor.AddDocumentAttachment(attachmentFile, "Invoice2023");

            // Retrieve the newly added attachment and set its creation date
            int count = doc.EmbeddedFiles.Count;
            if (count > 0)
            {
                // EmbeddedFiles collection is 1‑based
                FileSpecification fileSpec = doc.EmbeddedFiles[count];
                if (fileSpec.Params != null)
                {
                    fileSpec.Params.CreationDate = DateTime.Now;
                }
                else
                {
                    // If Params is null, create a new FileParams instance
                    fileSpec.Params = new FileParams(fileSpec);
                    fileSpec.Params.CreationDate = DateTime.Now;
                }
            }

            // Save the updated PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Attachment added and saved to '{outputPdf}'.");
    }
}