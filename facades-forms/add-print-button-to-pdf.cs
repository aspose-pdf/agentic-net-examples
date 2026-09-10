using System;
using System.IO;
using Aspose.Pdf.Facades;   // FormEditor, FieldType, etc.

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // existing PDF
        const string outputPath = "output_with_print.pdf"; // PDF with new button

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF for form editing
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPath);   // load the document

            // Add a push‑button field named "PrintForm" on page 1.
            // Parameters: field type, name, page number (1‑based), llx, lly, urx, ury
            formEditor.AddField(FieldType.PushButton, "PrintForm", 1,
                                100f, 700f, 200f, 750f);

            // Attach JavaScript that opens the print dialog.
            // "this.print(true);" shows the dialog; false would print silently.
            formEditor.AddFieldScript("PrintForm", "this.print(true);");

            // Save the modified PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with PrintForm button: {outputPath}");
    }
}