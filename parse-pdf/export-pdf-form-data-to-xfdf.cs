using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXfdf = "output.xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document using the core API
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the Facades Form object (fully qualified, no using directive)
            var form = new Aspose.Pdf.Facades.Form(doc);

            // Export form data to XFDF via a FileStream
            using (FileStream fs = new FileStream(outputXfdf, FileMode.Create, FileAccess.Write))
            {
                form.ExportXfdf(fs);
            }

            // Close the Form facade to release resources
            form.Close();
        }

        Console.WriteLine($"XFDF exported to '{outputXfdf}'.");
    }
}