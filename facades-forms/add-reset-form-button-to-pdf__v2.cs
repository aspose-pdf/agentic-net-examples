using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string outputPath = "output005.pdf";

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Create FormEditor and bind the source PDF.
        FormEditor editor = new FormEditor();
        editor.BindPdf(sourcePath);

        // Add a push‑button field named "ResetForm" on page 2.
        // Rectangle: lower‑left (80,400), upper‑right (180,440).
        editor.AddField(FieldType.PushButton, "ResetForm", 2, 80, 400, 180, 440);

        // Attach JavaScript that resets the whole form when the button is clicked.
        editor.SetFieldScript("ResetForm", "this.resetForm();");

        // Save the result and release resources.
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Button added and saved to '{outputPath}'.");
    }
}
