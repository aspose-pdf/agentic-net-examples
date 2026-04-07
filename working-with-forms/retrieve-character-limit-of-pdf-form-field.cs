using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF form using the Form facade
        Form form = new Form(inputPath);

        // Retrieve the character limit for the field named "Address"
        int limit = form.GetFieldLimit("Address");

        Console.WriteLine($"Character limit for 'Address' field: {limit}");
    }
}