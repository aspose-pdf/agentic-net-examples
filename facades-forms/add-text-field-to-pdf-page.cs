using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Rectangle coordinates for the text field (lower‑left x/y and upper‑right x/y)
        float llx = 100f;
        float lly = 500f;
        float urx = 300f;
        float ury = 530f;

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use FormEditor (Facades API) to add the field
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPdf);                                   // Load source PDF
            formEditor.AddField(FieldType.Text, "CustomerName", 1, llx, lly, urx, ury); // Add text field on page 1
            formEditor.Save(outputPdf);                                      // Save modified PDF
        }

        Console.WriteLine($"Text field 'CustomerName' added to page 1 and saved as '{outputPdf}'.");
    }
}