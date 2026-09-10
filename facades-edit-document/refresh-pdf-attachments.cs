using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Define the new attachments to add (file path and description)
        var attachments = new[]
        {
            new { Path = "file1.txt", Description = "First attachment" },
            new { Path = "file2.jpg", Description = "Second attachment" }
        };

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Verify each attachment file exists before proceeding
        foreach (var att in attachments)
        {
            if (!File.Exists(att.Path))
            {
                Console.Error.WriteLine($"Attachment file not found: {att.Path}");
                return;
            }
        }

        // Create a PdfContentEditor facade to manipulate attachments
        PdfContentEditor editor = new PdfContentEditor();

        // Bind the existing PDF document
        editor.BindPdf(inputPdf);

        // Remove all existing attachments from the PDF
        editor.DeleteAttachments();

        // Add the new set of attachments
        foreach (var att in attachments)
        {
            editor.AddDocumentAttachment(att.Path, att.Description);
        }

        // Save the updated PDF to a new file
        editor.Save(outputPdf);

        // Close the facade (releases resources)
        editor.Close();

        Console.WriteLine($"All attachments refreshed. Output saved to '{outputPdf}'.");
    }
}