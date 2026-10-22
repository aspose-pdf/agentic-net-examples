using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";
        const string formDataXmlPath = "formdata.xml";
        const string outputPdfPath  = "output.pdf";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(formDataXmlPath))
        {
            Console.Error.WriteLine($"Form data XML not found: {formDataXmlPath}");
            return;
        }

        try
        {
            // Use the Form facade to import XML data into the PDF form.
            // The Form facade preserves the original field order defined in the PDF.
            using (Form pdfForm = new Form(inputPdfPath, outputPdfPath))
            {
                using (FileStream xmlStream = new FileStream(formDataXmlPath, FileMode.Open, FileAccess.Read))
                {
                    pdfForm.ImportXml(xmlStream);
                }
                // Save the updated PDF. The Save call writes to the output path supplied in the constructor.
                pdfForm.Save();
            }

            Console.WriteLine($"Form fields imported and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
