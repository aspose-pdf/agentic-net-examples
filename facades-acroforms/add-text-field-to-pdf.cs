using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_field.pdf";
        const string fieldName = "NewTextField";

        // Coordinates for the text field (lower-left x, lower-left y, upper-right x, upper-right y)
        const float llx = 100f;
        const float lly = 500f;
        const float urx = 300f;
        const float ury = 530f;
        const int pageNumber = 1; // 1‑based page index

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // FormEditor can be instantiated with input and output file names
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // Add a text field to the specified page and coordinates
            bool added = formEditor.AddField(FieldType.Text, fieldName, pageNumber, llx, lly, urx, ury);
            if (!added)
            {
                Console.Error.WriteLine("Failed to add the text field.");
                return;
            }

            // Save the modified PDF
            formEditor.Save();
        }

        Console.WriteLine($"Text field '{fieldName}' added and saved to '{outputPdf}'.");
    }
}