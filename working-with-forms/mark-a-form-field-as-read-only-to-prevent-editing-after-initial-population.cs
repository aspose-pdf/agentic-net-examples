using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "readonly_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Example: set value and make the field read‑only
            // Replace "Name" with the actual field name in your PDF
            if (doc.Form["Name"] is TextBoxField nameField)
            {
                nameField.Value = "John Doe";   // initial population
                nameField.ReadOnly = true;      // prevent further editing
            }

            // If you need to mark all form fields read‑only, iterate annotations:
            // foreach (Page page in doc.Pages)
            // {
            //     foreach (Annotation ann in page.Annotations)
            //     {
            //         if (ann is WidgetAnnotation widget)
            //         {
            //             widget.ReadOnly = true;
            //         }
            //     }
            // }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Read‑only PDF saved to '{outputPath}'.");
    }
}