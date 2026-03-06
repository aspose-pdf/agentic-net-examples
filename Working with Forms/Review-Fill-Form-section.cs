using System;
using System.IO;
using Aspose.Pdf.Facades; // Facade API for AcroForm operations

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF with form fields
        const string outputPdf = "filled.pdf";  // destination PDF after filling

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Initialize the Form facade with the source PDF.
        // The Form class implements IDisposable, so wrap it in a using block.
        using (Form form = new Form(inputPdf))
        {
            // Fill a text box field. Use the full field name as defined in the PDF.
            // Example field name: "FirstName"
            form.FillField("FirstName", "John");

            // Fill a check box field. Use true to check, false to uncheck.
            // Example field name: "AgreeTerms"
            form.FillField("AgreeTerms", true);

            // Optionally, you can inspect available field names:
            // foreach (string name in form.FieldNames)
            // {
            //     Console.WriteLine($"Field: {name}");
            // }

            // Save the modified PDF to the output path.
            form.Save(outputPdf);
        }

        Console.WriteLine($"Form fields filled and saved to '{outputPdf}'.");
    }
}