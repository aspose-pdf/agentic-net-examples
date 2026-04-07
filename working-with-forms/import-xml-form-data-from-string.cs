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
        string xmlData = @"<?xml version='1.0' encoding='UTF-8'?>
<FormData>
    <field name='Name'>John Doe</field>
    <field name='Age'>30</field>
</FormData>";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Initialize the Form facade with source and destination PDF files
        using (Form form = new Form(inputPdf, outputPdf))
        {
            // Convert the XML string to a stream
            using (MemoryStream xmlStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xmlData)))
            {
                // Import the XML data into the PDF form fields
                form.ImportXml(xmlStream);
            }

            // Save the updated PDF
            form.Save();
        }

        Console.WriteLine($"Form data imported and saved to '{outputPdf}'.");
    }
}