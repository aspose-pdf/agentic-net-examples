using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string xfdfPath = "form_data.xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Load the PDF form using the Form facade (AcroForm handling)
        using (Form form = new Form(inputPdf))
        {
            // -----------------------------------------------------------------
            // Export the current form field values to XFDF.
            // XFDF (XML Forms Data Format) is an XML representation of AcroForm
            // field names and their values. It is commonly used for data exchange
            // between PDF forms and external systems.
            // -----------------------------------------------------------------
            using (FileStream xfdfOut = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportXfdf(xfdfOut);
            }

            // Display the exported XFDF content (optional, for explanation)
            string xfdfContent = File.ReadAllText(xfdfPath);
            Console.WriteLine("Exported XFDF content:");
            Console.WriteLine(xfdfContent);

            // -----------------------------------------------------------------
            // Import the XFDF data back into the PDF.
            // This simulates a scenario where the XFDF file was edited
            // externally and the updated values need to be applied to the form.
            // -----------------------------------------------------------------
            using (FileStream xfdfIn = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportXfdf(xfdfIn);
            }

            // Save the PDF with the (potentially) updated form fields
            form.Save(outputPdf);
            Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
        }
    }
}