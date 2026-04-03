using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF containing the form
        const string pdfPath = "input.pdf";

        // Path where the collected form data will be saved as XML
        const string xmlOutputPath = "formData.xml";

        // Ensure the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Use the Form facade to bind the PDF and export its field data to XML
        using (Form form = new Form())
        {
            // Initialize the facade with the PDF document
            form.BindPdf(pdfPath);

            // Export the form fields to the specified XML file
            using (FileStream xmlStream = new FileStream(xmlOutputPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportXml(xmlStream);
            }
        }

        Console.WriteLine($"Form data exported to '{xmlOutputPath}'.");
    }
}