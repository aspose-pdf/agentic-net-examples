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

        // If the source PDF does not exist, create a simple one‑page PDF to work with.
        if (!File.Exists(inputPdf))
        {
            using (Document doc = new Document())
            {
                doc.Pages.Add();               // add a blank page
                doc.Save(inputPdf);            // save the placeholder PDF
            }
        }

        // Open the PDF with FormEditor, add the radio button group, and save the result.
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // Options for the radio button group.
            formEditor.Items = new[] { "Male", "Female", "Other" };
            formEditor.RadioHoriz = true;      // arrange horizontally (default is true)

            // Add the radio button field:
            // FieldType.Radio – type of field
            // "Gender"        – field name
            // "Male"          – default selected option
            // 1               – page number (1‑based)
            // 100, 700        – lower‑left corner (llx, lly)
            // 200, 720        – upper‑right corner (urx, ury)
            formEditor.AddField(FieldType.Radio, "Gender", "Male", 1, 100, 700, 200, 720);

            // Write changes to the output file.
            formEditor.Save();
        }

        Console.WriteLine($"Radio button group \"Gender\" added. Output saved to '{outputPdf}'.");
    }
}