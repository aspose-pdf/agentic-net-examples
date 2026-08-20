using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf  = "input.pdf";   // existing PDF with at least 3 pages
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // FormEditor is a Facades class that works with AcroForm fields.
        // It implements IDisposable, so wrap it in a using block (document‑disposal‑with‑using rule).
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // Define the radio button options.
            formEditor.Items = new string[] { "Credit", "PayPal" };

            // Optional visual settings.
            formEditor.RadioGap  = 8;   // gap between the two radio buttons (pixels)
            formEditor.RadioHoriz = true; // arrange horizontally (default)

            // Add the radio button group on page 3.
            // Parameters: field type, field name, page number (1‑based), lower‑left x, lower‑left y,
            // upper‑right x, upper‑right y.
            // Choose a rectangle that fits the desired layout.
            formEditor.AddField(FieldType.Radio, "PaymentMethod", 3, 100, 500, 200, 520);

            // Persist the changes (save‑to‑non‑pdf‑always‑use‑save‑options rule does not apply here
            // because we are saving a PDF; the Save() method writes PDF by default).
            formEditor.Save();
        }

        Console.WriteLine($"Radio button group 'PaymentMethod' added to page 3 and saved as '{outputPdf}'.");
    }
}