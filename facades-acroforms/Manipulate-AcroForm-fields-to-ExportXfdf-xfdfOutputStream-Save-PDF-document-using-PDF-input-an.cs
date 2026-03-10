using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string xfdfPath = "output.xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF into the Form facade (AcroForm handling)
        using (Form form = new Form(inputPdf))
        {
            // Example manipulation: set all text fields to a sample value
            foreach (string fieldName in form.FieldNames)
            {
                try
                {
                    // Get the field type; compare with the FieldType enum
                    FieldType fieldType = form.GetFieldType(fieldName);
                    if (fieldType == FieldType.Text)
                    {
                        form.FillField(fieldName, "Sample");
                    }
                }
                catch
                {
                    // Ignore fields that cannot be processed
                }
            }

            // Export the form field data to XFDF (XML) via a stream
            using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportXfdf(xfdfStream);
            }

            // Save the (potentially modified) PDF to the output file
            form.Save(outputPdf);
        }

        Console.WriteLine("Processing complete: PDF saved and XFDF exported.");
    }
}
