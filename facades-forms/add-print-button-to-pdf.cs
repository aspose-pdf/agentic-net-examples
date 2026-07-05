using System;
using System.IO;
using Aspose.Pdf.Facades; // FormEditor, FieldType

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

        try
        {
            // Load the existing PDF for form editing
            using (FormEditor formEditor = new FormEditor())
            {
                formEditor.BindPdf(inputPath);

                // Add a push button named "PrintForm" on page 1
                // Rectangle coordinates (llx, lly, urx, ury) are in points
                float llx = 100f, lly = 500f, urx = 200f, ury = 550f;
                formEditor.AddField(FieldType.PushButton, "PrintForm", 1, llx, lly, urx, ury);

                // Attach JavaScript that opens the print dialog
                string javaScript = "this.print(true);";
                formEditor.AddFieldScript("PrintForm", javaScript);

                // Save the modified PDF
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