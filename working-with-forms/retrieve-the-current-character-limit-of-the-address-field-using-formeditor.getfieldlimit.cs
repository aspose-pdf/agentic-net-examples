using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade API for form operations

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF and bind it to the Form facade
        using (Form form = new Form(inputPdf))
        {
            // Retrieve the character limit of the field named "Address"
            int addressLimit = form.GetFieldLimit("Address");

            Console.WriteLine($"Current character limit of 'Address' field: {addressLimit}");
        }
    }
}