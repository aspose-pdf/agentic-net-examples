using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF (must exist) and output PDF paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_customername.pdf";

        // Rectangle coordinates for the text field (lower‑left x/y, upper‑right x/y)
        // Adjust these values as needed.
        const float llx = 100f; // lower‑left X
        const float lly = 500f; // lower‑left Y
        const float urx = 300f; // upper‑right X
        const float ury = 530f; // upper‑right Y

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // FormEditor can be instantiated with input and output file names.
        // It implements IDisposable, so we wrap it in a using block for deterministic cleanup.
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // Add a single‑line text field named "CustomerName" on page 1.
            // FieldType.Text specifies a regular text field.
            bool added = formEditor.AddField(FieldType.Text, "CustomerName", 1, llx, lly, urx, ury);

            if (!added)
            {
                Console.Error.WriteLine("Failed to add the text field.");
                return;
            }

            // Persist changes to the output PDF.
            formEditor.Save();
        }

        Console.WriteLine($"Text field \"CustomerName\" added and saved to '{outputPdf}'.");
    }
}