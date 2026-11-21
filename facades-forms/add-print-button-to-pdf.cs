using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // FormEditor is a Facades class for working with PDF forms.
            // It implements IDisposable, so we wrap it in a using block.
            using (FormEditor formEditor = new FormEditor())
            {
                // Bind the existing PDF.
                formEditor.BindPdf(inputPath);

                // Define the button rectangle (llx, lly, urx, ury).
                // Page numbers are 1‑based.
                float llx = 100f, lly = 700f, urx = 200f, ury = 750f;
                int pageNumber = 1;

                // Add a push‑button field. Use FieldType.PushButton (not FieldType.Button).
                formEditor.AddField(FieldType.PushButton, "PrintForm", pageNumber,
                    llx, lly, urx, ury);

                // Attach JavaScript that opens the print dialog.
                formEditor.AddFieldScript("PrintForm", "this.print(true);");

                // Save the modified PDF.
                formEditor.Save(outputPath);
            }

            Console.WriteLine($"PDF with PrintForm button saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
