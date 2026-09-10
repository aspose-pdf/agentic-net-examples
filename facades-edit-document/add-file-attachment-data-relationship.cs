using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf   = "input.pdf";
        const string attachment = "datafile.bin";
        const string outputPdf  = "output_with_attachment.pdf";

        // Verify files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachment))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachment}");
            return;
        }

        // Use PdfContentEditor (facade) to edit the PDF
        PdfContentEditor editor = new PdfContentEditor();

        // Bind the existing PDF document
        editor.BindPdf(inputPdf);

        // Add the attachment without a visible annotation
        // Second parameter is a description for the attachment
        editor.AddDocumentAttachment(attachment, "Sample data attachment");

        // After adding, the attachment is stored in the underlying Document's EmbeddedFiles collection.
        // Set the relationship type (AFRelationship) to "Data" for better organization.
        // Aspose collections are 1‑based, so the first embedded file is at index 1.
        if (editor.Document.EmbeddedFiles != null && editor.Document.EmbeddedFiles.Count > 0)
        {
            // FileSpecification represents the attached file
            var fileSpec = editor.Document.EmbeddedFiles[1]; // 1‑based index

            // The AFRelationship property expects an enum value, not a string.
            fileSpec.AFRelationship = Aspose.Pdf.AFRelationship.Data;
        }

        // Save the modified PDF
        editor.Save(outputPdf);

        // Clean up
        editor.Close();

        Console.WriteLine($"Attachment added and saved to '{outputPdf}'.");
    }
}
