using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "form.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Create an AutoFiller instance and bind the PDF form file
        using (AutoFiller autoFiller = new AutoFiller())
        {
            // Bind the PDF document to the AutoFiller
            autoFiller.BindPdf(pdfPath);

            // Example: list all field names in the form using the Form facade
            using (Form form = new Form(pdfPath))
            {
                Console.WriteLine("Form fields:");
                foreach (string fieldName in form.FieldNames)
                {
                    Console.WriteLine(fieldName);
                }
            }

            // Additional operations (e.g., importing data, saving) can be performed here
        }
    }
}