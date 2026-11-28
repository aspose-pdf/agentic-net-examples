using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "FormE.pdf";
        const string outputXfdf = "FormE_form.xfdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Create the Form facade and ensure proper disposal
        using (Form form = new Form())
        {
            // Bind the PDF that contains the form fields
            form.BindPdf(inputPdf);

            // Export the form data to XFDF using a stream overload
            using (FileStream xfdfStream = new FileStream(outputXfdf, FileMode.Create, FileAccess.Write))
            {
                form.ExportXfdf(xfdfStream);
            }
        }

        Console.WriteLine($"XFDF file created: {outputXfdf}");
    }
}