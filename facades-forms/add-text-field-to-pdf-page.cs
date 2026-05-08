using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Rectangle coordinates for the field (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
        float llx = 100f;
        float lly = 500f;
        float urx = 300f;
        float ury = 550f;

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Add a text field named "CustomerName" on page 1 using FormEditor
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPdf);                                   // Load the PDF
            formEditor.AddField(FieldType.Text, "CustomerName", 1, llx, lly, urx, ury); // Add field
            formEditor.Save(outputPdf);                                      // Save the result
        }

        Console.WriteLine($"Text field added and saved to '{outputPdf}'.");
    }
}