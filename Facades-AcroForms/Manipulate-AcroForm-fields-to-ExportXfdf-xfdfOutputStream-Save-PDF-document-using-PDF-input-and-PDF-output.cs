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
        const string xfdfPath = "fields.xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF using the Form facade
        using (Form form = new Form(inputPdf))
        {
            // Example manipulation: set all text fields to a sample value
            foreach (string fieldName in form.FieldNames)
            {
                try
                {
                    form.FillField(fieldName, "Sample");
                }
                catch
                {
                    // Ignore fields that cannot be filled with a string (e.g., checkboxes, radio buttons)
                }
            }

            // Export the form fields to XFDF
            using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportXfdf(xfdfStream);
            }

            // Save the modified PDF
            form.Save(outputPdf);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
        Console.WriteLine($"XFDF exported to '{xfdfPath}'.");
    }
}