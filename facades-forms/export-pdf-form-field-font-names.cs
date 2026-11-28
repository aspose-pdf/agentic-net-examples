using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // for WidgetAnnotation and DefaultAppearance

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputPath = "field_fonts.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document (Document, not the Form facade)
        Document doc = new Document(pdfPath);

        // Ensure the document actually contains a form
        if (doc.Form == null || doc.Form.Fields == null)
        {
            Console.Error.WriteLine("The PDF does not contain any form fields.");
            return;
        }

        var lines = new List<string>();

        foreach (Field field in doc.Form.Fields)
        {
            // Field name – use PartialName if available, otherwise FullName
            string fieldName = field?.PartialName ?? field?.FullName ?? "<unnamed>";
            string fontName = "Unknown";

            // All interactive fields inherit from WidgetAnnotation and expose DefaultAppearance.
            // The font name is stored in DefaultAppearance.FontName.
            if (field.DefaultAppearance != null && !string.IsNullOrEmpty(field.DefaultAppearance.FontName))
            {
                fontName = field.DefaultAppearance.FontName;
            }

            lines.Add($"{fieldName}: {fontName}");
        }

        // Write the field‑font mapping to a text file
        File.WriteAllLines(outputPath, lines);

        Console.WriteLine($"Field fonts exported to '{outputPath}'.");
    }
}
