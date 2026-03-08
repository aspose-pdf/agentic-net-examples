using System;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "annotated_output.pdf";

        // Verify the source file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Define a set of annotations to add (position, title, contents, open flag, icon, page)
        var annotations = new[]
        {
            new
            {
                Rect   = new Rectangle(100, 700, 200, 100), // x, y, width, height
                Title  = "Note 1",
                Text   = "First custom annotation.",
                Open   = true,
                Icon   = "Comment", // Valid icons: Comment, Key, Note, Help, NewParagraph, Paragraph, Insert
                Page   = 1
            },
            new
            {
                Rect   = new Rectangle(300, 500, 150, 80),
                Title  = "Reminder",
                Text   = "Check this section.",
                Open   = false,
                Icon   = "Key",
                Page   = 2
            },
            new
            {
                Rect   = new Rectangle(50, 400, 250, 120),
                Title  = "Info",
                Text   = "Additional information here.",
                Open   = true,
                Icon   = "Note",
                Page   = 1
            }
        };

        // Use PdfContentEditor facade to modify the PDF
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPdf);

            // Add each annotation using the CreateText method
            foreach (var ann in annotations)
            {
                editor.CreateText(
                    rect:    ann.Rect,
                    title:   ann.Title,
                    contents: ann.Text,
                    open:    ann.Open,
                    icon:    ann.Icon,
                    page:    ann.Page);
            }

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Annotations added and saved to '{outputPdf}'.");
    }
}