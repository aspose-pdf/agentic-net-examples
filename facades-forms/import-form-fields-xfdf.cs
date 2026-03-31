using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "form.pdf";
        const string xfdfFile = "data.xfdf";
        const string outputPdf = "filled_form.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPdf);
            return;
        }

        if (!File.Exists(xfdfFile))
        {
            Console.Error.WriteLine("XFDF file not found: " + xfdfFile);
            return;
        }

        // Load the existing PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create a Form facade bound to the loaded document
            Form pdfForm = new Form(pdfDoc);

            // Open the XFDF file as a stream and import field values
            using (FileStream xfdfStream = new FileStream(xfdfFile, FileMode.Open, FileAccess.Read))
            {
                pdfForm.ImportXfdf(xfdfStream);
            }

            // Save the updated PDF with imported field data
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine("Form fields imported and saved to '" + outputPdf + "'.");
    }
}
