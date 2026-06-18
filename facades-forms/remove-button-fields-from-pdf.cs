using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_buttons.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDoc = new Document(inputPath);

        // Collect the names of all button fields
        List<string> buttonFieldNames = new List<string>();
        foreach (Field field in pdfDoc.Form.Fields)
        {
            // In Aspose.Pdf.Forms a button is represented by ButtonField
            if (field is ButtonField)
            {
                // Use PartialName – the actual field identifier
                buttonFieldNames.Add(field.PartialName);
            }
        }

        // Remove the button fields using FormEditor
        using (FormEditor editor = new FormEditor())
        {
            // Bind the already‑loaded document (avoids re‑reading the file)
            editor.BindPdf(pdfDoc);

            foreach (string name in buttonFieldNames)
            {
                editor.RemoveField(name);
            }

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"All button fields removed. Saved to '{outputPath}'.");
    }
}
