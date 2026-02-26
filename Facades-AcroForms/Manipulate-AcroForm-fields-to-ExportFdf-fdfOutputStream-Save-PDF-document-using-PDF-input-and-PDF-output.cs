using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string fdfOutputPath = "output.fdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF as a Form facade (handles AcroForm fields)
        using (Form form = new Form(inputPdfPath))
        {
            // Example manipulation: set a generic value for every field
            // FieldNames provides the full names of all form fields
            foreach (string fieldName in form.FieldNames)
            {
                // Fill each field with a sample string; adjust as needed per field type
                form.FillField(fieldName, "Sample value");
            }

            // Export the current field values to an FDF stream (file)
            using (FileStream fdfStream = new FileStream(fdfOutputPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportFdf(fdfStream);
            }

            // Save the (potentially modified) PDF to the desired output location
            form.Save(outputPdfPath);
        }

        Console.WriteLine("PDF processed, fields exported to FDF, and output PDF saved.");
    }
}