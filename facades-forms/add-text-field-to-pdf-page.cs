using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf";  // PDF with added text field

        // Rectangle coordinates for the new field (lower‑left X/Y and upper‑right X/Y)
        float llx = 100f; // lower‑left X
        float lly = 500f; // lower‑left Y
        float urx = 300f; // upper‑right X
        float ury = 530f; // upper‑right Y

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use FormEditor (facade) to edit the form.
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the existing PDF.
            formEditor.BindPdf(inputPdf);

            // Add a text field named "CustomerName" on page 1.
            // Page numbers are 1‑based in Aspose.Pdf.
            formEditor.AddField(FieldType.Text, "CustomerName", 1, llx, lly, urx, ury);

            // Save the modified PDF.
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Text field \"CustomerName\" added and saved to '{outputPdf}'.");
    }
}