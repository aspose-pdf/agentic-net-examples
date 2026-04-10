using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_attachment.pdf";
        const string attachmentPath = "attachment.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        using (Document doc = new Document(inputPdf))
        {
            // -------------------------------------------------------------
            // 1. Create a FileSpecification for the attachment and set its
            //    contents via a stream. Then add it to the EmbeddedFiles
            //    collection of the document.
            // -------------------------------------------------------------
            var fileSpec = new FileSpecification(attachmentPath, "Attachment");
            using (FileStream attStream = File.OpenRead(attachmentPath))
            {
                var mem = new MemoryStream();
                attStream.CopyTo(mem);
                mem.Position = 0;
                fileSpec.Contents = mem;
            }
            doc.EmbeddedFiles.Add(fileSpec);

            // -------------------------------------------------------------
            // 2. Store attachment metadata (e.g., file name) into the
            //    document information dictionary.
            // -------------------------------------------------------------
            doc.Info.Add("Attachment", Path.GetFileName(attachmentPath));

            // -------------------------------------------------------------
            // 3. Save the document using explicit PdfSaveOptions.
            // -------------------------------------------------------------
            var saveOpts = new PdfSaveOptions();
            doc.Save(outputPdf, saveOpts);
        }

        Console.WriteLine($"PDF saved with attachment and metadata: {outputPdf}");
    }
}
