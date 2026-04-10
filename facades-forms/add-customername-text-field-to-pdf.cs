using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // FormEditor, FieldType

class Program
{
    static void Main()
    {
        // Input PDF file (will be created if it does not exist)
        const string inputPdf = "input.pdf";
        // Output PDF file with the new text field
        const string outputPdf = "output.pdf";

        // Rectangle coordinates for the text field (lower‑left x/y and upper‑right x/y)
        const float llx = 100f; // lower‑left X
        const float lly = 500f; // lower‑left Y
        const float urx = 300f; // upper‑right X
        const float ury = 530f; // upper‑right Y

        // Ensure the source PDF exists – create a minimal one if necessary.
        if (!File.Exists(inputPdf))
        {
            using (Document doc = new Document())
            {
                // Add a single blank page.
                doc.Pages.Add();
                doc.Save(inputPdf);
            }
        }

        // Use FormEditor (a Facades class) to edit the form.
        // The class implements IDisposable, so wrap it in a using block for deterministic disposal.
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the existing PDF document.
            formEditor.BindPdf(inputPdf);

            // Add a text field named "CustomerName" on page 1 at the specified rectangle.
            // Page numbers are 1‑based in Aspose.Pdf.
            formEditor.AddField(FieldType.Text, "CustomerName", 1, llx, lly, urx, ury);

            // Save the modified PDF to the output path.
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Text field \"CustomerName\" added to page 1 and saved as '{outputPdf}'.");
    }
}
