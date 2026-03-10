using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF file that contains AcroForm fields
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF using the Facades Form class
        // The constructor Form(string) binds the PDF file to the facade
        using (Form form = new Form(inputPdf))
        {
            // Retrieve all field names from the form
            string[] fieldNames = form.FieldNames;

            // Output each field name to the console
            Console.WriteLine("AcroForm field names:");
            foreach (string name in fieldNames)
            {
                Console.WriteLine(name);
            }
        }
    }
}