using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string attachmentName = "OldReport.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF to the facade
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);

        // Access the underlying Document to manipulate embedded files
        Document doc = editor.Document;

        // Delete the specific attachment by name
        // EmbeddedFiles.Delete(string) removes a single embedded file
        doc.EmbeddedFiles.Delete(attachmentName);

        // Save the modified PDF
        editor.Save(outputPdf);

        Console.WriteLine($"Attachment \"{attachmentName}\" removed. Saved to \"{outputPdf}\".");
    }
}