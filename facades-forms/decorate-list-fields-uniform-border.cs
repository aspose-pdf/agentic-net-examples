using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // needed for Border

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF document using the high‑level API (no external exe required)
        Document pdfDocument = new Document(inputPath);

        // If the PDF does not contain a form, just save a copy and exit
        if (pdfDocument.Form == null || pdfDocument.Form.Count == 0)
        {
            Console.WriteLine("No form fields found in the PDF.");
            pdfDocument.Save(outputPath);
            return;
        }

        // Iterate over all form fields and style ListBox (list) fields uniformly
        foreach (Field field in pdfDocument.Form)
        {
            if (field is ListBoxField listBox)
            {
                // Set a uniform border (width only – Border has no Color property)
                listBox.Border = new Border(listBox) { Width = 1 };

                // Set a background‑like color (ListBoxField does not expose a separate
                // BackgroundColor property, the Color property is used for appearance)
                listBox.Color = Aspose.Pdf.Color.LightGray;
            }
        }

        // Save the modified PDF
        pdfDocument.Save(outputPath);
        Console.WriteLine($"All list fields have been styled and saved to '{outputPath}'.");
    }
}
