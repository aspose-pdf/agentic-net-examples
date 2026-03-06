using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF with attachments
        const string outputPdfPath = "output_with_attachments.pdf"; // new PDF to create

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF and extract its embedded files
        using (Document sourceDoc = new Document(inputPdfPath))
        {
            // Create a new empty PDF document
            using (Document targetDoc = new Document())
            {
                // Add a blank page (optional, ensures the PDF has at least one page)
                targetDoc.Pages.Add();

                // Bind a PdfContentEditor to the new document for adding attachments
                PdfContentEditor editor = new PdfContentEditor();
                editor.BindPdf(targetDoc);

                // Iterate over each embedded file in the source PDF
                foreach (FileSpecification fileSpec in sourceDoc.EmbeddedFiles)
                {
                    string attachmentName = fileSpec.Name;
                    string attachmentDescription = fileSpec.Description ?? string.Empty;

                    // Copy the attachment content into a memory stream
                    using (MemoryStream attachmentStream = new MemoryStream())
                    {
                        // FileSpecification.Contents is a Stream containing the file data
                        fileSpec.Contents.CopyTo(attachmentStream);
                        attachmentStream.Position = 0; // reset for reading

                        // Add the attachment to the target PDF
                        editor.AddDocumentAttachment(attachmentStream, attachmentName, attachmentDescription);
                    }
                }

                // Save the new PDF containing all extracted attachments
                editor.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Attachments extracted and saved into '{outputPdfPath}'.");
    }
}