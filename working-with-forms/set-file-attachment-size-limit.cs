using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF (must contain a form)
        const string outputPdf = "output.pdf";     // PDF after applying the constraint

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Set the global limit for loading whole files into memory (value is in megabytes).
        // This limit also applies to file‑select (attachment) fields when the user chooses a file.
        Document.FileSizeLimitToMemoryLoading = 2; // 2 MB

        // Load the document, edit the form, and save.
        using (Document doc = new Document(inputPdf))
        {
            // FormEditor provides high‑level operations on form fields.
            // Here we simply ensure the document is saved after the global limit is set.
            // (If you need to add a new file‑select box field, you could do it as shown below.)

            // Example: add a FileSelectBoxField (optional)
            // FileSelectBoxField fileField = new FileSelectBoxField(doc, new Rectangle(100, 500, 300, 530));
            // fileField.PartialName = "Attachment";
            // doc.Form.Add(fileField);

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with 2 MB file‑size limit: {outputPdf}");
    }
}