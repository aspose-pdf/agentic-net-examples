using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // Added namespace for TextAnnotation

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
        using (FileStream fs = new FileStream(inputPath, FileMode.Open, FileAccess.ReadWrite))
        {
            using (Document doc = new Document(fs))
            {
                // Example modification: add a text annotation on the first page.
                Page page = doc.Pages[1];
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                TextAnnotation txtAnn = new TextAnnotation(page, rect)
                {
                    Title    = "Note",
                    Contents = "Incremental update example",
                    Open     = true,
                    Color    = Aspose.Pdf.Color.Yellow
                };
                page.Annotations.Add(txtAnn);

                // Save incrementally – no parameters means “save using incremental update technique”.
                doc.Save();
            }
        }

        // Use the Facades API to write the updated PDF to a new file.
        // PdfFileInfo works on the original file; SaveNewInfo creates a copy that includes the incremental changes.
        PdfFileInfo fileInfo = new PdfFileInfo();
        fileInfo.BindPdf(inputPath);
        bool saved = fileInfo.SaveNewInfo(outputPath);

        Console.WriteLine(saved
            ? $"Incremental update saved to '{outputPath}'."
            : "Failed to save incremental update.");
    }
}
