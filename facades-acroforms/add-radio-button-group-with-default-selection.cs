using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputPdf = "output.pdf";

        // -------------------------------------------------------------------
        // 1. Create a new PDF document in memory (no external input file needed).
        // -------------------------------------------------------------------
        using (Document pdfDocument = new Document())
        {
            // Add a single blank page – FormEditor needs at least one page.
            pdfDocument.Pages.Add();

            // -------------------------------------------------------------------
            // 2. Bind FormEditor to the in‑memory document.
            // -------------------------------------------------------------------
            FormEditor formEditor = new FormEditor(pdfDocument);

            // -------------------------------------------------------------------
            // 3. Define the radio‑button group options.
            // -------------------------------------------------------------------
            formEditor.Items = new string[] { "Option1", "Option2", "Option3" };

            // Optional visual settings.
            formEditor.RadioHoriz = true;          // arrange horizontally
            formEditor.RadioGap = 4;               // gap between buttons (points)
            formEditor.RadioButtonItemSize = 20;  // size of each radio button (points)

            // -------------------------------------------------------------------
            // 4. Add the radio‑button field.
            //    Parameters: field type, field name, default value, page number,
            //    lower‑left X, lower‑left Y, upper‑right X, upper‑right Y.
            // -------------------------------------------------------------------
            formEditor.AddField(FieldType.Radio, "MyRadioGroup", "Option2", 1, 10, 30, 110, 46);

            // -------------------------------------------------------------------
            // 5. Save the modified document.
            // -------------------------------------------------------------------
            pdfDocument.Save(outputPdf);
        }

        Console.WriteLine($"Radio button group added. Output saved to '{outputPdf}'.");
    }
}
