using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        var attachments = new List<(string Path, string Mime, string Description)>
        {
            ("file1.txt", "text/plain", "First text file"),
            ("image1.png", "image/png", "Sample image"),
            ("data.csv", "text/csv", "CSV data")
        };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        foreach (var (path, _, _) in attachments)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Attachment file not found: {path}");
                return;
            }
        }

        using (Document doc = new Document(inputPdf))
        {
            Page page = doc.Pages[1];

            double iconLeft = 50;          // X coordinate of left side
            double iconTop = 750;          // Y coordinate of top side
            double iconWidth = 20;
            double iconHeight = 20;
            double verticalSpacing = 30;   // Space between icons

            foreach (var (path, _, description) in attachments)
            {
                // Create a file specification using the constructor that accepts file path and description
                FileSpecification fileSpec = new FileSpecification(path, description);
                // Description can be set again if needed
                fileSpec.Description = description;

                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    iconLeft,
                    iconTop - iconHeight,
                    iconLeft + iconWidth,
                    iconTop);

                FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
                {
                    Title = Path.GetFileName(path), // Shown in the annotation popup title bar
                    Icon = FileIcon.Paperclip,      // Standard paperclip icon
                    Contents = description          // Tooltip / popup text
                };

                page.Annotations.Add(attachment);
                iconTop -= verticalSpacing;
            }

            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with attachments: {outputPdf}");
    }
}
