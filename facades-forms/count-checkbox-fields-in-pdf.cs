using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCountPath = "checkbox_count.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Retrieve the collection of form fields (may be null if the PDF has no form).
            var fields = pdfDoc.Form?.Fields;

            // Count only the fields that are check‑boxes.
            int checkboxCount = 0;
            if (fields != null)
            {
                // The correct class name is CheckboxField (lower‑case 'b').
                checkboxCount = fields.Count(f => f is CheckboxField);
            }

            // Write the count to a text file.
            File.WriteAllText(outputCountPath, checkboxCount.ToString());

            Console.WriteLine($"Checkbox fields counted: {checkboxCount}");
            Console.WriteLine($"Count saved to: {outputCountPath}");
        }
    }
}
