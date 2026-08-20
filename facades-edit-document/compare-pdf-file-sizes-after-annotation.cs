using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string originalPath = "original.pdf";
        const string editedPath   = "edited.pdf";

        if (!File.Exists(originalPath))
        {
            Console.Error.WriteLine($"File not found: {originalPath}");
            return;
        }

        // Load the original PDF, add a simple text annotation, and save as edited PDF
        using (Document doc = new Document(originalPath))
        {
            // Add a text annotation on the first page
            Page page = doc.Pages[1];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation txtAnn = new TextAnnotation(page, rect)
            {
                Title    = "Note",
                Contents = "Edited PDF",
                Color    = Aspose.Pdf.Color.Yellow,
                Open     = true,
                Icon     = TextIcon.Note
            };
            page.Annotations.Add(txtAnn);

            // Save the edited PDF
            doc.Save(editedPath);
        }

        // Demonstrate usage of Aspose.Pdf.Facades (PdfFileInfo) on the edited PDF
        using (PdfFileInfo info = new PdfFileInfo(editedPath))
        {
            Console.WriteLine($"Edited PDF pages: {info.NumberOfPages}");
        }

        // Compare file sizes of the original and edited PDFs
        long originalSize = new FileInfo(originalPath).Length;
        long editedSize   = new FileInfo(editedPath).Length;

        Console.WriteLine($"Original size: {originalSize} bytes");
        Console.WriteLine($"Edited size:   {editedSize} bytes");

        if (originalSize != editedSize)
        {
            Console.WriteLine("File sizes differ – changes applied correctly.");
        }
        else
        {
            Console.WriteLine("File sizes are identical – no changes detected.");
        }
    }
}