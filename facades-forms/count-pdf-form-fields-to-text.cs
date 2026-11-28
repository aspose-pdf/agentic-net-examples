using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "fieldcount.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Bind the PDF to a Form facade and count its fields
            using (Form form = new Form())
            {
                form.BindPdf(inputPdf);                     // Load the PDF
                int fieldCount = form.FieldNames?.Length ?? 0; // Count fields via FieldNames property

                // Write the count to a text file
                File.WriteAllText(outputTxt, fieldCount.ToString());

                Console.WriteLine($"Field count ({fieldCount}) written to '{outputTxt}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
