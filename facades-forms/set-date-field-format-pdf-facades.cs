using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use FormEditor (Facades) to open the PDF
        using (FormEditor editor = new FormEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPdf);

            // Access the underlying Document object
            Document doc = editor.Document;

            // Iterate over all form fields and adjust date fields
            foreach (var field in doc.Form)
            {
                if (field is DateField dateField)
                {
                    // Set the desired date format
                    dateField.DateFormat = "MM/dd/yyyy";
                }
            }

            // Save the modified PDF
            editor.Save(outputPdf);
            editor.Close();
        }

        Console.WriteLine($"Date field format updated and saved to '{outputPdf}'.");
    }
}