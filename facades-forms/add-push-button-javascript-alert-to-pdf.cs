using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "output025.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor is a facade for editing form fields.
        using (FormEditor editor = new FormEditor())
        {
            // Bind the existing PDF document.
            editor.BindPdf(inputPath);

            // Add a push‑button field named "AlertBtn" on page 1.
            // Coordinates are in points (1/72 inch). Lower‑left (100,100), upper‑right (200,150).
            editor.AddField(FieldType.PushButton, "AlertBtn", 1, 100, 100, 200, 150);

            // Attach JavaScript that shows an alert when the button is pressed.
            // Use SetFieldScript (or AddFieldScript) – both work for attaching a script to a field.
            editor.SetFieldScript("AlertBtn", "app.alert('Hello');");

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF with push button saved to '{outputPath}'.");
    }
}
