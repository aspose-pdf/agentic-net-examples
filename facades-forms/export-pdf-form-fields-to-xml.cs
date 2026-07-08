using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlPath = "form_fields.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Initialize the Form facade with the PDF file
        using (Form form = new Form(pdfPath))
        {
            // Export the form fields to an XML file
            using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportXml(xmlStream);
            }
        }

        Console.WriteLine($"Form fields exported to XML: {xmlPath}");
    }
}