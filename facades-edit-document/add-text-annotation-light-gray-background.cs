using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF document to the content editor
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Define the annotation rectangle (System.Drawing.Rectangle is required by PdfContentEditor)
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 200, 100);

        // Create a text annotation on page 1 (this adds the annotation to the page automatically)
        // Parameters: rectangle, contents, title, open flag, author, page number
        editor.CreateText(rect, "This is a note with light gray background.", "Note", true, "Author", 1);

        // Retrieve the underlying Document to adjust the annotation appearance
        Document doc = editor.Document;
        Page page = doc.Pages[1];

        // The annotation we just added is the last one in the page's Annotations collection.
        // Cast it to TextAnnotation to set the background (fill) color.
        if (page.Annotations != null && page.Annotations.Count > 0)
        {
            TextAnnotation txtAnn = null;
            for (int i = page.Annotations.Count - 1; i >= 0; i--)
            {
                if (page.Annotations[i] is TextAnnotation ta && ta.Title == "Note")
                {
                    txtAnn = ta;
                    break;
                }
            }

            if (txtAnn != null)
            {
                // Light gray background improves readability on dark pages.
                // Use Aspose.Pdf.Color to set the interior (background) color.
                txtAnn.Color = Aspose.Pdf.Color.FromRgb(211, 211, 211); // light gray
                // Optionally, set border color if desired
                // txtAnn.Border = new Border(Aspose.Pdf.Color.Black, 1);
            }
        }

        // Save the modified PDF and release resources
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Saved PDF with light gray text annotation to '{outputPath}'.");
    }
}
