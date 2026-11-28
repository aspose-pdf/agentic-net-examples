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
        const string inputPath  = "input.pdf";
        const string outputPath = "output33.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF with FormEditor (no IDisposable implementation needed)
        FormEditor editor = new FormEditor();
        editor.BindPdf(inputPath);

        // Configure the default appearance for text fields via the Facade
        editor.Facade.Alignment        = FormFieldFacade.AlignLeft;          // left alignment (int constant)
        editor.Facade.BorderColor      = System.Drawing.Color.Black;          // fully qualified to avoid ambiguity
        editor.Facade.BackgroundColor  = System.Drawing.Color.LightGray;      // fully qualified to avoid ambiguity
        editor.Facade.BorderWidth      = FormFieldFacade.BorderWidthThin;    // float constant
        editor.Facade.BorderStyle      = FormFieldFacade.BorderStyleSolid;   // int constant

        // Enumerate all form fields using the high‑level Document API and decorate only text boxes
        Document doc = new Document(inputPath);
        if (doc.Form != null && doc.Form.Fields != null)
        {
            foreach (var field in doc.Form.Fields)
            {
                // TextBoxField is the class representing a text form field
                if (field is TextBoxField)
                {
                    // Decorate the field by its fully qualified name
                    editor.DecorateField(field.FullName);
                }
            }
        }

        // Save the modified PDF
        editor.Save(outputPath);
        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
