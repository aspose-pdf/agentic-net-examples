using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the input file exists – if not, create an empty PDF so the program can run without throwing.
        if (!File.Exists(inputPath))
        {
            using (var emptyDoc = new Document())
            {
                emptyDoc.Pages.Add(); // add a blank page
                emptyDoc.Save(inputPath);
            }
        }

        // Load the document.
        using (Document doc = new Document(inputPath))
        {
            // Extract attachment names.
            PdfExtractor extractor = new PdfExtractor();
            extractor.BindPdf(inputPath);
            extractor.ExtractAttachment();
            IList<string> attachmentNames = extractor.GetAttachNames();

            // Remove attachments whose names end with "_old.pdf".
            // Iterate over a copy of the list to avoid collection‑modification issues.
            foreach (string name in attachmentNames)
            {
                if (!string.IsNullOrEmpty(name) &&
                    name.EndsWith("_old.pdf", StringComparison.OrdinalIgnoreCase))
                {
                    // Delete by name – the Delete(string) overload is the correct one.
                    doc.EmbeddedFiles.Delete(name);
                }
            }

            doc.Save(outputPath);
        }

        Console.WriteLine("Attachments ending with '_old.pdf' have been removed.");
    }
}
