using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output18.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF so we can enumerate its form fields.
        Document pdfDoc = new Document(inputPath);

        // FormEditor does not implement IDisposable in older versions, but using it with a
        // using‑statement is safe for newer releases and guarantees proper release.
        using (FormEditor editor = new FormEditor())
        {
            // Bind the loaded document to the editor.
            editor.BindPdf(pdfDoc);

            // Configure the appearance that will be applied to ListBox fields.
            editor.Facade.BorderColor = System.Drawing.Color.Green; // System.Drawing.Color, not Aspose.Pdf.Color
            // Optional: set other appearance defaults if desired.
            // editor.Facade.BorderWidth = FormFieldFacade.BorderWidthThin;
            // editor.Facade.BorderStyle = FormFieldFacade.BorderStyleSolid;

            // Iterate over all form fields and decorate only ListBox fields.
            if (pdfDoc.Form != null && pdfDoc.Form.Fields != null)
            {
                foreach (var field in pdfDoc.Form.Fields)
                {
                    if (field is ListBoxField)
                    {
                        // Apply the facade settings to this specific field.
                        editor.DecorateField(field.Name);
                    }
                }
            }

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
