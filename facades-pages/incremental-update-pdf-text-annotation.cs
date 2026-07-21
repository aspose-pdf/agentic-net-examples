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

        // Open the source PDF with read/write access – required for incremental saving
        using (FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.ReadWrite))
        using (Document doc = new Document(stream))
        {
            // ---- Example modification: add a text annotation on the first page ----
            Page page = doc.Pages[1];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title    = "Incremental Update",
                Contents = "Added via incremental save",
                Color    = Aspose.Pdf.Color.Yellow,
                Open     = true,
                Icon     = TextIcon.Note
            };
            page.Annotations.Add(annotation);

            // ---- Use a Facade class (PdfFileInfo) to demonstrate Facades usage ----
            // PdfFileInfo can retrieve information about the PDF; here we just instantiate it.
            PdfFileInfo info = new PdfFileInfo();
            // (No explicit binding is needed for PdfFileInfo; it works with the file on disk.)

            // Save the document incrementally – this preserves the original file structure
            // and writes only the changes as an incremental update.
            doc.Save(); // Incremental save because the document was opened with a writable stream
        }

        // After the incremental save, the original file has been updated.
        // If you need a separate copy, simply copy the file.
        File.Copy(inputPath, outputPath, overwrite: true);
        Console.WriteLine($"Incrementally updated PDF saved to '{outputPath}'.");
    }
}