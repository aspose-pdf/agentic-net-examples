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

        // Open the PDF with a read/write FileStream.
        // This is required for the Document.Save() overload that performs an incremental update.
        using (FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.ReadWrite))
        {
            // Load the document from the writable stream.
            using (Document doc = new Document(stream))
            {
                // Example modification: add a text annotation on the first page.
                Page page = doc.Pages[1];
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                TextAnnotation txtAnn = new TextAnnotation(page, rect)
                {
                    Title    = "Note",
                    Contents = "Incremental update example",
                    Color    = Aspose.Pdf.Color.Yellow,
                    Open     = true,
                    Icon     = TextIcon.Note
                };
                page.Annotations.Add(txtAnn);

                // Save the document incrementally (no parameters).
                // Because the document was opened from a writable stream,
                // this call writes only the changes as an incremental update.
                doc.Save();
            }
        }

        // The original file now contains the incremental update.
        // If a separate output file is desired, copy the updated source file.
        File.Copy(inputPath, outputPath, true);
        Console.WriteLine($"Incrementally updated PDF saved to '{outputPath}'.");

        // OPTIONAL: demonstrate use of a Facade class (PdfFileInfo) as required.
        // PdfFileInfo can be used to read or write document information without loading the full Document.
        PdfFileInfo fileInfo = new PdfFileInfo(outputPath);
        // SaveNewInfo writes the (already updated) PDF to a new file.
        // Here we simply rewrite to the same path to illustrate the API usage.
        bool success = fileInfo.SaveNewInfo(outputPath);
        Console.WriteLine($"PdfFileInfo.SaveNewInfo executed: {success}");
    }
}