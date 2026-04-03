using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // XML string containing form field values
        const string xmlData = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<FormData>
    <field name=""Name"">John Doe</field>
    <field name=""Age"">30</field>
</FormData>";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Convert the XML string to a memory stream
        using (MemoryStream xmlStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xmlData)))
        {
            // Initialize the Form facade, bind the PDF, import XML data, and save
            using (Form form = new Form())
            {
                form.BindPdf(inputPdf);          // Load the PDF with form fields
                form.ImportXml(xmlStream);       // Import form data from the XML stream
                form.Save(outputPdf);            // Save the updated PDF
            }
        }

        Console.WriteLine($"Form data imported and saved to '{outputPdf}'.");
    }
}