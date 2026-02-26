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

        using (Form form = new Form(inputPath))
        {
            string[] fieldNames = form.FieldNames;
            Console.WriteLine("AcroForm field names:");
            foreach (string name in fieldNames)
            {
                Console.WriteLine(name);
            }
        }
    }
}