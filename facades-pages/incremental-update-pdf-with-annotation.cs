using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_incremental.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF with read/write access so that Document.Save() can perform an incremental update.
        using (FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.ReadWrite))
        {
            // Load the document from the writable stream.
            using (Document doc = new Document(stream))
            {
                // Example modification: add a text annotation on the first page.
                Page page = doc.Pages[1];
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                TextAnnotation annotation = new TextAnnotation(page, rect)
                {
                    Title    = "Note",
                    Contents = "Incremental update example",
                    Open     = true,
                    Color    = Aspose.Pdf.Color.Yellow
                };
                page.Annotations.Add(annotation);

                // Save incrementally – no parameters means an incremental update is written to the same stream.
                doc.Save();
            }
        }

        // Copy the updated PDF (which now contains the incremental update) to a new file.
        // PdfFileInfo preserves the incremental changes when saving to a new location.
        PdfFileInfo fileInfo = new PdfFileInfo(inputPath);
        fileInfo.SaveNewInfo(outputPath);
    }
}